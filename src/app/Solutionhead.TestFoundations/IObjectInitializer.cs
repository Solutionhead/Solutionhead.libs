using System.Collections.Generic;

namespace Solutionhead.TestFoundations
{
    public interface IObjectInitializer<TObject>
    {
        TObject CreateNew();

        List<TObject> BuildListOfSize(int size);
    }
}