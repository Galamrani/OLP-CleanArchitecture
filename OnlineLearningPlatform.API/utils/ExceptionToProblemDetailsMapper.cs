using Microsoft.AspNetCore.Mvc;

namespace OnlineLearningPlatform.API.utils;

public static class ExceptionToProblemDetailsMapper
{
    public static ProblemDetails Map(Exception exception, HttpContext context)
    {
        return new ProblemDetails
        {
            Type = GetProblemType(exception),
            Title = GetTitle(exception),
            Status = GetStatusCode(exception),
            Detail = exception.Message,
            Instance = context.Request.Path,
        };
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        ArgumentException => StatusCodes.Status400BadRequest,
        UnauthorizedAccessException => StatusCodes.Status403Forbidden,
        KeyNotFoundException => StatusCodes.Status404NotFound,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetProblemType(Exception exception) => exception switch
    {
        ArgumentException => "https://datatracker.ietf.org/doc/html/rfc9110#name-400-bad-request",
        UnauthorizedAccessException => "https://datatracker.ietf.org/doc/html/rfc9110#name-403-forbidden",
        KeyNotFoundException => "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found",
        _ => "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error"
    };

    private static string GetTitle(Exception exception) => exception switch
    {
        ArgumentException => "Invalid Request Parameter",
        UnauthorizedAccessException => "Access Denied",
        KeyNotFoundException => "Resource Not Found",
        _ => "Internal Server Error"
    };

}