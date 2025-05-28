using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace SportScore2.Api.Exception;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        context.Response.ContentType = "application/json";
        int statusCode = exception switch
        {
            NotFoundException _    => (int)HttpStatusCode.NotFound,
            ValidationException _  => (int)HttpStatusCode.BadRequest,
            _                      => (int)HttpStatusCode.InternalServerError
        };

        var problem = new
        {
            type   = exception.GetType().Name,
            title  = exception.Message,
            status = statusCode,
            errors = exception is ValidationException ve ? ve.Errors : null
        };

        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}