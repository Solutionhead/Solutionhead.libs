using System;

namespace Solutionhead.Core
{
    public class UniversalTimeStamper : ITimeStamper
    {
        public DateTime CurrentTimeStamp
        {
            get { return DateTime.UtcNow; }
        }
    }
}
