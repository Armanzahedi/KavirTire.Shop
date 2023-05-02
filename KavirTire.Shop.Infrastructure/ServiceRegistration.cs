using EasyCaching.Core.Configurations;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Cache;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KavirTire.Shop.Application.Payments.Services.PaymentService;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Infrastructure.Cache;
using KavirTire.Shop.Infrastructure.Common;
using KavirTire.Shop.Infrastructure.Identity;
using KavirTire.Shop.Infrastructure.PaymentService;
using KavirTire.Shop.Infrastructure.Persistence.Audit.Interceptors;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using KavirTire.Shop.Infrastructure.Persistence.Repository;
using KavirTire.Shop.Infrastructure.RecurringJob;
using MessagePack.Formatters;
using MessagePack.Resolvers;

namespace KavirTire.Shop.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditSaveChangesInterceptor>();
        services.AddScoped<SoftDeleteSaveChangeInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("AppDb"));
        }
        else
        {
            Console.WriteLine($"---------------------{configuration.GetConnectionString("DefaultConnection")}--------------------");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }

        services.AddScoped(typeof(IReadRepository<>), typeof(CachedRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(EfRepository<>));
        services.AddScoped<IInvoiceRepository,InvoiceRepository>();
        
        services.AddScoped<IReadRepository<InventoryItem>, EfRepository<InventoryItem>>();
        

        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<ICacheService, CacheService>();

        services.AddScoped<AppDbContextInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDistributedLock, DistributedLock>();
        
        services.AddScoped<IPaymentServiceFactory, PaymentServiceFactory>();
        services.AddScoped<ISequenceGenerator, SequenceGenerator>();

        services.Configure<RedisOptions>(configuration.GetSection(RedisOptions.Redis));
        services.Configure<DistributedLockOptions>(configuration.GetSection(DistributedLockOptions.DistributedLock));

        services.AddRecurringJobService(configuration);
        
        services.AddLZ4Compressor("lz4");
        services.AddEasyCaching(options =>
        {
            options.WithMessagePack(x =>
            {
                x.EnableCustomResolver = true;
                x.CustomResolvers = CompositeResolver.Create(
                    new IMessagePackFormatter[] { DBNullFormatter.Instance },
                    new MessagePack.IFormatterResolver[]
                {
                    NativeDateTimeResolver.Instance,
                    ContractlessStandardResolverAllowPrivate.Instance,
                    StandardResolverAllowPrivate.Instance
                });
            }, "mymsgpack");
            options.UseRedis(config =>
            {
                config.DBConfig.Endpoints.Add(new ServerEndPoint(configuration.GetSection("Redis")["Address"],
                    Convert.ToInt32(configuration.GetSection("Redis")["Port"])));
                config.DBConfig.AllowAdmin = true;
                config.SerializerName = "mymsgpack";
            }, "redis")
            .WithCompressor("mymsgpack", "lz4");
        });

        return services;
    }
}
