using System;

namespace Solutionhead.Data.Archiving
{
    public interface IArchiveObject<TObject> where TObject : class
    {
        DateTime DateArchived { get; }

        TObject Object { get; }

        string Revision { get; }

        IArchiveObjectIdentity UniqueIdentifier { get; }
    }
}