using System;

namespace Solutionhead.Data.Archiving
{
    public class ArchiveRepositoryWriter<TObject> : IArchiveWriter<TObject> where TObject : class
    {
        #region fields

        private readonly IArchiveRepository<TObject> _archiveRepository;    

        private readonly IArchiveObjectPackager<TObject> _archiveObjectPackager;

        #endregion

        #region constructors

        public ArchiveRepositoryWriter(IArchiveRepository<TObject> archiveRepository, IArchiveObjectPackager<TObject> archiveObjectPackager)
        {
            if(archiveRepository == null) { throw new ArgumentNullException("archiveRepository"); }
            if(archiveObjectPackager == null) { throw new ArgumentNullException("archiveObjectPackager"); }

            _archiveRepository = archiveRepository;
            _archiveObjectPackager = archiveObjectPackager;
        }

        #endregion

        #region methods

        public IArchiveObject<TObject> WriteToArchive(TObject objectToArchive)
        {
            var packagedObject = _archiveObjectPackager.PackageObjectForArchiving(objectToArchive);

            return _archiveRepository.Add(packagedObject);
        }

        #endregion
    }
}
