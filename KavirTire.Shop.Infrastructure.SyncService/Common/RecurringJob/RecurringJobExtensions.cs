using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.Extensions.DependencyInjection;
using Owin;

namespace KavirTire.Shop.Infrastructure.SyncService.Common.RecurringJob
{
    public static class RecurringJobExtensions
    {
        public static IServiceCollection AddRecurringJobService(this IServiceCollection services,
            NameValueCollection appsettings)
        {
            GlobalConfiguration.Configuration
                .UseSQLiteStorage("HangfireStorage")
                // .UseSqlServerStorage("AppDbContext")
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();

            var recurringJobServices = GetRecurringJobServices();

            foreach (var item in recurringJobServices)
            {
                services.AddScoped(item);
            }

            return services;
        }

        public static void StartRecurringJobs(this IAppBuilder app, IServiceProvider serviceProvider)
        {
            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(serviceProvider));
            app.UseHangfireDashboard();
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1
            });
            var recurringJobServices = GetRecurringJobServices();
            foreach (var item in recurringJobServices)
            {
                CronScheduleAttribute attribute =
                    (CronScheduleAttribute)item.GetCustomAttributes(typeof(CronScheduleAttribute), false)[0];
                string cronExpression = attribute.CronExpression;
                if (string.IsNullOrEmpty(cronExpression))
                    throw new Exception($"Recurring job service {item} doesn't have a cron expression");


                var recurringJobService = (IRecurringJob)serviceProvider.GetRequiredService(item);
                Hangfire.RecurringJob.AddOrUpdate(item.Name, () => recurringJobService.Run(), cronExpression);
            }
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
}