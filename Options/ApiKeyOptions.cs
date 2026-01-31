namespace Hrithik.Security.ApiKeyManagement.Options
{
    /// <summary>
    /// Configuration options for API key management.
    /// </summary>
    public sealed class ApiKeyOptions
    {
        /// <summary>
        /// Secret used for hashing API keys.
        /// </summary>
        public string HashingSecret { get; set; } = string.Empty;
    }
}
