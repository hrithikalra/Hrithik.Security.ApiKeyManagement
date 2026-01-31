using Hrithik.Security.ApiKeyManagement.Abstractions;
using Hrithik.Security.ApiKeyManagement.Models;
using Hrithik.Security.ApiKeyManagement.Options;
using Hrithik.Security.ApiKeyManagement.Security;
using Microsoft.Extensions.Options;

namespace Hrithik.Security.ApiKeyManagement.Services
{
    /// <summary>
    /// Default API key service implementation.
    /// </summary>
    public sealed class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyStore _store;
        private readonly ApiKeyHasher _hasher;

        /// <summary>
        /// Initializes service.
        /// </summary>
        public ApiKeyService(
            IApiKeyStore store,
            IOptions<ApiKeyOptions> options)
        {
            _store = store;
            _hasher = new ApiKeyHasher(options.Value.HashingSecret);
        }

        /// <inheritdoc />
        public string GenerateKey(
    string name,
    IEnumerable<ApiKeyScope> scopes,
    DateTime? expiresAt = null)
        {
            var rawKey = SecureKeyGenerator.Generate();
            var hash = _hasher.Hash(rawKey);

            var apiKey = new ApiKey
            {
                Name = name,
                KeyHash = hash,
                Scopes = scopes.ToArray(),
                ExpiresAt = expiresAt
            };

            _store.StoreAsync(apiKey).GetAwaiter().GetResult();
            return rawKey;
        }

        /// <inheritdoc />
        public async Task<ApiKeyValidationResult> ValidateAsync(string rawApiKey)
        {
            var hash = _hasher.Hash(rawApiKey);
            var apiKey = await _store.GetByHashAsync(hash);

            if (apiKey == null)
                return ApiKeyValidationResult.Fail("Invalid API key.");

            if (apiKey.Status == ApiKeyStatus.Revoked)
                return ApiKeyValidationResult.Fail("API key revoked.");

            if (apiKey.IsExpired)
            {
                apiKey.Status = ApiKeyStatus.Expired;
                await _store.UpdateAsync(apiKey);
                return ApiKeyValidationResult.Fail("API key expired.");
            }

            return ApiKeyValidationResult.Success(apiKey);
        }


        /// <inheritdoc />
        public async Task RevokeAsync(Guid apiKeyId)
        {
            var apiKey = await _store.GetByIdAsync(apiKeyId);
            if (apiKey == null) return;

            apiKey.Status = ApiKeyStatus.Revoked;
            await _store.UpdateAsync(apiKey);
        }
    }
}
