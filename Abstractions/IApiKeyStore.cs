using Hrithik.Security.ApiKeyManagement.Models;

namespace Hrithik.Security.ApiKeyManagement.Abstractions
{
    /// <summary>
    /// Abstracts persistence for API keys.
    /// </summary>
    public interface IApiKeyStore
    {
        /// <summary>
        /// Persists a new API key.
        /// </summary>
        Task StoreAsync(ApiKey apiKey);

        /// <summary>
        /// Retrieves an API key by its hashed value.
        /// </summary>
        Task<ApiKey?> GetByHashAsync(string keyHash);

        /// <summary>
        /// Retrieves an API key by its unique identifier.
        /// </summary>
        Task<ApiKey?> GetByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing API key.
        /// </summary>
        Task UpdateAsync(ApiKey apiKey);
    }
}
