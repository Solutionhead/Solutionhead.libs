using System;
using Solutionhead.Data.Archiving;

namespace Solutionhead.Data.Tests.Helpers
{
    internal class ArchiveObjectHelper<TObject> where TObject : class
    {
        #region fields

        private DateTime _timeStamp;

        #endregion

        #region properties

        internal DateTime TimeStamp 
        {
            get { return _timeStamp; }
            set 
            {
                _timeStamp = value;
                setRevision();
            }
        }

        internal string Revision { get; set; }

        #endregion

        #region methods

        internal IArchiveObject<TObject> GenerateArchiveObject(TObject objectToArchive)
        {
            TimeStamp = DateTime.Now;

            return new ArchiveObject<TObject>(
                objectToArchive,
                TimeStamp,
                Revision
                );
        }

        private void setRevision()
        {
            Revision = "Revision+" + TimeStamp.Millisecond;
        }

        #endregion
    }
}