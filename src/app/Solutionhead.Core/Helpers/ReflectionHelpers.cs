using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Solutionhead.Helpers
{
    public static class ReflectionHelpers
    {
        public static TObject ConstructDefaultInstance<TObject>()
        {
            var type = typeof (TObject);
            var constructor = type.GetConstructor(new Type[0]);

            if(constructor == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "The type '{0}' does not contain a default constructor. Unable to construct an instance of the type.",
                        type.Name));
            }
            
            var constructedObject = constructor.Invoke(null);
            return (TObject) constructedObject;
        }
    }
}
