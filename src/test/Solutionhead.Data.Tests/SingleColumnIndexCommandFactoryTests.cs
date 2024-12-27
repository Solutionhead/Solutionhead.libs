using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using NUnit.Framework;
using Solutionhead.Factories;
using Solutionhead.Data.Tests.TestClasses;
using Solutionhead.DataAnnotations;
using Solutionhead.Extensions;

namespace Solutionhead.Data.Tests
{
    [TestFixture]
    public class SingleColumnIndexCommandFactoryTests
    {
        private readonly Mock<ITableNameIdentifier> mockTableNameIdentifier = new Mock<ITableNameIdentifier>();
        private readonly ISqlCommandFactory sqlCommandFactory = new TestableCommandFactory();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsIfTableNameIdentifierIsNull()
        {
            new SingleColumnIndexSqlCommandFactory<DefaultSingleColumnIndex>(null, sqlCommandFactory);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsIfSqlCommandFactoryIsNull()
        {
            new SingleColumnIndexSqlCommandFactory<DefaultSingleColumnIndex>(mockTableNameIdentifier.Object, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildSqlCommandThrowsIfPropertyInfoIsNull()
        {
            var factory = new SingleColumnIndexSqlCommandFactory<DefaultSingleColumnIndex>(mockTableNameIdentifier.Object, sqlCommandFactory);
            factory.BuildSqlCommand(null, new SingleColumnIndexAttribute());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildSqlCommandThrowsIfSingleColumnIndexAttributeIsNull()
        {
            var o = new DefaultSingleColumnIndex();
            var propInfo = o.GetPropertiesWithAttribute<DefaultSingleColumnIndex, SingleColumnIndexAttribute>();
            var factory = new SingleColumnIndexSqlCommandFactory<DefaultSingleColumnIndex>(mockTableNameIdentifier.Object, sqlCommandFactory);
            factory.BuildSqlCommand(propInfo.First(), null);
        }

        [Test]
        public void GeneratesCorrectSqlCommandForDefaultSingleColumnIndex()
        {
            const string expectedSql = "CREATE INDEX IX_Prop1 ON DefaultSingleColumnIndex (Prop1 ASC)";
            var command = buildSqlCommandFromSingleColumnIndexAttribute(new DefaultSingleColumnIndex());

            Assert.IsNotNull(command);
            Assert.AreEqual(expectedSql, command.PublicSqlCommand);
        }

        [Test]
        public void GeneratesCorrectSqlCommandForUniqueSingleColumnIndex()
        {
            const string expectedSql = "CREATE INDEX UNIQUE IX_Prop1 ON UniqueSingleColumnIndex (Prop1 ASC)";
            var command = buildSqlCommandFromSingleColumnIndexAttribute(new UniqueSingleColumnIndex());

            Assert.IsNotNull(command);
            Assert.AreEqual(expectedSql, command.PublicSqlCommand);
        }

        [Test]
        public void GeneratesCorrectSqlCommandForUniqueSingleColumnDescendingIndex()
        {
            const string expectedSql = "CREATE INDEX UNIQUE IX_Prop1 ON UniqueDescendingSingleColumnIndex (Prop1 DESC)";
            var command = buildSqlCommandFromSingleColumnIndexAttribute(new UniqueDescendingSingleColumnIndex());

            Assert.IsNotNull(command);
            Assert.AreEqual(expectedSql, command.PublicSqlCommand);
        }

        private TestableCommand buildSqlCommandFromSingleColumnIndexAttribute<TObject>(TObject tableObject) where TObject : class
        {
            var propInfo = tableObject.GetPropertiesWithAttribute(typeof (SingleColumnIndexAttribute)).First();
            var attr = Attribute.GetCustomAttribute(propInfo, typeof(SingleColumnIndexAttribute)) as SingleColumnIndexAttribute;

            mockTableNameIdentifier.Setup(t => t.GetTableName<TObject>()).Returns(tableObject.GetType().Name);
            var factory = new SingleColumnIndexSqlCommandFactory<TObject>(mockTableNameIdentifier.Object, sqlCommandFactory);
            return factory.BuildSqlCommand(propInfo, attr) as TestableCommand;
        }
    }
}
