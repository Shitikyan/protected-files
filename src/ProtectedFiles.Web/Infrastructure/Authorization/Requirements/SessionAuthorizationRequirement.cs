using Microsoft.AspNetCore.Authorization;

namespace ProtectedFiles.Web.Infrastructure.Authorization.Requirements
{
    public class SessionAuthorizationRequirement : IAuthorizationRequirement
    {
        public int Role { get; set; }

        public SessionAuthorizationRequirement(int role)
        {
            Role = role;
        }
    }
}
