using NUnit.Framework;
using Solutionhead.EntityParser;

namespace Solutionhead.Libs.Tests
{
    [TestFixture]
    public class DelegateTests
    {
        public class TestModel
        {
            public int Integer { get; set; }
        }

        [Test]
        public void CreateGetAndSetDelegates()
        {
            var propertyInfo = typeof(TestModel).GetProperty("Integer");

            var get = DelegateHelper.CreateGetDelegate(propertyInfo);
            var set = DelegateHelper.CreateSetDelegate(propertyInfo);

            var test = new TestModel();
            Assert.AreEqual(test.Integer, get(test));

            set(test, 22);
            Assert.AreEqual(22, test.Integer);
        }
    }
}
