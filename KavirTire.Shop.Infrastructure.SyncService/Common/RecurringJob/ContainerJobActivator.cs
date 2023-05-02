using System;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.SyncService.Common.RecurringJob
{
    public class ContainerJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ContainerJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetRequiredService(type);
        }
    }
}