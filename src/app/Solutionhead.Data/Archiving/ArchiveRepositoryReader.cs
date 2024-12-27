using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutionhead.Data.Archiving
{
    public class ArchiveRepositoryReader<TObject> : IArchiveReader<TObject> where TObject : class
    {
        #region fields

        private readonly IArchiveRepository<TObject> _archiveRepository;

        private readonly IArchiveObjectIdentityBuilder<TObject> archiveObjectIdentityBuilder; 

        #endregion

        #region constructors

        public ArchiveRepositoryReader(IArchiveRepository<TObject> archiveRepository)
        {
            if(archiveRepository == null) { throw new ArgumentNullException("archiveRepository"); }

            _archiveRepository = archiveRepository;
        }

        #endregion

        #region methods

        public IArchiveObject<TObject> GetArchiveRevision(TObject objectToArchive, string revision)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IArchiveObject<TObject>> GetRevisionsForObject(TObject archivedObject)
        {
            if(archivedObject == null) { throw new ArgumentNullException("archivedObject"); }

            var archiveObjectIdentity = archiveObjectIdentityBuilder.BuildArchiveObjectIdentity(archivedObject);

            return _archiveRepository.GetRevisionsForObject(archiveObjectIdentity);
        }

        #endregion
    }
}
