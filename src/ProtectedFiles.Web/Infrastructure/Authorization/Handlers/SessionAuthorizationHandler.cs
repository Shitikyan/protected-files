using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProtectedFiles.Web.Infrastructure.Authorization.Requirements;
using ProtectedFiles.Web.Infrastructure.Constants;
using System.Threading.Tasks;

namespace ProtectedFiles.Web.Infrastructure.Authorization.Handlers
{
    public class SessionAuthorizationHandler : AuthorizationHandler<SessionAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            SessionAuthorizationRequirement requirement)
        {
            var role = requirement.Role;
            var sessionRole = _httpContextAccessor.HttpContext.Session.GetInt32(SessionConstants.LoggedInRoleKey);

            if (sessionRole.HasValue && sessionRole == role)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
