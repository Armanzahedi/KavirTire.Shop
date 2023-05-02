using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
// using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.SyncService
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return new DefaultDependencyResolver(_serviceProvider.CreateScope().ServiceProvider);
        }

        public void Dispose()
        {
            // no-op
        }

        public TService GetService<TService>()
        {
            return (TService)GetService(typeof(TService));
        }
    }
}