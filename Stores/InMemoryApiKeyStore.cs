using Hrithik.Security.ApiKeyManagement.Abstractions;
using Hrithik.Security.ApiKeyManagement.Models;
using System.Collections.Concurrent;

namespace Hrithik.Security.ApiKeyManagement.Stores
{
    /// <summary>
    /// In-memory API key store intended for development, testing, and demos.
    /// Not suitable for production use.
    /// </summary>
    public sealed class InMemoryApiKeyStore : IApiKeyStore
    {
        private readonly ConcurrentDictionary<string, ApiKey> _keysByHash = new();
        private readonly ConcurrentDictionary<Guid, string> _hashById = new();

        /// <inheritdoc />
        public Task StoreAsync(ApiKey apiKey)
        {
            _keysByHash[apiKey.KeyHash] = apiKey;
            _hashById[apiKey.Id] = apiKey.KeyHash;
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<ApiKey?> GetByHashAsync(string keyHash)
        {
            _keysByHash.TryGetValue(keyHash, out var apiKey);
            return Task.FromResult(apiKey);
        }

        /// <inheritdoc />
        public Task UpdateAsync(ApiKey apiKey)
        {
            if (_hashById.TryGetValue(apiKey.Id, out var hash))
            {
                _keysByHash[hash] = apiKey;
            }

            return Task.CompletedTask;
        }

        public Task<ApiKey?> GetByIdAsync(Guid id)
        {
            if (_hashById.TryGetValue(id, out var hash) &&
                _keysByHash.TryGetValue(hash, out var apiKey))
            {
                return Task.FromResult<ApiKey?>(apiKey);
            }

            return Task.FromResult<ApiKey?>(null);
        }
    }
}
