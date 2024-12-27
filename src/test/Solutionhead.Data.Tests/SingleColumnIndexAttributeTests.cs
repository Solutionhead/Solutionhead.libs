using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Solutionhead.DataAnnotations;

namespace Solutionhead.Data.Tests
{
    [TestFixture]
    public class SingleColumnIndexAttributeTests
    {
        [Test]
        public void IndexIsNotUniqueByDefault()
        {
            var attr = new SingleColumnIndexAttribute();
            Assert.IsFalse(attr.IsUnique);
        }

        [Test]
        public void ConstructorParameterSetsIsUniqueProperty()
        {
            var attr = new SingleColumnIndexAttribute(true);
            Assert.True(attr.IsUnique);
        }
        
        [Test]
        public void IndexIsOrderedAscendingByDefault()
        {
            var attr = new SingleColumnIndexAttribute();
            Assert.AreEqual(IndexOrdering.Ascending, attr.Ordering);
        }
    }
}
