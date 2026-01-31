using System.Security.Cryptography;

namespace Hrithik.Security.ApiKeyManagement.Security
{
    /// <summary>
    /// Generates cryptographically secure API keys.
    /// </summary>
    public static class SecureKeyGenerator
    {
        /// <summary>
        /// Generates a new API key.
        /// </summary>
        public static string Generate(string environment = "live")
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return $"hrk_{environment}_{Convert.ToBase64String(bytes)}";
        }
    }
}
