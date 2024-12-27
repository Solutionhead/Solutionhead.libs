using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Solutionhead.Data.Archiving
{
    public interface IArchiveObjectIdentityBuilder<TObject> where TObject : class
    {
        IArchiveObjectIdentity BuildArchiveObjectIdentity(TObject objectToArchive);
    }
}
