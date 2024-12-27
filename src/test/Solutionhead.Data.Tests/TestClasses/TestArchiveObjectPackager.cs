using System;
using Solutionhead.Data.Archiving;
using Solutionhead.Data.Tests.Helpers;

namespace Solutionhead.Data.Tests.TestClasses
{
    internal class TestArchiveObjectPackager<TObject> : IArchiveObjectPackager<TObject> where TObject : class
    {
        private readonly ArchiveObjectHelper<TObject> helper;

        public TestArchiveObjectPackager()
        {
            helper = new ArchiveObjectHelper<TObject>();
        }

        public IArchiveObject<TObject> PackageObjectForArchiving(TObject objectToArchive)
        {
            WasPackageMethodCalled = true;
            LastPackageCreated = helper.GenerateArchiveObject(objectToArchive);
            return LastPackageCreated;
        }

        public DateTime Timestamp { get { return helper.TimeStamp; } }

        public string Revision { get { return helper.Revision; } }

        public bool WasPackageMethodCalled { get; private set; }

        public IArchiveObject<TObject> LastPackageCreated { get; private set; }
    }
}