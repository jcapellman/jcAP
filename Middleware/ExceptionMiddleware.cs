using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using jcAP.API.Controllers.Base;

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var payload = new { Success = false, Data = (object?)null, Error = "An unexpected error occurred.", Errors = (string[]?)null, Timestamp = DateTime.UtcNow };
            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}