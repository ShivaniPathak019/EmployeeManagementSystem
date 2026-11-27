using EmployeeManagement.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.WebAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    // defining the refvars of requestdelegate and ILogger as read only that means they can initialsed only using constructor 
    //cannot be modified
    //Private to this class, and assignable only in the constructor
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        ProblemDetails problem;

        switch (ex)
        {
            case ValidationFailedException validation:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed",
                    Detail = "One or more validation errors occurred."
                };
                problem.Extensions["errors"] = validation.Errors;
                break;

            case NotFoundException:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = ex.Message
                };
                break;

            case ConflictException conflict:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Conflict",
                    Detail = conflict.Message
                };
                break;

            case UnauthorizedException unauthorized:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = unauthorized.Message
                };
                break;

            case DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } }:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Duplicate value",
                    Detail = "A record with the same unique value already exists."
                };
                break;

            case DbUpdateException { InnerException: SqlException { Number: 547 } }:
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Conflict",
                    Detail = "This record is linked to other records and cannot be changed or removed."
                };
                break;

            default:
                _logger.LogError(ex, "Unhandled exception occurred.");
                problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server error",
                    Detail = "An unexpected error occurred. Please try again later."
                };
                break;
        }

        context.Response.StatusCode = problem.Status!.Value;
        await context.Response.WriteAsJsonAsync(problem);
    }
}