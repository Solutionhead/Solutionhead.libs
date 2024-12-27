using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutionhead.Libs.Mvc3.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AllowAnonymousAttribute : Attribute
    {
    }
}
