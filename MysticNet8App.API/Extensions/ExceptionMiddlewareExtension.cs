using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using MysticNet8App.Contracts.Exception;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.ErrorModel;

namespace MysticNet8App.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureExceptionHandler(this WebApplication app,
        ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature?.Error is ResourceNotFoundException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.LogError($"StackTrace - {contextFeature?.Error.StackTrace}");
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature?.Error.Message
                    }.ToString());
                }
                else
                {
                    logger.LogError($"Something went wrong: {contextFeature?.Error}");
                    if (contextFeature?.Error != null)
                        logger.LogError($"StackTrace - {contextFeature?.Error.StackTrace}");
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error."
                    }.ToString());
                }
            });
        });
    }
    
}