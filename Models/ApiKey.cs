namespace Hrithik.Security.ApiKeyManagement.Models
{
    /// <summary>
    /// Represents an API key entity.
    /// </summary>
    public class ApiKey
    {
        /// <summary>Unique identifier.</summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>Friendly name for identification.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Hashed API key value.</summary>
        public string KeyHash { get; set; } = string.Empty;

        /// <summary>Creation timestamp (UTC).</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Expiration timestamp (UTC).</summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Current lifecycle status of the API key.
        /// </summary>
        public ApiKeyStatus Status { get; set; } = ApiKeyStatus.Active;

        /// <summary>
        /// Assigned permission scopes.
        /// </summary>
        public IReadOnlyCollection<ApiKeyScope> Scopes { get; set; }
            = Array.Empty<ApiKeyScope>();

        /// <summary>
        /// Indicates whether the API key is expired.
        /// </summary>
        public bool IsExpired =>
            ExpiresAt.HasValue && ExpiresAt.Value <= DateTime.UtcNow;
    }
}
