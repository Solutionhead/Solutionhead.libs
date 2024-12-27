using System.Diagnostics;

namespace Solutionhead.EntityParser
{
    [DebuggerDisplay("{Source.Parent.TypeName,nq}.{Source.Name,nq} -> {NavigationalProperty.Name,nq}.{Destination.Name,nq}")]
    public class EntityPropertyNavigationLink
    {
        public EntityProperty Source { get; private set; }

        public EntityNavigationalProperty NavigationalProperty { get; private set; }

        public EntityProperty Destination { get; private set; }

        public EntityPropertyNavigationLink(EntityProperty source, EntityNavigationalProperty navigationalProperty, EntityProperty destination)
        {
            Source = source;
            NavigationalProperty = navigationalProperty;
            Destination = destination;
        }
    }
}