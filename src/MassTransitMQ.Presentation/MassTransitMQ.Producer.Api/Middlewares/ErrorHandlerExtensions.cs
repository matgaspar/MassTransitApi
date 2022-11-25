using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MassTransitMQ.Producer.Api.Middlewares;

public static class ErrorHandlerExtensions
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder appBuilder, ILoggerFactory loggerFactory)
    {
        return appBuilder.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionHandlerFeature = context
                    .Features
                    .Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature != null)
                {
                    var exception = context.Features.Get<Exception>();

                    var logger = loggerFactory.CreateLogger("ErrorHandler");
                    logger.LogError($"Error: {exceptionHandlerFeature.Error}");

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    
                    var statusCode = StatusCodes.Status500InternalServerError; // 500 if unexpected

                    if (exception is Exception) statusCode = StatusCodes.Status404NotFound;
                    //else if (exception is MyUnauthorizedException) code = StatusCodes.Status401Unauthorized;
                    //else if (exception is MyException) code = StatusCodes.Status400BadRequest;

                    var problemDetails = new ProblemDetails()
                    {
                        Title = exception.Message,
                        Detail = exception?.InnerException?.Message,
                        Status = 500,
                    };

                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }
            });
        });
    }

    public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder appBuilder)
    {
        return appBuilder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}