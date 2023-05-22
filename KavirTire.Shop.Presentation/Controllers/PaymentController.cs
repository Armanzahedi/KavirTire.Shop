using System.Web;
using KavirTire.Shop.Application.Payments.Commands.ConfirmPayment;
using KavirTire.Shop.Application.Payments.Commands.CreatePayment;
using KavirTire.Shop.Domain.IPGs.Enums;
using KavirTire.Shop.Presentation.Common;
using KavirTire.Shop.Presentation.Filters;
using KavirTire.Shop.Presentation.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KavirTire.Shop.Presentation.Controllers;

public class PaymentController : BaseController
{
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [JsonExceptionFilter]
    [Route("payment")]
    public async Task<IActionResult> Payment([FromBody] CreatePaymentCommand command)
    {
        var result = await Mediator.Send(command);
        Response.Cookies.Delete("inv");
        return Ok(result);
    }

    [Route("payment/redirect")]
    public async Task<IActionResult> PaymentRedirect(Bank bank, string redirectUrl)
    {
        return View(new PaymentRedirect(bank, redirectUrl));
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("payment/verification/sep")]
    public async Task<ActionResult> VerificationSamanKish(Dictionary<string,string?> bankResponse,[FromQuery] Guid ipg,[FromQuery] Guid pmnt,[FromQuery] Guid inv)
    {
        var verificationResponse = await Mediator.Send(new ConfirmPaymentCommand(ipg, pmnt, inv, bankResponse));
        TempData["Status"] = verificationResponse.IsSuccessful;
        TempData["Message"] = verificationResponse.Message;
        TempData["TracNo"] = verificationResponse.TraceNo;
        return RedirectToAction("Confirmation");
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("payment/verification/irankish")]
    public async Task<ActionResult> VerificationIranKish([FromBody] Dictionary<string,string?> bankResponse,[FromQuery] Guid ipg,[FromQuery] Guid pmnt,[FromQuery] Guid inv)
    {
        var verificationResponse = await Mediator.Send(new ConfirmPaymentCommand(ipg, pmnt, inv, bankResponse));
        TempData["Status"] = verificationResponse.IsSuccessful;
        TempData["Message"] = verificationResponse.Message;
        TempData["TracNo"] = verificationResponse.TraceNo;
        return RedirectToAction("Confirmation");
    }
    [Route("payment/confirmation")]
    public IActionResult Confirmation()
    {
        if (TempData["Status"] == null)
            return RedirectToAction("Index", "Home");

        return View(new PaymentConfirmationResult
        {
            IsSuccessful = (bool)TempData["Status"],
            Message = (string?)TempData["Message"] ?? "",
            TraceNo = (string?)TempData["TracNo"] ?? ""
        });   
    }
}