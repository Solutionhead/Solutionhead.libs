using System.Web;

namespace Solutionhead.Web.Mvc.Authorization
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated(HttpContextBase context);
    }
}
