using System.Diagnostics;

namespace Solutionhead.EntityParser
{
    [DebuggerDisplay("{Name,nq}")]
    public class EntityProperty : EntityPropertyBase
    {
        private readonly int? _keyOrder;

        public bool IsKey { get { return _keyOrder != null; } }

        public int KeyOrder { get { return _keyOrder ?? -1; } }

        public EntityProperty(Entity parent, string propertyName, int? keyOrder = null)
            : base(parent, propertyName)
        {
            _keyOrder = keyOrder;
        }
    }
}