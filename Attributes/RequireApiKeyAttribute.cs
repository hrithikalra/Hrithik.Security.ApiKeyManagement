namespace Hrithik.Security.ApiKeyManagement.Attributes
{
    /// <summary>
    /// Enforces presence of a valid API key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class RequireApiKeyAttribute : Attribute
    {
    }
}
