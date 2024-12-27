using System;
using Solutionhead.Core;

namespace Solutionhead.Data.Archiving
{
    public class ArchiveObjectPackager<TObject> : IArchiveObjectPackager<TObject> where TObject : class
    {
        private readonly ITimeStamper _timeStamper;

        private readonly IRevisionIdGenerator _revisionIdGenerator;

        public ArchiveObjectPackager(ITimeStamper timeStamper, IRevisionIdGenerator revisionIdGenerator)
        {
            if(timeStamper == null) { throw new ArgumentNullException("timeStamper"); }
            if(revisionIdGenerator == null) { throw new ArgumentNullException("revisionIdGenerator"); }

            _timeStamper = timeStamper;
            _revisionIdGenerator = revisionIdGenerator;
        }

        public IArchiveObject<TObject> PackageObjectForArchiving(TObject objectToArchive)
        {
            return new ArchiveObject<TObject>
                (objectToArchive, 
                _timeStamper.CurrentTimeStamp, 
                _revisionIdGenerator.GenerateRevisionId());
        }
    }
}
