using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.KavirTire.Shop.Plugins.Entities;
using KavirTire.Shop.KavirTire.Shop.Plugins.Enums;
using KavirTire.Shop.Plugins.Core;
using KavirTire.Shop.Plugins.Core.Repository;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Repositories
{
    public class QuoteRepository : PluginRepository<Quote>
    {
        private readonly PluginBase.LocalPluginContext _currentContext;
        public QuoteRepository(PluginBase.LocalPluginContext currentContext) : base(currentContext)
        {
            _currentContext = currentContext;
        }

        public Guid ConvertQuoteToOrder(Guid quoteId)
        {

            var convertQuoteToSalesOrderRequest = new ConvertQuoteToSalesOrderRequest
            {
                ColumnSet = new ColumnSet("salesorderid"),
                QuoteId = quoteId,
            };

            var convertQuoteResponse =
                (ConvertQuoteToSalesOrderResponse)_currentContext.OrganizationService.Execute(convertQuoteToSalesOrderRequest);
            var orderEntityReference = convertQuoteResponse.Entity.ToEntityReference();

            return orderEntityReference.Id;
        }

        public Quote GetByShopId(Guid shopId)
        {
            var query = new QueryExpression(PluginResource.Quote)
            {
                ColumnSet = new ColumnSet( PluginResource.Quote_QuoteNumber),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(PluginResource.Quote_ShopId, ConditionOperator.Equal, shopId.ToString())
                    }
                }
            };
            return _currentContext.OrganizationService.RetrieveMultiple(query)?.Entities?.FirstOrDefault()?.ToEntity<Quote>();
        }
        public void WonQuote(EntityReference quoteRef)
        {
            WinQuoteRequest winQuoteRequest = new WinQuoteRequest
            {
                QuoteClose = new Entity(PluginResource.QuoteClose)
                {
                    [PluginResource.QuoteClose_QuoteId] = quoteRef,
                    [PluginResource.QuoteClose_Subject] = "Quote Won " + DateTime.Now
                },
                Status = new OptionSetValue((int)QuoteStatusReason.Won)
            };
            _currentContext.OrganizationService.Execute(winQuoteRequest);
        }

        public void CancelQuote(EntityReference quoteRef)
        {
            CloseQuoteRequest closeQuoteRequest = new CloseQuoteRequest()
            {
                QuoteClose = new Entity(PluginResource.QuoteClose)
                {
                    [PluginResource.QuoteClose_QuoteId] = quoteRef,
                    [PluginResource.QuoteClose_Subject] = "Quote Cancel " + DateTime.Now
                },
                Status = new OptionSetValue((int)QuoteStatusReason.Canceled)
            };
            _currentContext.OrganizationService.Execute(closeQuoteRequest);
        }
    }
}
