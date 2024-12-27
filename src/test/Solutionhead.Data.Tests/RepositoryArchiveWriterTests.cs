using System;
using Moq;
using NUnit.Framework;
using Solutionhead.Data.Archiving;
using Solutionhead.Data.Tests.TestClasses;

namespace Solutionhead.Data.Tests
{
    [TestFixture]
    public class RepositoryArchiveWriterTests
    {
        private Mock<IArchiveRepository<Widget>> mockArchiveRepository;
        private TestArchiveObjectPackager<Widget> archiveObjectPackager;
        private ArchiveRepositoryWriter<Widget> writer;

        [SetUp]
        public void Setup()
        {
            mockArchiveRepository = new Mock<IArchiveRepository<Widget>>();

            mockArchiveRepository.Setup(mock => mock.Add(It.IsAny<IArchiveObject<Widget>>())).Verifiable();
            mockArchiveRepository.Setup(mock =>
                mock.Add(It.IsAny<IArchiveObject<Widget>>()))
                .Returns((IArchiveObject<Widget> o) => o);

            archiveObjectPackager = new TestArchiveObjectPackager<Widget>();
            writer = new ArchiveRepositoryWriter<Widget>(mockArchiveRepository.Object, archiveObjectPackager);
        }

        [TestFixture]
        public class WhenConstructing : RepositoryArchiveWriterTests
        {
            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Constructor_Throws_If_ArchiveRepository_Is_Null()
            {
                writer = new ArchiveRepositoryWriter<Widget>(null, archiveObjectPackager);
            }

            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Constructor_Throws_If_ArchiveObjectPackager_Is_Null()
            {
                writer = new ArchiveRepositoryWriter<Widget>(mockArchiveRepository.Object, null);
            }
        }

        [TestFixture]
        public class WhenCallingWriteToArchive : RepositoryArchiveWriterTests
        {
            [Test]
            public void Object_Is_Packaged_By_IArchiveObjectPackager()
            {
                writer.WriteToArchive(new Widget());

                Assert.IsTrue(archiveObjectPackager.WasPackageMethodCalled);
            }

            [Test]
            public void Packaged_Object_Is_Returned()
            {
                var objectToArchive = new Widget();

                var archivedObject = writer.WriteToArchive(objectToArchive);

                Assert.AreEqual(objectToArchive, archivedObject.Object);
                Assert.AreEqual(archiveObjectPackager.Timestamp, archivedObject.DateArchived);
                Assert.AreEqual(archiveObjectPackager.Revision, archivedObject.Revision);
            }

            [Test]
            public void Packaged_Object_Is_Added_To_Repository()
            {
                var objectToArchive = new Widget();

                writer.WriteToArchive(objectToArchive);

                mockArchiveRepository.Verify(mock => mock.Add(archiveObjectPackager.LastPackageCreated), Times.Once());
            }
        }
    }
}
