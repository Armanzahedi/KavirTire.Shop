
// <copyright file="SyncShopWithCRMPlugin.cs" company="">
// Copyright (c) 2023 All Rights Reserved
// </copyright>
// <author></author>
// <date>5/7/2023 9:59:32 AM</date>
// <summary>Implements the SyncShopWithCRMPlugin Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>

using System;
using System.Threading.Tasks;
using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Helper;
using KavirTire.Shop.KavirTire.Shop.Plugins.Repositories;
using KavirTire.Shop.Plugins.Core;
using KavirTire.Shop.Plugins.Core.Enums;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.KavirTire.Shop.Plugins
{

    /// <summary>
    /// SyncShopWithCRMPlugin Plugin.
    /// </summary>    
    public class SyncShopWithCRMPlugin : PluginBase
    {
        public SyncShopWithCRMPlugin(string unsecure, string secure)
            : base(typeof(SyncShopWithCRMPlugin))
        {

            RegisterEvent();
        }

        private void RegisterEvent()
        {
            var SyncShopWithCRMPostEvent =
                new Tuple<int, string, string, Action<LocalPluginContext>>((int)PipelineStep.PreOperation, "bmsd_KavirTireSyncShopWithCRM",
                    null,
                    new Action<LocalPluginContext>(SyncShopWithCRMPostEventHandler));

            RegisteredEvents.Add(SyncShopWithCRMPostEvent);
        }

        private void SyncShopWithCRMPostEventHandler(LocalPluginContext context)
        {

            var generalPolicyRepo = new GeneralPolicyRepository(context);
            var generalPolicy = generalPolicyRepo.GetGeneralPolicy();

            if (generalPolicy == null)
                throw new InvalidPluginExecutionException("General Policy was not found.");

            if (generalPolicy.SyncServiceAddress == null)
                throw new InvalidPluginExecutionException("Sync Service Address was not set in General Policy.");

            var httpCLient = new HttpHelper(context, generalPolicy.SyncServiceAddress);
            Task.Run(async ()=> await httpCLient.Get("/api/sync")).Wait();
        }
    }
}