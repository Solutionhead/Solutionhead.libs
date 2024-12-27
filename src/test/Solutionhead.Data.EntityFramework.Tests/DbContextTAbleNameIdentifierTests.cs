using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Solutionhead.Data.EntityFramework.Utilities;

namespace Solutionhead.Data.EntityFramework.Tests
{
    public class TestContext : DbContext
    {
        public TestContext()
            :base(@"data source=.\SOLUTIONSQLSRV;Integrated Security=SSPI;Initial Catalog=Solutionhead.Libs.DeleteMe;MultipleActiveResultSets=True")
        {
            Database.SetInitializer(new TestContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public IDbSet<FooModel> FooModels { get; set; }
    }

    public class TestContextInitializer : DropCreateDatabaseAlways<TestContext>
    {
    }

    public class FooModel
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }

    [TestFixture]
    public class DbContextTableNameIdentifierTests
    {
        private TestContext dbContext;

        [SetUp]
        public void Setup()
        {
            dbContext = new TestContext();   
        }

        [Test]
        public void Test()
        {
            var tableIdentifier = new DbContextTableNameIdentifier(dbContext);

            var tableName = tableIdentifier.GetTableName<FooModel>();

            Assert.AreEqual("[dbo].[FooModels]", tableName);
        }
    }
}
