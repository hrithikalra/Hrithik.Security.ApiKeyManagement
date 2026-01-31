using Hrithik.Security.ApiKeyManagement.Abstractions;
using Hrithik.Security.ApiKeyManagement.Options;
using Hrithik.Security.ApiKeyManagement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hrithik.Security.ApiKeyManagement.Extensions
{
    /// <summary>
    /// Dependency injection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers API key management services.
        /// </summary>
        public static IServiceCollection AddApiKeyManagement(
            this IServiceCollection services,
            Action<ApiKeyOptions> configure)
        {
            services.Configure(configure);
            services.AddScoped<IApiKeyService, ApiKeyService>();
            return services;
        }
    }
}
