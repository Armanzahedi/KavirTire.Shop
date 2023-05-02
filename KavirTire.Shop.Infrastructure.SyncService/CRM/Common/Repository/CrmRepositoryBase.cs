using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Xml;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Proxy;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository
{
    public class CrmRepositoryBase<T>: ICrmRepositoryBase<T> where T: Entity
    {
        protected OrganizationServiceContext serviceContext;
        protected OrganizationServiceProxy serviceProxy;
        protected ICrmSettingManager CrmSettingManager;
        public CrmRepositoryBase(ICrmSettingManager currentCrmSettingManager)
        {
            CrmSettingManager = currentCrmSettingManager;
            var serviceFactory = new CrmServiceProxyFactory(CrmSettingManager);
            serviceProxy = serviceFactory.CreateInstance();
            serviceContext = new OrganizationServiceContext(serviceProxy);
        }

        public void SetEntityState(EntityReference entityRef, OptionSetValue state, OptionSetValue status)
        {
            var setStateRequest = new SetStateRequest
            {
                EntityMoniker = entityRef,
                State = state,
                Status = status,
            };
            serviceProxy.Execute(setStateRequest);
        }

        public void DeleteEntity(string logicalName,Guid id)
        {
            serviceProxy.Delete(logicalName,id);
        }
        public string CreateEntity(Entity entity)
        {
            return serviceProxy.Create(entity).ToString();
        }
        public ExecuteMultipleResponse BulkCreate(List<T> entities, ExecuteMultipleSettings settings = null)
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
                CreateRequest createRequest = new CreateRequest { Target = entity };
                multipleRequest.Requests.Add(createRequest);
            }
            return (ExecuteMultipleResponse)serviceProxy.Execute(multipleRequest);
        }
       
        public T GetEntityById(EntityReference entityReference, string[] columns)
        {
            ColumnSet entityColumns = null;
            entityColumns = columns == null ? new ColumnSet(true) : new ColumnSet(columns);
            var entity = serviceProxy.Retrieve(entityReference.LogicalName, entityReference.Id, entityColumns);
            return entity.ToEntity<T>();
        }

        public void UpdateEntity(Entity entity)
        {
            entity.EntityState = EntityState.Changed;
            serviceProxy.Update(entity);
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

            return (ExecuteMultipleResponse)serviceProxy.Execute(multipleRequest);

        }
        public List<T> GetAllEntities(string logicalName)
        {
            var entities = (from s in serviceContext.CreateQuery(logicalName)
                select s).ToList().ConvertAll(a => a.ToEntity<T>());
            return entities;
        }
        protected List<Entity> GetMoreThan5000WithFetchXml(string fetchXml)
        {
            var entities = new List<Entity>();
            int fetchCount = 5000;
            int pageNumber = 1;
            string pagingCookie = null;

            while (true)
            {
                string xml = CreateXml(fetchXml, pagingCookie, pageNumber, fetchCount);
                FetchExpression fetchExpression = new FetchExpression(xml);
                EntityCollection returnCollection = serviceProxy.RetrieveMultiple(fetchExpression);
                if (returnCollection.Entities.Count > 0)
                {
                    entities.AddRange(returnCollection.Entities);
                }

                if (returnCollection.MoreRecords)
                {
                    pageNumber++;
                    pagingCookie = returnCollection.PagingCookie;
                }
                else
                {
                    break;
                }
            }

            return entities;
        }

        protected List<Entity> GetMoreThan5000WithQueryExpression(QueryExpression expression)
        {
            var entities = new List<Entity>();
            int fetchCount = 5000;
            int pageNumber = 1;

            expression.PageInfo = new PagingInfo
            {
                PageNumber = pageNumber,
                Count = fetchCount,
                PagingCookie = null
            };

            while (true)
            {
                var collection = serviceProxy.RetrieveMultiple(expression);
                if (collection.Entities.Count > 0)
                {
                    entities.AddRange(collection.Entities);
                }

                if (collection.MoreRecords)
                {
                    expression.PageInfo.PageNumber++;
                    expression.PageInfo.PagingCookie = collection.PagingCookie;
                }
                else
                {
                    break;
                }
            }

            return entities;
        }

        private string CreateXml(string xml, string cookie, int page, int count)
        {
            var stringReader = new StringReader(xml);
            var reader = new XmlTextReader(stringReader);

            var doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml(doc, cookie, page, count);
        }

        private string CreateXml(XmlDocument doc, string cookie, int page, int count)
        {
            if (doc == null || doc.DocumentElement == null)
                return string.Empty;

            var attrs = doc.DocumentElement.Attributes;

            if (cookie != null)
            {
                var pagingAttr = doc.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                attrs.Append(pagingAttr);
            }

            var pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(page);
            attrs.Append(pageAttr);

            var countAttr = doc.CreateAttribute("count");
            countAttr.Value = System.Convert.ToString(count);
            attrs.Append(countAttr);

            var sb = new StringBuilder(1024);
            var stringWriter = new StringWriter(sb);

            var writer = new XmlTextWriter(stringWriter);
            doc.WriteTo(writer);
            writer.Close();

            return sb.ToString();
        }

        public RepositoryResult IsRecordNotFindException(FaultException<OrganizationServiceFault> ex)
        {
            if (ex != null && ex.Detail.ErrorCode == -2147220969)
            {
                return new RepositoryResult
                {
                    ErrorMessage = ex.ToString(),
                    IsSuccessful = false,
                    ErrorCode = 500
                };
            }

            return null;
        }
    }
}
