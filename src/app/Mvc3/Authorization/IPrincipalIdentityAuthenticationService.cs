using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Solutionhead.Libs.Mvc3.Authorization
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
