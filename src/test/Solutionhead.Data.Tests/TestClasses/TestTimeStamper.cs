using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solutionhead.Core;

namespace Solutionhead.Data.Tests.TestClasses
{
    public class TestTimeStamper : ITimeStamper
    {
        private readonly DateTime _timeStamp;

        public TestTimeStamper(DateTime timeToReturn)
        {
            _timeStamp = timeToReturn;
        }

        public DateTime CurrentTimeStamp
        {
            get { return _timeStamp; }
        }
    }
}
