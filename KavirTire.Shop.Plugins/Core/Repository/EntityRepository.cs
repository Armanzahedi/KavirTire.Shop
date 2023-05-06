using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using KavirTire.Shop.Plugins.Core.Extensions;
using KavirTire.Shop.Plugins.Core.Resources;

namespace KavirTire.Shop.Plugins.Core.Repository
{
    public class EntityRepository<T> : EntityRepositoryBase where T : Entity
    {
        public EntityRepository(IOrganizationService currentService)
            : base(currentService)
        {
        }

        protected PluginBase.LocalPluginContext context;

        public virtual T GetById(EntityReference refrence)
        {
            return GetEntityById(refrence).ToEntity<T>();
        }

        public virtual T GetById(EntityReference refrence, string[] columns)
        {
            return GetEntityById(refrence, columns).ToEntity<T>();
        }

        private Entity CreatePreImageInstance()
        {
            Entity entity = null;

            string entityLogicalName = context.PluginExecutionContext.PrimaryEntityName;

            if (context.PluginExecutionContext.PreEntityImages.Contains(entityLogicalName) &&
                context.PluginExecutionContext.PreEntityImages[entityLogicalName] != null)
            {
                entity = (Entity)context.PluginExecutionContext.PreEntityImages[entityLogicalName];
            }
            return entity;
        }

        public virtual T CreateInstance<Y>() where Y : EntityWrapper
        {
            Y instance = default(Y);

            if (context.PluginExecutionContext.InputParameters.Contains(PluginResource.Target) &&
                context.PluginExecutionContext.InputParameters[PluginResource.Target] is Entity)
            {
                Entity entity = (Entity)context.PluginExecutionContext.InputParameters[PluginResource.Target];
                Entity entityPreImage = CreatePreImageInstance();

                instance = entity.ToEntity<Y>();
                instance.LogicalName = context.PluginExecutionContext.PrimaryEntityName;
                instance.PreImage = (EntityWrapper)entityPreImage;
            }
            return instance as T;
        }


        public ExecuteMultipleResponse BulkCreate(List<T> entities, ExecuteMultipleSettings settings = null)
        {
            var multipleRequest = new ExecuteMultipleRequest()
            {
                Settings = settings ?? new ExecuteMultipleSettings()
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection()
            };
            foreach (var entity in entities)
            {
                CreateRequest createRequest = new CreateRequest { Target = entity };
                multipleRequest.Requests.Add(createRequest);
            }
            return (ExecuteMultipleResponse)service.Execute(multipleRequest);
        }
        public ExecuteMultipleResponse BulkUpdate(List<T> entities, ExecuteMultipleSettings settings = null)
        {
            var multipleRequest = new ExecuteMultipleRequest()
            {
                Settings = settings ?? new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection()
            };

            foreach (var entity in entities)
            {
                entity.EntityState = EntityState.Changed;
                UpdateRequest updateRequest = new UpdateRequest { Target = entity };
                multipleRequest.Requests.Add(updateRequest);
            }

            return (ExecuteMultipleResponse)service.Execute(multipleRequest);

        }
    }
}