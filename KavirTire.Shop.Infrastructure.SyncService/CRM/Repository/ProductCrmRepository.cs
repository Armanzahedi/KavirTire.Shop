using System;
using System.Collections.Generic;
using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class ProductCrmRepository : CrmRepositoryBase<Product>
    {
        public ProductCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }

        public void UpdateInventoryItem(Entity entity)
        {
            serviceProxy.Update(entity);
        }
        public List<Product> GetActiveProducts()
        {
            var query = new QueryExpression(CrmResource.Product)
            {
                ColumnSet = new ColumnSet(
                    CrmResource.Product_Name,
                    CrmResource.Product_FirstImage
                ),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal,
                            (int)StatusReason.Active)
                    }
                }
            };
            return GetMoreThan5000WithQueryExpression(query)?.Select(e => e.ToEntity<Product>()).ToList();
        }

        public List<InventoryItem> GetActiveInventoryItems()
        {
            var query = new QueryExpression(CrmResource.InventoryItem)
            {
                ColumnSet = new ColumnSet(
                    CrmResource.InventoryItem_ProductId,
                    CrmResource.InventoryItem_ReservedInventory,
                    CrmResource.InventoryItem_InventoryForSale,
                    CrmResource.InventoryItem_Warehouse),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal, (int)StatusReason.Active)
                    }
                }
            };
            return GetMoreThan5000WithQueryExpression(query)?.Select(e => new InventoryItem
            {
                Id = e.Id,
                Product = e.GetAttributeValueOrDefault<EntityReference>(CrmResource.InventoryItem_ProductId),
                Warehouse = e.GetAttributeValueOrDefault<OptionSetValue>(CrmResource.InventoryItem_Warehouse)?.Value,
                InventoryForSale = e.GetAttributeValueOrDefault<int?>(CrmResource.InventoryItem_InventoryForSale),
                ReservedInventory = e.GetAttributeValueOrDefault<int?>(CrmResource.InventoryItem_ReservedInventory)
            }).ToList();
        }
        public List<VehicleTypeProduct> GetActiveVehicleTypeProducts()
        {
            var query = new QueryExpression(CrmResource.VehicleTypeProduct)
            {
                ColumnSet = new ColumnSet(
                    CrmResource.VehicleTypeProduct_Product,
                    CrmResource.VehicleTypeProduct_VehicleType,
                    CrmResource.VehicleTypeProduct_Type),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal, (int)StatusReason.Active)
                    }
                }
            };
            return GetMoreThan5000WithQueryExpression(query)?.Select(e => e.ToEntity<VehicleTypeProduct>()).ToList();
        }
        public List<ProductImage> GetProductImages(List<Guid?> productImageIds)
        {
            var query = new QueryExpression(CrmResource.ProductImage)
            {
                ColumnSet = new ColumnSet(new string[]
                {
                    CrmResource.ProductImage_Product,
                    CrmResource.ProductImage_WebFile
                }),
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal,
                            (int)StatusReason.Active),
                        new ConditionExpression(CrmResource.ProductImage_Id, ConditionOperator.In, productImageIds),
                    }
                },
                LinkEntities =
                {
                    new LinkEntity()
                    {
                        LinkFromEntityName = CrmResource.ProductImage,
                        LinkToEntityName = CrmResource.WebFile,
                        LinkFromAttributeName = CrmResource.ProductImage_WebFile,
                        LinkToAttributeName = CrmResource.WebFile_Id,
                        Columns = new ColumnSet(
                            CrmResource.WebFile_PartialUrl
                        ),
                        LinkCriteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal,
                                    (int)Status.Active),
                                new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal,
                                    (int)StatusReason.Active),
                            }
                        },
                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                Columns = new ColumnSet(
                                    CrmResource.Note_Id,
                                    CrmResource.Note_DocumentBody,
                                    CrmResource.Note_DocumentMimeType),
                                LinkFromEntityName = CrmResource.WebFile,
                                LinkToEntityName = CrmResource.Note,
                                LinkFromAttributeName = CrmResource.WebFile_Id,
                                LinkToAttributeName = CrmResource.Note_Object,
                            }
                        }
                    }
                }
            };
            var retrieveMultipleRequest = new RetrieveMultipleRequest
            {
                Query = query,
            };
            var retrieveMultipleResponse =
                (RetrieveMultipleResponse)this.serviceProxy.Execute(retrieveMultipleRequest);
            var entities = retrieveMultipleResponse?.EntityCollection?.Entities;

            return entities?.Select(entity=>
                new ProductImage
            {
                Id = entity.Id,
                Product = entity.GetAttributeValue<EntityReference>(CrmResource.ProductImage_Product),
                WebFile = entity.GetAttributeValue<EntityReference>(CrmResource.ProductImage_WebFile),
                PartialUrl = (string)entity.GetAttributeValueOrDefault<AliasedValue>("adx_webfile1.adx_partialurl").Value,
                Data = (string)entity.GetAttributeValueOrDefault<AliasedValue>("annotation2.documentbody").Value,
                MimeType = (string)entity.GetAttributeValueOrDefault<AliasedValue>("annotation2.mimetype").Value
            }).ToList();
        }
    }
}