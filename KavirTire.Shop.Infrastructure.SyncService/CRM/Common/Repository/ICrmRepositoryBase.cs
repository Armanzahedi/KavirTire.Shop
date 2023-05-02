using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository
{
    public interface ICrmRepositoryBase<T> : IRepository
    {
        void SetEntityState(EntityReference entityRef, OptionSetValue state, OptionSetValue status);

        void DeleteEntity(string logicalName, Guid id);
        string CreateEntity(Entity entity);
        ExecuteMultipleResponse BulkCreate(List<T> entities, ExecuteMultipleSettings settings = null);
        T GetEntityById(EntityReference entityReference, string[] columns);

        void UpdateEntity(Entity entity);
        ExecuteMultipleResponse BulkUpdate(List<T> entities, ExecuteMultipleSettings settings = null);
        List<T> GetAllEntities(string logicalName);
    }
}
