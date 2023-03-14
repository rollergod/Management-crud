using Management.Application.Common.Interfaces;
using Management.Application.Shared.Errors;
using Management.Application.Shared.Errors.Exceptions;
using Management.Application.Shared.Errors.Exceptions.Abstractions;
using Microsoft.AspNetCore.Diagnostics;

namespace Management.Api.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app,ILoggerManager logger)
        {
            app.UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            OrderWithCurrentNumberAndProviderExist => StatusCodes.Status409Conflict,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wonr: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            Message = contextFeature.Error.Message,
                            StatusCode = context.Response.StatusCode
                        }.ToString());
                    }
                });
            });
        }
    }
}
