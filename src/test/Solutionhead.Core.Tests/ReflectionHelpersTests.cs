using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using Solutionhead.Extensions;
using Solutionhead.Helpers;

namespace Solutionhead.Core.Tests
{
    [TestFixture]
    public class ReflectionHelpersTests
    {
        #region construct default instance tests

        [Test]
        public void CanConstructObjectWithParamaterlessConstructor()
        {
            var obj = ReflectionHelpers.ConstructDefaultInstance<ParameterlessConstructorObject>();

            Assert.IsNotNull(obj);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowsIfDefaultConstructorIsNotFound()
        {
            var obj = ReflectionHelpers.ConstructDefaultInstance<ParameteredConstructorObject>();

            Assert.IsNotNull(obj);
        }

        [Test]
        public void CanConstructObjectWithMultipleConstructorsWhenDefualtIsAvailable()
        {
            var obj = ReflectionHelpers.ConstructDefaultInstance<DualConstructorObjectWithDefault>();

            Assert.IsNotNull(obj);
        }

        #endregion

        #region get properties with attribute tests

        [Test]
        public void ReturnsPropertiesWithAttributeDecoration()
        {
            var testObject = new AttributeDecoratedObject();
            var properties = testObject.GetPropertiesWithAttribute<AttributeDecoratedObject, RequiredAttribute>();

            Assert.IsNotNull(properties);

            var propertyList = properties.ToList();

            Assert.IsNotEmpty(propertyList);
            Assert.AreEqual(2, propertyList.Count());
        }

        [Test]
        public void ReturnsEmptyListWhenRequestedAttributeIsNotPresent()
        {
            var testObject = new NonAttributeDecoratedObject();

            var properties = testObject.GetPropertiesWithAttribute<NonAttributeDecoratedObject, RequiredAttribute>();

            Assert.IsNotNull(properties);
            Assert.IsEmpty(properties);
        }

        #endregion

        #region test objects

        public class ParameterlessConstructorObject
        {
        }

        public class ParameteredConstructorObject
        {
            public ParameteredConstructorObject(string param)
            {
                
            }
        }

        public class DualConstructorObjectWithDefault
        {
            public DualConstructorObjectWithDefault()
            {
            }

            public DualConstructorObjectWithDefault(object param)
            {
            }
        }

        public class AttributeDecoratedObject
        {
            [Required]
            public string Prop1 { get; set; }

            public string Prop2 { get; set; }

            [Required]
            public string Prop3 { get; set; }
        }

        public class NonAttributeDecoratedObject
        {
            public string Prop1 { get; set; }

            public string Prop2 { get; set; }

            public string Prop3 { get; set; }
        }

        #endregion
    }
}
