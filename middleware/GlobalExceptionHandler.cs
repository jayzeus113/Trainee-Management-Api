using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TraineeManagement.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        // _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        var (statusCode, title) = exception switch
        {
            BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request"),
            NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            // ServiceUnavailableException => (StatusCodes.Status503ServiceUnavailable, "Service Unavailable"),
            // _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };


        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true; 
    }
}
