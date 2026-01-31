using System.Security.Cryptography;
using System.Text;

namespace Hrithik.Security.ApiKeyManagement.Security
{
    /// <summary>
    /// Provides hashing functionality for API keys.
    /// </summary>
    public sealed class ApiKeyHasher
    {
        private readonly byte[] _secret;

        /// <summary>
        /// Initializes a new hasher using a server-side secret.
        /// </summary>
        public ApiKeyHasher(string secret)
        {
            _secret = Encoding.UTF8.GetBytes(secret);
        }

        /// <summary>
        /// Hashes an API key using HMAC-SHA256.
        /// </summary>
        public string Hash(string apiKey)
        {
            using var hmac = new HMACSHA256(_secret);
            return Convert.ToBase64String(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(apiKey)));
        }
    }
}
