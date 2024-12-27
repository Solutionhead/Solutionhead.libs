using System;
using System.Collections.Generic;

namespace Solutionhead.TestFoundations.Utilities
{
    internal class MultipleObjectInstantiatorWithInitializationActions<TObject>
    {
        private readonly SingleObjectInstantiatorWithInitializationActions<TObject> _objectBuilder;
        private readonly int _listSize;

        public MultipleObjectInstantiatorWithInitializationActions(int listSize, params Action<TObject>[] initializations)
        {
            _objectBuilder = new SingleObjectInstantiatorWithInitializationActions<TObject>(initializations);
            _listSize = Math.Max(listSize, 0);
        }

        public List<TObject> BuildList()
        {
            var items = new List<TObject>();

            for (var i = 0; i < _listSize; i++)
            {
                items.Add(_objectBuilder.Build());
            }

            return items;
        }
    }
}