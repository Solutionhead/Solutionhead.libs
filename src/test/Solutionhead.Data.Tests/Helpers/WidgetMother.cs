using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solutionhead.Data.Tests.TestClasses;

namespace Solutionhead.Data.Tests.Helpers
{
    internal static class WidgetMother
    {
        public static List<Widget> GetWidgets()
        {
            return new List<Widget>
                       {
                           new Widget("Sprocket"),
                           new Widget("Sprocket"),
                           new Widget("Thingy"),
                           new Widget("Sprocket"),
                           new Widget("Whatsit"),
                           new Widget("Thingy")
                       };
        }
    }
}
