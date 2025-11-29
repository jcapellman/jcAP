using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace jcAP.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> Logger;
        protected readonly IHostEnvironment _env;

        public BaseController(ILogger<BaseController> logger, IHostEnvironment env)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env;
        }

        protected ActionResult ApiOk()
            => Ok();

        protected ActionResult ApiOk<T>(T? data)
            => data is null
                ? Ok()
                : Ok(new ApiResponse<T>(true, data, null, null));

        // Created response with location
        protected ActionResult ApiCreated<T>(string location, T data)
            => Created(location, new ApiResponse<T>(true, data, null, null));

        // Generic error envelope
        protected ActionResult ApiError(string message, int statusCode = 500)
            => StatusCode(statusCode, new ApiResponse<object>(false, null, message, null));

        // Returns validation problems in the envelope
        protected ActionResult ApiValidationError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .OfType<string>()
                .ToArray();

            return BadRequest(new ApiResponse<object>(false, null, "Validation failed", errors));
        }

        // Paged result helper
        protected ActionResult Paged<T>(IEnumerable<T> items, int page, int pageSize, long total)
        {
            var result = new PagedResult<T>(items ?? Enumerable.Empty<T>(), page, pageSize, total);
            return Ok(new ApiResponse<PagedResult<T>>(true, result, null, null));
        }

        // Convenience accessors for common request metadata
        protected string? GetUserId()
            => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? User?.FindFirst("sub")?.Value;

        protected IEnumerable<string> GetUserRoles()
            => User?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) ?? Enumerable.Empty<string>();

        protected string? GetClientIp()
            => HttpContext?.Connection?.RemoteIpAddress?.ToString();

        protected bool TryGetHeader(string name, out string? value)
        {
            if (Request?.Headers?.TryGetValue(name, out var vals) ?? false)
            {
                value = vals.ToString();
                return true;
            }

            value = null;
            return false;
        }
    }

    public sealed record ApiResponse<T>(bool Success, T? Data, string? Error, IEnumerable<string>? Errors)
    {
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }

    public sealed record PagedResult<T>(IEnumerable<T> Items, int Page, int PageSize, long Total)
    {
        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)Total / PageSize);
    }
}