using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solutionhead.Data.EntityFramework;

namespace Solutionhead.Libs.Tests
{
    #region Classes with different key possibilities.

    public class ClassWithNothing
    {
    }

    public class ClassWithIDProperty
    {
        public int id { get; set; }
    }

    public class ClassWithClassNameIDProperty
    {
        public string ClassWithClassNameIDPropertyID { get; set; }
    }

    public class ClassWithOneKey
    {
        public char misc0 { get; set; }
        public int misc1 { get; set; }

        [Key]
        public int key { get; set; }
    }

    public class ClassWithMultipleKeysOutOfOrder
    {
        [Key]
        [Column(Order = 1)]
        public int key1 { get; set; }

        public char misc0 { get; set; }
        public int misc1 { get; set; }

        [Key]
        [Column(Order = 0)]
        public string key0 { get; set; }

        [Key]
        [Column(Order = 3)]
        public string key3 { get; set; }

        public char misc2 { get; set; }
        public int misc3 { get; set; }

        [Key]
        [Column(Order = 2)]
        public int key2 { get; set; }

        public char misc4 { get; set; }
        public int misc5 { get; set; }
    }

    public class ClassWithOneNavigationProperty
    {
        public int ID { get; set; }

        public ClassWithOneKey NavigationProperty { get; set; }
    }

    #endregion

    [TestClass]
    public class ObjectKeyExtractorTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region ObjectPrimaryKeyExtractor tests

        [TestMethod]
        public void ObjectKeyExtractor_ExtractKey_Returns_a_string_made_up_of_keys_sorted_by_column_order()
        {
            var testClass = new ClassWithMultipleKeysOutOfOrder();
            testClass.key0 = "key0";
            testClass.key1 = 1;
            testClass.key2 = 2;
            testClass.key3 = "key3";
            var actual = ObjectKeyExtractor<ClassWithMultipleKeysOutOfOrder>.ExtractKey(testClass);
            var expected = "key0:1:2:key3";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ObjectKeyExtractor_ExtractKey_Returns_a_string_made_up_of_a_single_key()
        {
            var testClass = new ClassWithOneKey();
            testClass.key = 1234;
            var actual = ObjectKeyExtractor<ClassWithOneKey>.ExtractKey(testClass);
            var expected = "1234";            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ObjectKeyExtractor_ExtractKey_Returns_an_empty_string_if_class_has_no_members()
        {
            var testClass = new ClassWithNothing();
            var actual = ObjectKeyExtractor<ClassWithNothing>.ExtractKey(testClass);
            var expected = "";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ObjectKeyExtractor_ExtractKey_Returns_a_string_following_the_ID_convention()
        {
            var testClass = new ClassWithIDProperty();
            testClass.id = 3214;
            var actual = ObjectKeyExtractor<ClassWithIDProperty>.ExtractKey(testClass);
            var expected = "3214";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ObjectKeyExtractor_ExtractKey_Returns_a_string_following_the_classNameID_convention()
        {
            var testClass = new ClassWithClassNameIDProperty();
            testClass.ClassWithClassNameIDPropertyID = "Oh no!";
            var actual = ObjectKeyExtractor<ClassWithClassNameIDProperty>.ExtractKey(testClass);
            var expected = "Oh no!";
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }

    /*[TestClass]
    public class ObjectGraphArchiverTest
    {
        [TestMethod]
        public void Test()
        {
            var archiver = new EFArchiveWriter<ClassWithOneNavigationProperty>();

            var objectToArchive = new ClassWithOneNavigationProperty{
                ID = 1,
                NavigationProperty = new ClassWithOneKey
                {
                    key = 1,
                    misc0 = 'x',
                    misc1 = 10
                }
            };

            archiver.WriteToArchive<ClassWithOneKey>(objectToArchive, o => o.NavigationProperty);
        }
    }*/
}
