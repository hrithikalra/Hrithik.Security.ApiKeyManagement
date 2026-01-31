namespace Hrithik.Security.ApiKeyManagement.Models
{
    /// <summary>
    /// Represents the lifecycle state of an API key.
    /// </summary>
    public enum ApiKeyStatus
    {
        /// <summary>Key is active and valid.</summary>
        Active = 1,

        /// <summary>Key has been revoked.</summary>
        Revoked = 2,

        /// <summary>Key has expired.</summary>
        Expired = 3
    }
}
