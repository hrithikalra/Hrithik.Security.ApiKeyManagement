using Hrithik.Security.ApiKeyManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Hrithik.Security.ApiKeyManagement.Attributes
{
    /// <summary>
    /// Enforces a specific API key scope.
    /// </summary>
    public sealed class RequireApiKeyScopeAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _scope;

        /// <summary>
        /// Initializes scope requirement.
        /// </summary>
        public RequireApiKeyScopeAttribute(string scope)
        {
            _scope = scope;
        }

        /// <inheritdoc />
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Items.TryGetValue("ApiKey", out var apiKeyObj))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var apiKey = (ApiKey)apiKeyObj;

            if (!apiKey.Scopes.Any(s => s.Name == _scope))
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
