using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solutionhead.Data.Archiving;
using Solutionhead.Data.Tests.Helpers;
using Solutionhead.Data.Tests.TestClasses;

namespace Solutionhead.Data.Tests
{
    [TestFixture]
    public class RepositoryArchiveReaderTests
    {
        private Mock<IArchiveRepository<Widget>> mockArchiveRepository;
        private ArchiveRepositoryReader<Widget> reader;
        readonly ArchiveObjectHelper<Widget> helper = new ArchiveObjectHelper<Widget>();

        [SetUp]
        public void Setup()
        {
            mockArchiveRepository = new Mock<IArchiveRepository<Widget>>();
            mockArchiveRepository.Setup(mock => mock
                .GetRevisionsForObject(It.IsAny<IArchiveObjectIdentity>()))
                .Returns((IArchiveObjectIdentity identity) =>
                {
                    var widgets = WidgetMother.GetWidgets();
                    return widgets.Select(helper.GenerateArchiveObject).ToList();
                });

            reader = new ArchiveRepositoryReader<Widget>(mockArchiveRepository.Object);
        }

        [Test]
        public void Constructor_Throws_If_IArchiveRepository_Parameter_Is_Null()
        {
            try
            {
                new ArchiveRepositoryReader<Widget>(null);
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail("ArchiveRepositoryReader constructor should have thrown ArgumentNullException.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRevisionsForObject_Throws_If_Object_Parameter_Is_Null()
        {
            reader.GetRevisionsForObject(null);
        }

        [Test]
        public void GetRevisionsForObject_Returns_Null_If_No_Revisions_Exist()
        {
            var widget = new Widget("this item should not exist");
            var revisions = reader.GetRevisionsForObject(widget);

            Assert.IsNull(revisions);
        }
    }
}
