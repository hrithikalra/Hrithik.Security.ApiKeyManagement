namespace Hrithik.Security.ApiKeyManagement.Models
{
    /// <summary>
    /// Result of API key validation.
    /// </summary>
    public sealed class ApiKeyValidationResult
    {
        /// <summary>Indicates validity.</summary>
        public bool IsValid { get; init; }

        /// <summary>Associated API key when valid.</summary>
        public ApiKey? ApiKey { get; init; }

        /// <summary>Failure reason if invalid.</summary>
        public string? Error { get; init; }

        /// <summary>Creates a success result.</summary>
        public static ApiKeyValidationResult Success(ApiKey key)
            => new() { IsValid = true, ApiKey = key };

        /// <summary>Creates a failure result.</summary>
        public static ApiKeyValidationResult Fail(string error)
            => new() { IsValid = false, Error = error };
    }
}
