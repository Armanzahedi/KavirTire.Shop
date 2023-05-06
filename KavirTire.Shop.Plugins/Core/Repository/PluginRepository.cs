using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using KavirTire.Shop.Plugins.Core.Extensions;
using KavirTire.Shop.Plugins.Core.Resources;

namespace KavirTire.Shop.Plugins.Core.Repository
{
    public class PluginRepository<T> : EntityRepository<T> where T : EntityWrapper
    {
        protected PluginBase.LocalPluginContext context;

        public PluginRepository(PluginBase.LocalPluginContext currentContext)
            : base(currentContext.OrganizationService)
        {
            context = currentContext;
        }

        public virtual T CreateInstanceForCustomAction()
        {
            T typedEntity = default(T);

            if (context.PluginExecutionContext.InputParameters.Contains(PluginResource.Target) &&
                context.PluginExecutionContext.InputParameters[PluginResource.Target] is EntityReference)
            {
                var entityRef = (EntityReference)context.PluginExecutionContext.InputParameters[PluginResource.Target];
                typedEntity = service.Retrieve(entityRef.LogicalName, entityRef.Id, new ColumnSet(true)).ToEntity<T>();
            }
            return typedEntity;
        }

        public virtual T CreateInstance()
        {
            T typedEntity = default(T);

            if (context.PluginExecutionContext.InputParameters.Contains(PluginResource.Target) &&
                context.PluginExecutionContext.InputParameters[PluginResource.Target] is Entity)
            {
                var entity = (Entity)context.PluginExecutionContext.InputParameters[PluginResource.Target];
                typedEntity = entity.ToEntity<T>();
                typedEntity.PreImage = CreatePreImageInstance();
            }
            return typedEntity;
        }

        public virtual T CreatePreImageInstance()
        {
            T typedEntity = default(T);

            EntityLogicalNameAttribute entityAttribute =
                (EntityLogicalNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(EntityLogicalNameAttribute));

            if (context.PluginExecutionContext.PreEntityImages != null && context.PluginExecutionContext.PreEntityImages.Contains(entityAttribute.LogicalName) &&
                context.PluginExecutionContext.PreEntityImages[entityAttribute.LogicalName] != null)
            {
                var entity = (Entity)context.PluginExecutionContext.PreEntityImages[entityAttribute.LogicalName];
                typedEntity = entity.ToEntity<T>();

            }
            return typedEntity;
        }

        public virtual T CreatePostImageInstance()
        {
            T typedEntity = default(T);

            EntityLogicalNameAttribute entityAttribute =
                (EntityLogicalNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(EntityLogicalNameAttribute));

            if (context.PluginExecutionContext.PostEntityImages.Contains(entityAttribute.LogicalName) &&
                context.PluginExecutionContext.PostEntityImages[entityAttribute.LogicalName] != null)
            {
                var entity = (Entity)context.PluginExecutionContext.PostEntityImages[entityAttribute.LogicalName];
                typedEntity = entity.ToEntity<T>();

            }
            return typedEntity;
        }

        public virtual EntityReference GetDeleteEntityReference()
        {
            EntityReference entityReference = null;

            if (context.PluginExecutionContext.InputParameters.Contains(PluginResource.Target) &&
                context.PluginExecutionContext.InputParameters[PluginResource.Target] is EntityReference)
            {
                entityReference = (EntityReference)context.PluginExecutionContext.InputParameters[PluginResource.Target];
            }
            return entityReference;
        }

        public T CreateRelatedEntity(EntityReference refrence)
        {
            var columnSet = new ColumnSet(true);

            var query = new QueryExpression(refrence.LogicalName)
            {
                ColumnSet = columnSet,
                Criteria = new FilterExpression()
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                                {
                                    new ConditionExpression(refrence.LogicalName+"id", ConditionOperator.Equal,
                                                            refrence.Id)
                                }
                }

            };
            Entity relatedQuote =
                 context.OrganizationService.RetrieveMultiple(query).Entities.FirstOrDefault();
            return relatedQuote != null ? relatedQuote.ToEntity<T>() : null;
        }

        public EntityCollection LoadRelatedEntities(EntityReference reference)
        {
            var columnSet = new ColumnSet(true);

            var query = new QueryExpression(reference.LogicalName)
            {
                ColumnSet = columnSet,
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                                {
                                    new ConditionExpression(reference.LogicalName+"id", ConditionOperator.Equal,
                                                            reference.Id)
                                }
                }

            };

            return context.OrganizationService.RetrieveMultiple(query);
        }

        public virtual T GetDeleteEntity(string[] columns)
        {
            var entityRef = GetDeleteEntityReference();
            return GetById(entityRef, columns);
        }

        public virtual T GetDeleteEntity()
        {
            var entityRef = GetDeleteEntityReference();
            return GetById(entityRef);
        }

        public virtual EntityReference CreateEntityReferenceInstance()
        {
            EntityReference entityRef = null;

            if (context.PluginExecutionContext.InputParameters.Contains(PluginResource.Target) &&
                context.PluginExecutionContext.InputParameters[PluginResource.Target] is EntityReference)
            {
                entityRef = (EntityReference)context.PluginExecutionContext.InputParameters[PluginResource.Target];
            }
            return entityRef;
        }

        public List<Entity> GetMoreThan5000WithFetchXml(string fetchXml)
        {
            var entities = new List<Entity>();
            int fetchCount = 5000;
            int pageNumber = 1;
            string pagingCookie = null;

            while (true)
            {
                string xml = CreateXml(fetchXml, pagingCookie, pageNumber, fetchCount);
                FetchExpression fetchExpression = new FetchExpression(xml);
                EntityCollection returnCollection = context.OrganizationService.RetrieveMultiple(fetchExpression);
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

        public List<Entity> GetMoreThan5000WithQueryExpression(QueryExpression expression)
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
                var collection = context.OrganizationService.RetrieveMultiple(expression);
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
    }
}