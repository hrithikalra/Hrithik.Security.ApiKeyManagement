namespace Hrithik.Security.ApiKeyManagement.Models
{
    /// <summary>
    /// Represents a permission scope for an API key.
    /// </summary>
    public sealed class ApiKeyScope
    {
        /// <summary>Scope identifier (e.g. payments:write).</summary>
        public string Name { get; }

        public ApiKeyScope(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
