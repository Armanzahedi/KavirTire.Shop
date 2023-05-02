using System.Globalization;
using System.Reflection;
using FluentValidation;
using KavirTire.Shop.Application.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KavirTire.Shop.Application.Common.Mapping;
using KavirTire.Shop.Application.Common.Behaviors;
using KavirTire.Shop.Application.InventoryItems.Services;
using KavirTire.Shop.Application.Invoices.Services;
using MediatR;


namespace KavirTire.Shop.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMappings();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("fa");
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        });
        
        services.Configure<KavirTireOptions>(configuration.GetSection(KavirTireOptions.KavirTire));

        services.AddScoped<ExpireInvoiceAndReleaseReservedInventoryRecurringJob>();
        services.AddScoped<InventoryItemReservationService>();
        services.AddScoped<GeneralPolicyService>();
        return services;
    }
}