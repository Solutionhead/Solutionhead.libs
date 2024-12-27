using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Solutionhead.EntityParser
{
    [DebuggerDisplay("{TypeName,nq}")]
    public class Entity
    {
        public Type Type { get; private set; }

        public string TypeName { get { return Type.Name; } }

        public List<EntityProperty> Properties = new List<EntityProperty>();

        public List<EntityNavigationalProperty> NavigationalProperties = new List<EntityNavigationalProperty>();

        public Entity(Type type)
        {
            Type = type;
        }
    }
}