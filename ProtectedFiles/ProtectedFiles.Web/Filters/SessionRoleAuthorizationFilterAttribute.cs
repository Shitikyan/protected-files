using ProtectedFiles.Web.Constants;
using ProtectedFiles.Web.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProtectedFiles.Web.Filters
{
    public class SessionRoleAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        private readonly IEnumerable<Roles> _roles;
        public SessionRoleAuthorizationFilterAttribute(params Roles[] roles)
        {
            _roles = roles ?? new Roles[0];
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var role = (Roles)HttpContext.Current.Session[SessionConstants.ROLE_SESSION_KEY];
            if (!_roles.Contains(role))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                    $"Not logged in as an {_roles.FirstOrDefault().ToString()}");
            }

            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }
    }

}