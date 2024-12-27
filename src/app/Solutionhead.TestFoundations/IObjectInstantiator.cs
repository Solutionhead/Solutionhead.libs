using System;
using Ploeh.AutoFixture;

namespace Solutionhead.TestFoundations
{
    public interface IObjectInstantiator
    {
        IFixture Fixture { get; }

        bool CreateDateTimesWithTimeComponent { get; set; }

        TObject InstantiateObject<TObject>(params Action<TObject>[] initializations);
    }
}