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
        UnauthorizedAccessException => StatusCodes.Status403Forbidden,
        InvalidOperationException => StatusCodes.Status400BadRequest,
        KeyNotFoundException => StatusCodes.Status404NotFound,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetProblemType(Exception exception) => exception switch
    {
        InvalidOperationException => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
        UnauthorizedAccessException => "https://tools.ietf.org/html/rfc9110#section-15.5.4",
        KeyNotFoundException => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
        _ => "https://tools.ietf.org/html/rfc9110#section-15.6.1"
    };

    private static string GetTitle(Exception exception) => exception switch
    {
        InvalidOperationException => "Invalid Operation",
        UnauthorizedAccessException => "Access Denied",
        KeyNotFoundException => "Resource Not Found",
        _ => "Internal Server Error"
    };

}