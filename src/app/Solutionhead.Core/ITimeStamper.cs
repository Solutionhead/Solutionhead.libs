using System;

namespace Solutionhead.Core
{
    public interface ITimeStamper
    {
        DateTime CurrentTimeStamp { get; }
    }
}
