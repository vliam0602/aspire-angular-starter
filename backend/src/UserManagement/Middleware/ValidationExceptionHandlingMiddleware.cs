using Microsoft.AspNetCore.Mvc;
using System;
using UserManagement.Features.UserManagement.Validators.CustomException;

namespace UserManagement.Middleware;

public sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };

            if (exception.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exception.Errors;
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
