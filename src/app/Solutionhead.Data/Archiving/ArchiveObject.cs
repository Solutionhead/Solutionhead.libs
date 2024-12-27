using System;

namespace Solutionhead.Data.Archiving
{
    public class ArchiveObject<TObject> : IArchiveObject<TObject> where TObject : class
    {
        #region fields

        private readonly TObject _objectFromArchive;

        private readonly DateTime _datetimeArchived;

        private readonly string _revision;

        #endregion

        #region constructors

        public ArchiveObject(TObject objectFromArchive, DateTime dateTime, string revision)
        {
            _objectFromArchive = objectFromArchive;
            _datetimeArchived = dateTime;
            _revision = revision;
        }

        #endregion

        #region properties

        public TObject Object { get { return _objectFromArchive; } }

        public DateTime DateArchived { get { return _datetimeArchived; } }

        public string Revision { get { return _revision; } }

        public IArchiveObjectIdentity UniqueIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
