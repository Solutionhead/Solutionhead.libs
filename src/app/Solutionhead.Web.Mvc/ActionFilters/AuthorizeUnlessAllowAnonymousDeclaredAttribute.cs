using System;
using System.Web;
using System.Web.Mvc;
using Solutionhead.Web.Mvc.Authorization;

namespace Solutionhead.Web.Mvc.ActionFilters
{
    public sealed class AuthorizeUnlessAllowAnonymousDeclaredAttribute : AuthorizeAttribute
    {
        private readonly IAuthenticationService _authorizationService;

        #region constructors

        /// <summary>
        /// Determines authorization requirements for an action or controller. Uses IPrincipal to determine authentication by default.
        /// </summary>
        public AuthorizeUnlessAllowAnonymousDeclaredAttribute()
            : this(new PrincipalIdentityAuthenticationService()) { }

        /// <summary>
        /// Determines authorization requirements for an action or controller. Uses supplied authentication service to determine authentication.
        /// </summary>
        /// <param name="authenticationService">Authentication service used to determine authentication status of the user.</param>
        public AuthorizeUnlessAllowAnonymousDeclaredAttribute(IAuthenticationService authenticationService)
        {
            if (authenticationService == null) { throw new NullReferenceException("authenticationService"); }

            _authorizationService = authenticationService;
        }

        #endregion

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var bypassAuthorization = anonymousAccessAllowed(filterContext);

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
            return _authorizationService.IsUserAuthenticated(httpContext);
        }
    }
}
