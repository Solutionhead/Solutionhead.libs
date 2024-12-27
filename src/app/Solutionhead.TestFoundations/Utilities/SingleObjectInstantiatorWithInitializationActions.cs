using System;
using Ploeh.AutoFixture;

namespace Solutionhead.TestFoundations.Utilities
{
    internal class SingleObjectInstantiatorWithInitializationActions<TObject>
    {
        private readonly IFixture _fixture;
        private readonly Action<TObject>[] _initializations;

        public SingleObjectInstantiatorWithInitializationActions(params Action<TObject>[] initializations)
        {
            _fixture = new Fixture().Customize(new MultipleCustomization());
            _fixture.RepeatCount = 3;
            _fixture.Behaviors.Clear();
            _fixture.Behaviors.Add(new NullRecursionBehavior());

            _initializations = initializations;
        }

        public TObject Build()
        {
            try
            {
                var @object = _fixture.Build<TObject>().CreateAnonymous();
                if (_initializations != null)
                {
                    foreach (var initialization in _initializations)
                    {
                        initialization.Invoke(@object);
                    }
                }
                return @object;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error building object graph. Make sure initializations do not reference null objects. Also, register concrete implementation of abstract types in Auto-Fixture by adding a TypeRelay object to the fixture's customizations. See inner exception for details.", ex);
            }
        }
    }
}