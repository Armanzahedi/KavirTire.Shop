using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using KavirTire.Shop.Application.Common.RecurringJob;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
namespace KavirTire.Shop.Infrastructure.RecurringJob;

public static class RecurringJobExtensions
{
    public static IServiceCollection AddRecurringJobService(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddHangfire(conf => conf
        //     .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        //     .UseSimpleAssemblyNameTypeSerializer()
        //     .UseRecommendedSerializerSettings()
        //     .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
        
        // services.AddHangfireServer(opt => opt.WorkerCount = 1);
        
        // var recurringJobServices = GetRecurringJobServices();

        // foreach (var item in recurringJobServices)
        // {
        //     services.AddScoped(item);
        // }

        return services;
    }
    
    public static IApplicationBuilder StartRecurringJobs(this IApplicationBuilder app)
    {
        // var recurringJobServices = GetRecurringJobServices();

        // using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();

        // foreach (var item in recurringJobServices)
        // {
        //     CronScheduleAttribute attribute = (CronScheduleAttribute)item.GetCustomAttributes(typeof(CronScheduleAttribute), false)[0];
        //     string cronExpression = attribute.CronExpression;
        //     if (string.IsNullOrEmpty(cronExpression))
        //         throw new Exception($"Recurring job service {item} doesn't have a cron expression");
            
            
        //     var recurringJobService = (IRecurringJob)serviceScope.ServiceProvider.GetRequiredService(item);
        //     Hangfire.RecurringJob.AddOrUpdate( item.Name ,() => recurringJobService.Run(), cronExpression);
        // }

        return app;
    }

    private static List<Type> GetRecurringJobServices()
    {
        Assembly assembly = typeof(IRecurringJob).Assembly;
        Type interfaceType = typeof(IRecurringJob);

        var recurringJobServices = assembly.GetExportedTypes()
            .Where(t => t.IsClass && interfaceType.IsAssignableFrom(t))
            .ToList();
        return recurringJobServices;
    }
}