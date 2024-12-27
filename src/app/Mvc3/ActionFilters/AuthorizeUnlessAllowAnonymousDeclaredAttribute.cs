using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Solutionhead.Libs.Mvc3.Authorization;
using System.Web;

namespace Solutionhead.Libs.Mvc3.ActionFilters
{
    public sealed class AuthorizeUnlessAllowAnonymousDeclaredAttribute : AuthorizeAttribute
    {
        private IAuthenticationService authorizationService;

        #region constructors

        /// <summary>
        /// Determines authorization requirements for an action or controller. Uses IPrincipal to determine authentication by default.
        /// </summary>
        public AuthorizeUnlessAllowAnonymousDeclaredAttribute()
            : this(new IPrincipalIdentityAuthenticationService()) { }

        /// <summary>
        /// Determines authorization requirements for an action or controller. Uses supplied authentication service to determine authentication.
        /// </summary>
        /// <param name="authenticationService">Authentication service used to determine authentication status of the user.</param>
        public AuthorizeUnlessAllowAnonymousDeclaredAttribute(IAuthenticationService authenticationService)
        {
            if (authenticationService == null) { throw new NullReferenceException("authenticationService"); }

            this.authorizationService = authenticationService;
        }

        #endregion

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool bypassAuthorization = anonymousAccessAllowed(filterContext);

            if (!bypassAuthorization)
            {
                base.OnAuthorization(filterContext);
            }
        }

        private static bool anonymousAccessAllowed(AuthorizationContext filterContext)
        {
            return actionAllowsAnonymousAccess(filterContext) ||
                            (controllerAllowsAnonymousAccess(filterContext) && !actionRequiresAuthentication(filterContext));
        }

        private static bool actionAllowsAnonymousAccess(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
        }

        private static bool controllerAllowsAnonymousAccess(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
        }

        private static bool actionRequiresAuthentication(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.IsDefined(typeof(AuthorizeAttribute), true);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return authorizationService.IsUserAuthenticated(httpContext);
        }
    }
}
