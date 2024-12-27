using System.Collections.Generic;

namespace Solutionhead.EntityParser
{
    public interface IEntityParser
    {
        List<Entity> Entities { get; }
    }
}
