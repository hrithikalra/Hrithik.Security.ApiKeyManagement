using Hrithik.Security.ApiKeyManagement.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Hrithik.Security.ApiKeyManagement.Middleware
{
    /// <summary>
    /// Middleware responsible for API key validation.
    /// </summary>
    public sealed class ApiKeyMiddleware
    {
        private const string HeaderName = "X-API-KEY";
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes middleware.
        /// </summary>
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes middleware logic.
        /// </summary>
        public async Task InvokeAsync(HttpContext context, IApiKeyService service)
        {
            var path = context.Request.Path.Value;

            // Allow Swagger & dev endpoints
            if (path != null &&
                (path.StartsWith("/swagger") ||
                 path.StartsWith("/dev")))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(HeaderName, out var key))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var result = await service.ValidateAsync(key!);

            if (!result.IsValid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            context.Items["ApiKey"] = result.ApiKey!;
            await _next(context);
        }
    }
}
