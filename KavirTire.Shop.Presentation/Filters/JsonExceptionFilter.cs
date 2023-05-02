using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KavirTire.Shop.Presentation.Filters;

public class JsonExceptionFilterAttribute :Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = new ObjectResult(new
        {
            code = 500,
            message = "خطای سیستمی.",
            detailedMessage = context.Exception.Message,
        })
        {
            StatusCode = 500
        };

        context.Result = result;
    }
}