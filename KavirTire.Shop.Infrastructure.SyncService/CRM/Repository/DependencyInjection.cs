using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCrmRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICrmRepositoryBase<>), typeof(CrmRepositoryBase<>));
            services.AddScoped<VehicleTypeCrmRepository>();
            services.AddScoped<PriceListCrmRepository>();
            services.AddScoped<GeneralPolicyCrmRepository>();
            services.AddScoped<LocationCrmRepository>();
            services.AddScoped<ProductCrmRepository>();
            services.AddScoped<PostCostCategoryCrmRepository>();
            services.AddScoped<ContactCrmRepository>();
            services.AddScoped<VehicleCrmRepository>();
            services.AddScoped<OrderCrmRepository>();
            services.AddScoped<WebPageCrmRepository>();
            services.AddScoped<IpgCrmRepository>();
            services.AddScoped<QuoteCrmRepository>();
            
            return services;
        }
    }
}