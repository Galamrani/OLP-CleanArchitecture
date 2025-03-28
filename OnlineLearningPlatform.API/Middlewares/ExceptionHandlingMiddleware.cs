using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.API.utils;

namespace OnlineLearningPlatform.API.Middlewares;


public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate next = next;

    // TODO: add logger

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ProblemDetails problemDetails = ExceptionToProblemDetailsMapper.Map(exception, context);

        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}