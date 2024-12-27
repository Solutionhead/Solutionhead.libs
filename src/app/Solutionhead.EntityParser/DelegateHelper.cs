using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Solutionhead.EntityParser
{
    public static class DelegateHelper
    {
        public static Func<object, object> CreateGetDelegate(PropertyInfo propertyInfo)
        {
            var del = Delegate.CreateDelegate(GenericGet.MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), propertyInfo.GetGetMethod());
            return CreateCompatibleDelegate<Func<object, object>>(del, del.GetType().GetMethod("Invoke"));
        }

        public static Action<object, object> CreateSetDelegate(PropertyInfo propertyInfo)
        {
            var del = Delegate.CreateDelegate(GenericSet.MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), propertyInfo.GetSetMethod());
            return CreateCompatibleDelegate<Action<object, object>>(del, del.GetType().GetMethod("Invoke"));
        }

        private static readonly Type GenericGet = typeof(Func<,>);
        private static readonly Type GenericSet = typeof(Action<,>);

        private static T CreateCompatibleDelegate<T>(object instance, MethodInfo method)
        {
            var methodParameters = method.GetParameters();
            var delegateInfo = typeof(T).GetMethod("Invoke");
            var delegateParameters = delegateInfo.GetParameters();

            // Convert the arguments from the delegate argument type
            // to the method argument type when necessary.
            var arguments = delegateParameters.Select(d => Expression.Parameter(d.ParameterType, d.Name)).ToArray();
            var convertedArguments = new Expression[methodParameters.Length];
            for(var i = 0; i < methodParameters.Length; ++i)
            {
                var methodType = methodParameters[i].ParameterType;
                var delegateType = delegateParameters[i].ParameterType;
                convertedArguments[i] = methodType != delegateType ? (Expression)Expression.Convert(arguments[i], methodType) : arguments[i];
            }

            // Create method call.
            var methodCall = Expression.Call(instance == null ? null : Expression.Constant(instance), method, convertedArguments);

            // Convert return type when necessary.
            var convertedMethodCall = delegateInfo.ReturnType == method.ReturnType ? (Expression)methodCall : Expression.Convert(methodCall, delegateInfo.ReturnType);

            return Expression.Lambda<T>(convertedMethodCall, arguments).Compile();
        }
    }
}
