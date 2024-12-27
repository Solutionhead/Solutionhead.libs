using System.Collections.Generic;

namespace Solutionhead.Data.Archiving
{
    public interface IArchiveReader<TObject> where TObject : class
    {
        IArchiveObject<TObject> GetArchiveRevision(TObject objectToArchive, string revision);

        IEnumerable<IArchiveObject<TObject>> GetRevisionsForObject(TObject archivedObject);
    }
}
