using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.SyncService.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddControllers(this IServiceCollection services)
        {
            var controllerTypes = typeof(Startup).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IController).IsAssignableFrom(t)
                            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
        
        
    }
}