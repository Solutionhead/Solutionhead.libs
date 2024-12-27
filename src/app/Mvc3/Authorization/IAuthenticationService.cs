using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Solutionhead.Libs.Mvc3.Authorization
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated(HttpContextBase context);
    }
}
