using System;
using System.Security.Principal;

namespace Solutionhead.Web.Mvc.Authorization
{
    public class IPrincipalIdentityAuthenticationService : IAuthenticationService
    {
        #region IAuthenticationService Members

        public bool IsUserAuthenticated(System.Web.HttpContextBase httpContext)
        {
            if (httpContext == null) { throw new ArgumentNullException("httpContext"); }

            IPrincipal user = httpContext.User;
            return user.Identity.IsAuthenticated;
        }

        #endregion
    }
}
