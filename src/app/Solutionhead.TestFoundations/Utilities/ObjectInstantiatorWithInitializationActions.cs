using System;
using System.Collections.Generic;

namespace Solutionhead.TestFoundations.Utilities
{
    internal class ObjectInstantiatorWithInitializationActions<TObject> : IObjectInitializer<TObject>
    {
        private readonly Action<TObject>[] _initializations;

        public ObjectInstantiatorWithInitializationActions(params Action<TObject>[] initializations)
        {
            _initializations = initializations;
        }

        public List<TObject> BuildListOfSize(int size)
        {
            return new MultipleObjectInstantiatorWithInitializationActions<TObject>(size, _initializations).BuildList();
        }

        public TObject CreateNew()
        {
            return new SingleObjectInstantiatorWithInitializationActions<TObject>(_initializations).Build();
        }
    }
}