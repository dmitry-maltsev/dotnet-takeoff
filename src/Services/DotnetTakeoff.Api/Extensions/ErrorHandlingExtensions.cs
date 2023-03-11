using System.Diagnostics;
using DotnetTakeoff.Api.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog.Enrichers.Span;

namespace DotnetTakeoff.Api.Extensions;

internal static class ErrorHandlingExtensions
{
    public static WebApplicationBuilder AddErrorHandling(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails();

        return builder;
    }

    public static WebApplication UseErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler(error => HandleErrors(error, app.Logger));
        app.UseStatusCodePages();

        return app;
    }

    private static void HandleErrors(IApplicationBuilder app, ILogger logger)
    {
        app.Run(async context =>
        {
            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            if (exception is not null)
            {
                logger.LogError(exception, exception.Message);

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError
                };

                var traceId = Activity.Current?.GetTraceId();
                if (traceId is not null)
                {
                    problem.Extensions.Add("traceId", traceId);
                }

                if (exception is DomainException)
                {
                    problem.Status = StatusCodes.Status400BadRequest;
                    problem.Detail = exception.Message;
                }

                await Results.Problem(problem).ExecuteAsync(context);
            }
        });
    }
}
