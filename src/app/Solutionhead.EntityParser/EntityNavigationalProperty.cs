using System.Collections.Generic;
using System.Diagnostics;

namespace Solutionhead.EntityParser
{
    [DebuggerDisplay("{Name,nq}")]
    public class EntityNavigationalProperty : EntityPropertyBase
    {
        public Entity Target { get; set; }

        public bool IsCollection { get { return PropertyInfo.PropertyType.IsGenericType && PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>); } }

        public EntityNavigationalProperty(Entity parent, string propertyName, Entity target) : base(parent, propertyName)
        {
            Target = target;
        }
    }
}