using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solutionhead.Commands;
using Solutionhead.DataAnnotations;
using Solutionhead.Extensions;

namespace Solutionhead.Data.EntityFramework.Tests
{
    [TestFixture]
    public class DatabaseIndexBuilderTests
    {
        private Mock<ITableNameIdentifier> mockTableNameIdentifier;
        private TestableCommandFactory sqlCommandFactory;


        [SetUp]
        public void Setup()
        {
            mockTableNameIdentifier = new Mock<ITableNameIdentifier>();
            mockTableNameIdentifier
                .Setup(mock => mock.GetTableName<SingleIndexOject>())
                .Returns("tblSingleObject");

            sqlCommandFactory = new TestableCommandFactory();
        }

        [Test]
        public void CanCreateSqlCommandForSingleIndex()
        {
            var indexBuilder = new DatabaseIndexBuilder<SingleIndexOject>(mockTableNameIdentifier.Object, sqlCommandFactory);
            indexBuilder.CreateAllIndices();

            Assert.IsNotNull(sqlCommandFactory.Commands);
            Assert.AreEqual(1, sqlCommandFactory.Commands.Count);
        }

        [Test]
        public void CanCreateSqlCommandsForMultipleIndexes()
        {
            var indexBuilder = new DatabaseIndexBuilder<MultipleIndexOject>(mockTableNameIdentifier.Object, sqlCommandFactory);
            indexBuilder.CreateAllIndices();

            Assert.IsNotNull(sqlCommandFactory.Commands);
            Assert.IsNotEmpty(sqlCommandFactory.Commands);

            var o = new MultipleIndexOject();
            var countOfAttributes = o.GetPropertiesWithAttribute<MultipleIndexOject, SingleColumnIndexAttribute>().Count();

            Assert.IsTrue(countOfAttributes > 0);
            Assert.AreEqual(countOfAttributes, sqlCommandFactory.Commands.Count);
        }

        public class TestableCommandFactory : ISqlCommandFactory
        {
            public List<TestableCommand> Commands { get; private set; }

            public TestableCommandFactory()
            {
                Commands = new List<TestableCommand>();
            }

            public SqlCommand BuildCommand(string sqlCommand)
            {
                var command = new TestableCommand(sqlCommand);
                Commands.Add(command);
                return command;
            }
        }

        public class SingleIndexOject
        {
            [SingleColumnIndex]
            public string Index1 { get; set; }
        }
        
        public class MultipleIndexOject
        {
            [SingleColumnIndex]
            public string Index1 { get; set; }

            [SingleColumnIndex]
            public string Index2 { get; set; }

            [SingleColumnIndex]
            public string Index3 { get; set; }
        }

        public class TestableCommand : SqlCommand
        {
            public bool Executed { get; private set; }

            public TestableCommand(string sqlCommand)
                :base(sqlCommand) { }

            public override void Execute()
            {
                Executed = true;
            }
        }
    }
}
