using System;
using NUnit.Framework;
using Solutionhead.Core;
using Solutionhead.Data.Archiving;
using Solutionhead.Data.Tests.TestClasses;

namespace Solutionhead.Data.Tests
{
    [TestFixture]
    public class ArchiveObjectPackagerTests
    {
        private ITimeStamper timeStamper;
        private IRevisionIdGenerator revisionIdGenerator;
        private ArchiveObjectPackager<Widget> archiveObjectPackager;
        private Widget objectToArchive;

        [SetUp]
        public void Setup()
        {
            timeStamper = new TestTimeStamper(DateTime.Today);
            revisionIdGenerator = new TestRevisionIdGenerator("revision_" + DateTime.Now.Millisecond);
            archiveObjectPackager = new ArchiveObjectPackager<Widget>(timeStamper, revisionIdGenerator);
            objectToArchive = new Widget();
        }

        [Test]
        public void Package_Object_For_Archiving_Sets_ObjectToArchive_Property()
        {
            var packagedObject = archiveObjectPackager.PackageObjectForArchiving(objectToArchive);

            Assert.AreEqual(packagedObject.Object, objectToArchive);
        }

        [Test]
        public void Package_Object_For_Archiving_Sets_Timestamp_Property()
        {
            var packagedObject = archiveObjectPackager.PackageObjectForArchiving(objectToArchive);

            Assert.AreEqual(packagedObject.DateArchived, timeStamper.CurrentTimeStamp);
        }

        [Test]
        public void Package_Object_For_Archiving_Sets_Revision_Property()
        {
            var packagedObject = archiveObjectPackager.PackageObjectForArchiving(objectToArchive);

            Assert.AreEqual(revisionIdGenerator.GenerateRevisionId(), packagedObject.Revision);
        }
    }
}
