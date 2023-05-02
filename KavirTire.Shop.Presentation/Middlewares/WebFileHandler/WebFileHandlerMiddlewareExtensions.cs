namespace KavirTire.Shop.Presentation.Middlewares.WebFileHandler;

public static class WebFileHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseWebFileHandler(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<WebFileHandlerMiddleware>();
    }
}