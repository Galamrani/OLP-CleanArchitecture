using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace OnlineLearningPlatform.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, IHostEnvironment environment)
{
    private readonly RequestDelegate next = next;
    private readonly IHostEnvironment environment = environment;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "An unhandled exception occurred while processing the request.");

            context.Response.StatusCode = GetStatusCode(exception);
            context.Response.ContentType = "application/problem+json";

            // ProblemDetails problemDetails = Map(exception, context);
            // string jsonProblemDetails = JsonSerializer.Serialize(problemDetails);
            // Log.Information("Problem details: {ProblemDetailsJson}", jsonProblemDetails);

            // if (environment.IsDevelopment())
            // {
            //     await context.Response.WriteAsJsonAsync(problemDetails);
            // }
        }
    }

    private int GetStatusCode(Exception exception) => exception switch
    {
        ArgumentException => StatusCodes.Status400BadRequest,
        UnauthorizedAccessException => StatusCodes.Status403Forbidden,
        KeyNotFoundException => StatusCodes.Status404NotFound,
        _ => StatusCodes.Status500InternalServerError
    };

    // private ProblemDetails Map(Exception exception, HttpContext context)
    // {
    //     return new ProblemDetails
    //     {
    //         Type = GetProblemType(exception),
    //         Title = GetTitle(exception),
    //         Status = GetStatusCode(exception),
    //         Detail = exception.Message,
    //         Instance = context.Request.Path,
    //     };
    // }

    // private string GetProblemType(Exception exception) => exception switch
    // {
    //     ArgumentException => "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request",
    //     UnauthorizedAccessException => "https://datatracker.ietf.org/doc/html/rfc9110#name-403-forbidden",
    //     KeyNotFoundException => "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found",
    //     _ => "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error"
    // };

    // private string GetTitle(Exception exception) => exception switch
    // {
    //     ArgumentException => "Invalid Request Parameter",
    //     UnauthorizedAccessException => "Access Denied",
    //     KeyNotFoundException => "Resource Not Found",
    //     _ => "Internal Server Error"
    // };
}