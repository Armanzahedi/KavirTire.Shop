using KavirTire.Shop.Application.WebFiles.Queries.GetWebFile;
using MediatR;

namespace KavirTire.Shop.Presentation.Middlewares.WebFileHandler;

public class WebFileHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public WebFileHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestPathContainsWebFile = context.Request.Path.HasValue && context.Request.Path.Value.ToLower().StartsWith("/webfile/");
        if (requestPathContainsWebFile)
        {
            var webFilePartialUrl = context.Request.Path.Value?.ToLower().Substring("/webfile/".Length);
            if (webFilePartialUrl != null)
            {
                ISender mediator = (ISender)context.RequestServices.GetService(typeof(ISender))!;
                
                var webFile = await mediator.Send(new GetWebFileQuery(webFilePartialUrl));

                if (webFile != null)
                {
                    context.Response.ContentType = webFile.MimeType;
                    var fileBytes = Convert.FromBase64String(webFile.Data);
                    await context.Response.Body.WriteAsync(fileBytes, 0, fileBytes.Length);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                return;
            }
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}