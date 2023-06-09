﻿using System.Diagnostics;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Application.Invoices.Commands.CreateInvoice;
using KavirTire.Shop.Application.Invoices.Queries.GetInvoiceDetails;
using KavirTire.Shop.Application.Products.Queries.GetProducts;
using KavirTire.Shop.Application.WebPages.Queries.GetWebPage;
using KavirTire.Shop.Domain.OrderHistory;
using KavirTire.Shop.Domain.VehicleTypes.Enums;
using KavirTire.Shop.Domain.WebPages;
using KavirTire.Shop.Presentation.Filters;
using KavirTire.Shop.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KavirTire.Shop.Presentation.Controllers;

[TermsOfServiceApprovedFilter]
[TypeFilter(typeof(CustomerValidForPurchaseFilterAttribute))]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly GeneralPolicyService _generalPolicyService;

    public HomeController(ILogger<HomeController> logger,
        GeneralPolicyService generalPolicyService)
    {
        _logger = logger;
        _generalPolicyService = generalPolicyService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var generalPolicy = await _generalPolicyService.GetGeneralPolicy();
        if(generalPolicy?.BasketExpirationInMin != null)
            Response.Cookies.Append("cart-exp",generalPolicy.BasketExpirationInMin.ToString());
        
        var queryResult = await Mediator.Send(new GetProductsQuery());
        return View(
            new ShoppingViewModel
            {
                ProductSections = new List<ShoppingViewModelProductSection>
                {
                    new(0, "محصولات",
                        queryResult?.Products?.Where(x => x.ProductType == null).ToList()),
                    new(1, "سایزهای اصلی",
                        queryResult?.Products?.Where(x => x.ProductType == ProductType.Original).ToList()),
                    new(2, "سایزهای جایگزین",
                        queryResult?.Products?.Where(x => x.ProductType == ProductType.Substitute).ToList())
                }
            });
    }

    public async Task<IActionResult> TermsOfService(string? returnUrl = null)
    {
        var saleInfoContentPage = await Mediator.Send(new GetWebPageQuery(WebPages.SaleInformationContentPage));
        return View(new TermsOfServiceViewModel
        {
            ReturnUrl = returnUrl ?? "/",
            WebPage = saleInfoContentPage
        });
    }

    [HttpPost]
    [JsonExceptionFilter]
    public async Task<IActionResult> SubmitCart([FromBody] CreateInvoiceCommand command)
    {
        if (Request.Cookies.TryGetValue("inv", out var val))
            command.ExistingInvoiceId = Guid.Parse(val);

        var invoiceId = await Mediator.Send(command);

        var generalPolicy = await _generalPolicyService.GetGeneralPolicy();
        Response.Cookies.Append("inv", invoiceId.ToString(),
            new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(generalPolicy?.BasketExpirationInMin ?? 100) });
        
        return Ok(new SubmitCartResult { InvoiceId = invoiceId });
    }

    [Route("purchase-summary/{invoiceId}")]
    public async Task<IActionResult> PurchaseSummary(Guid invoiceId)
    {
        var result = await Mediator.Send(new GetInvoiceDetailsQuery(invoiceId));
        return View(result);
    }

    // [TermsOfServiceApprovedFilter]
    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}