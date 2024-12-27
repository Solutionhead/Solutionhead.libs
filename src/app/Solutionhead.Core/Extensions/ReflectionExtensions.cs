using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Solutionhead.Extensions
{
    public static class ReflectionExtensions
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> propertySelector)
        {
            var type = typeof(TSource);
            var member = propertySelector.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method but a property is required.", propertySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field but a property is required.", propertySelector));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.", propertySelector, type));
            }

            return propInfo;
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TSource, TAttribute>(this TSource source)
            where TAttribute : Attribute
        {
            var type = typeof(TAttribute);
            return GetPropertiesWithAttribute(source, type);
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TSource>(this TSource source, Type attributeType)
        {
            var type = source.GetType();
            return type.GetProperties().Where(prop => Attribute.IsDefined(prop, attributeType));
        }
    }
}
