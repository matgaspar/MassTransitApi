using Microsoft.AspNetCore.Mvc;

namespace MassTransitMQ.Producer.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _log;

    public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory log)
    {
        this._next = next;
        this._log = log.CreateLogger("CustomErrorHandler");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(httpContext, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        int statusCode = StatusCodes.Status500InternalServerError;
        
        //if (exception is MyUnauthorizedException) code = StatusCodes.Status401Unauthorized;
        //else if (exception is MyException) code = StatusCodes.Status400BadRequest;

        var errorResponse = new ProblemDetails
        {
            Status = statusCode,
            Title = exception.Message,
            Detail = exception?.InnerException?.Message ?? null
        };

        _log.LogError($"Error: {exception.Message}");
        _log.LogError($"Stack: {exception.StackTrace}");
        
        context.Response.StatusCode = errorResponse.Status ?? StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}