using System.Net;

namespace Shared.Middlewares.MiddlewareErrorHandle.Abstract;

public abstract class AbstractExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public abstract string GetResponse(Exception exception);

    public AbstractExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public virtual async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string data = GetResponse(exception);
            await response.WriteAsync(data);
        }
    }
}
