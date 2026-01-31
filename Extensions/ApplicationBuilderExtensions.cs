using Hrithik.Security.ApiKeyManagement.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Hrithik.Security.ApiKeyManagement.Extensions
{
    /// <summary>
    /// Application pipeline extensions.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Enables API key middleware.
        /// </summary>
        public static IApplicationBuilder UseApiKeyManagement(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
