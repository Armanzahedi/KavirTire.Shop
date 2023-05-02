using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using KavirTire.Shop.Plugins.Core.Extensions;

namespace KavirTire.Shop.Plugins.Core.Repository
{
    public class EntityRepositoryBase
    {
        protected IOrganizationService service;
        protected OrganizationServiceContext serviceContext;

        public EntityRepositoryBase(IOrganizationService currentService)
        {
            service = currentService;
            serviceContext = new OrganizationServiceContext(service);
        }

        public virtual Entity GetEntityById(EntityReference refrence)
        {
            var columnSet = new ColumnSet(true);
            var entity =
                service.Retrieve(refrence.LogicalName, refrence.Id, columnSet);
            return entity;
        }

        public virtual Entity GetEntityById(EntityReference refrence, string[] columns)
        {
            var columnSet = new ColumnSet(columns);
            var entity =
                service.Retrieve(refrence.LogicalName, refrence.Id, columnSet);
            return entity;
        }

        public virtual void UpdateEntity(Entity entity)
        {
            entity.EntityState = EntityState.Changed;
            service.Update(entity);
        }

        public virtual void UpdateEntity(Entity entity, bool ifRowVersionMatch)
        {
            var updateRequest = new UpdateRequest
            {
                Target = entity,
                ConcurrencyBehavior = ifRowVersionMatch ? ConcurrencyBehavior.IfRowVersionMatches : ConcurrencyBehavior.Default
            };
            service.Execute(updateRequest);
        }

        public virtual void SetState(EntityReference entityRef, OptionSetValue state, OptionSetValue status)
        {
            var setStateRequest = new SetStateRequest
            {
                EntityMoniker = entityRef,
                State = state,
                Status = status
            };
            var response = service.Execute(setStateRequest);
        }

        public virtual Guid Create(Entity entity)
        {
            return service.Create(entity);
        }

        public virtual void Delete(EntityReference entityRef)
        {
            service.Delete(entityRef.LogicalName,entityRef.Id);
        }

        public virtual void DeleteByService(EntityReference entityRef)
        {
            var updateLineContact = new EntityWrapper(entityRef.LogicalName)
            {
                Id = entityRef.Id
            };
            
            
            var updateEntity = new UpdateRequest
            {
                Target = updateLineContact
            };

            var deleteRequest = new DeleteRequest
            {
                Target = entityRef
            };

            var requestsCollection = new OrganizationRequestCollection {updateEntity, deleteRequest};
            var executeTransaction = new ExecuteTransactionRequest {Requests = requestsCollection};
            var response = service.Execute(executeTransaction);
        }

        public virtual OrganizationResponse Execute(OrganizationRequest request)
        {
            return service.Execute(request);
        }

    }
}
