using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KavirTire.Shop.Presentation.Filters;

public class TermsOfServiceApprovedFilterAttribute: Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Cookies.ContainsKey("tof-approved"))
        {
            var redirectUrl = "TermsOfService";
            if (context.HttpContext.Request.Path != "/")
                redirectUrl += $"?returnUrl={context.HttpContext.Request.Path + context.HttpContext.Request.QueryString}";
            
            context.Result = new RedirectResult(redirectUrl);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}