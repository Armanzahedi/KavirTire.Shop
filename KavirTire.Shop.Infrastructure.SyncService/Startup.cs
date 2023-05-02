using Microsoft.Owin;
using Owin;
using System.Web.Http;
using KavirTire.Shop.Infrastructure.SyncService.Common.Extensions;
using KavirTire.Shop.Infrastructure.SyncService.Common.RecurringJob;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Repository;
using KavirTire.Shop.Infrastructure.SyncService.Services;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NLog.Internal;

[assembly: OwinStartup(typeof(KavirTire.Shop.Infrastructure.SyncService.Startup))]

namespace KavirTire.Shop.Infrastructure.SyncService
{
    public class Startup 
    {
        void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<AppDbContext>();
            services.AddScoped(typeof(IShopReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IShopRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(DataMigrationService));
            services.AddScoped<ICrmSettingManager, CrmSettingsManager>();
            services.AddCrmRepositories();
            services.AddScoped<SyncCrmService>();
            services.AddRecurringJobService(System.Configuration.ConfigurationManager.AppSettings);
        }
        public void Configuration(IAppBuilder appBuilder)
        {
            
            var services = new ServiceCollection();
            ConfigureServices(services);
            
            HttpConfiguration config = new HttpConfiguration();
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var provider = services.BuildServiceProvider();
            config.DependencyResolver =  new DefaultDependencyResolver(provider);
            // config.MessageHandlers.Add(new ExceptionResponseMiddleware());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            // remove default xml formatter and use json output by default 
            // var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            // config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            config.EnsureInitialized();
            appBuilder.StartRecurringJobs(provider);
            appBuilder.UseWebApi(config);
        }
    }
}
