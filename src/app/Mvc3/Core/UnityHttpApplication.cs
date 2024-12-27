using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Solutionhead.Libs.Mvc3.DependencyInjection;

namespace Solutionhead.Libs.Mvc3.Core
{
    /// <summary>
    /// Bootstrapped start up code utilizing Unity as the DependencyResolver. 
    /// </summary>
    public class UnityHttpApplication : System.Web.HttpApplication
    {
        protected virtual void StartUp() { }

        protected virtual void RegisterRoutes(RouteCollection routes) { }

        protected virtual void RegisterGlobalFilters(GlobalFilterCollection filters) { }

        protected virtual void SetupUnityContainer(IUnityContainer UnityContainer) { }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            StartUp();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var container = InitializeContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private IUnityContainer InitializeContainer()
        {
            var unityContainer = new UnityContainer();

            SetupUnityContainer(unityContainer);

            return unityContainer;
        }
    }
}