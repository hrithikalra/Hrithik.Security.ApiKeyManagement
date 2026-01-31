using Hrithik.Security.ApiKeyManagement.Models;

namespace Hrithik.Security.ApiKeyManagement.Abstractions
{
    /// <summary>
    /// Provides operations for API key generation, validation, rotation, and revocation.
    /// </summary>
    public interface IApiKeyService
    {
        /// <summary>
        /// Generates a new API key with given scopes and optional expiry.
        /// </summary>
        string GenerateKey(
            string name,
            IEnumerable<ApiKeyScope> scopes,
            DateTime? expiresAt = null);

        /// <summary>
        /// Validates an incoming API key.
        /// </summary>
        Task<ApiKeyValidationResult> ValidateAsync(string rawApiKey);

        /// <summary>
        /// Revokes an existing API key.
        /// </summary>
        Task RevokeAsync(Guid apiKeyId);
    }
}
