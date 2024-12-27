using System;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Solutionhead.TestFoundations.Utilities
{
    public static class ObjectInstantiator<TObject>
    {
        public static IObjectInitializer<TObject> InstantiateObject(params Action<TObject>[] initializations)
        {
            return new ObjectInstantiatorWithInitializationActions<TObject>(initializations);
        }
    }

    public class ObjectInstantiator : IObjectInstantiator
    {
        public IFixture Fixture { get; protected set; }

        private bool createDateTimesWithTimeComponent = true;

        private ISpecimenBuilder _dateTimeCustomization = null;

        public bool CreateDateTimesWithTimeComponent
        {
            get { return createDateTimesWithTimeComponent; }
            set
            {
                if (value != createDateTimesWithTimeComponent)
                {
                    createDateTimesWithTimeComponent = value;
                    if (createDateTimesWithTimeComponent && _dateTimeCustomization != null)
                    {
                        Fixture.Customizations.Remove(_dateTimeCustomization);
                        _dateTimeCustomization = null;
                    }
                    else
                    {
                        Fixture.Register(() => dateFixture.CreateAnonymous<DateTime>().Date);
                        _dateTimeCustomization = Fixture.Customizations.Last();
                    }
                }

            }
        }

        private IFixture dateFixture;

        public ObjectInstantiator()
        {
            Fixture = new Fixture().Customize(new MultipleCustomization());
            Fixture.RepeatCount = 3;
            Fixture.Behaviors.Clear();
            Fixture.Behaviors.Add(new NullRecursionBehavior());

            dateFixture = new Fixture();
            CreateDateTimesWithTimeComponent = false;
        }

        public TObject InstantiateObject<TObject>(params Action<TObject>[] initializations)
        {
            try
            {
                var @object = Fixture.Build<TObject>().CreateAnonymous();
                if (initializations != null)
                {
                    foreach (var initialization in initializations)
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