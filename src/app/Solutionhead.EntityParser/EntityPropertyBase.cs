using System;
using System.Collections.Generic;
using System.Reflection;

namespace Solutionhead.EntityParser
{
    public abstract class EntityPropertyBase
    {
        public string Name { get { return PropertyInfo.Name; } }

        public Entity Parent { get; private set; }

        public List<EntityPropertyNavigationLink> NavigationalLinks = new List<EntityPropertyNavigationLink>();

        public object GetValue(object @object)
        {
            return _getDelegate(@object);
        }

        public void SetValue(object @object, object value)
        {
            _setDelegate(@object, value);
        }

        protected PropertyInfo PropertyInfo { get; set; }

        protected EntityPropertyBase(Entity parent, string propertyName)
        {
            if(parent == null) { throw new ArgumentNullException("parent"); }
            Parent = parent;

            PropertyInfo = Parent.Type.GetProperty(propertyName);
            if(PropertyInfo == null) throw new ArgumentException(string.Format("Type '{0}' does not contain property '{1}'", parent.TypeName, propertyName));

            _getDelegate = DelegateHelper.CreateGetDelegate(PropertyInfo);
            _setDelegate = DelegateHelper.CreateSetDelegate(PropertyInfo);
        }

        private readonly Func<object, object> _getDelegate;
        private readonly Action<object, object> _setDelegate;
    }
}