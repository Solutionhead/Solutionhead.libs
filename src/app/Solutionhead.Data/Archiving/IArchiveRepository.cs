using System.Collections.Generic;

namespace Solutionhead.Data.Archiving
{
    public interface IArchiveRepository<TObject> where TObject : class 
    {
        IArchiveObject<TObject> Add(IArchiveObject<TObject> objectToArchive);

        IEnumerable<IArchiveObject<TObject>> GetRevisionsForObject(IArchiveObjectIdentity archiveObjectIdentity);
    }
}
