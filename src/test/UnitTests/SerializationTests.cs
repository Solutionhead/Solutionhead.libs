using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;

namespace Solutionhead.Libs.Tests
{
    #region Test classes.

    public class Parent
    {
        public int parentMember0 { get; set; }
        public string parentMember1 { get; set; }
        public Child parentMember2 { get; set; }
    }

    public class ParentWithChildren : Parent
    {
        public List<Child> parentMembers3 { get; set; }
    }

    public class Child
    {
        public int childMember0 { get; set; }
        public string childMember1 { get; set; }
    }

    #endregion

    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void Serialize_and_Deserialize_parent_without_child()
        {
            XmlSerializer x = new XmlSerializer(typeof(Parent));

            Parent parentObject = new Parent();
            Parent parentCopy;
            parentObject.parentMember1 = "parentMember1";

            var myXML = new System.IO.MemoryStream();
            x.Serialize(myXML, parentObject);
            myXML.Position = 0;
            object obj = x.Deserialize(myXML);
            parentCopy = (Parent)obj;
            x.Serialize(Console.Out, parentCopy);

            //Assert.AreEqual(parentObject, parentCopy);
        }

        [TestMethod]
        public void Serialize_and_Deserialize_parent_without_children()
        {
            XmlSerializer x = new XmlSerializer(typeof(ParentWithChildren));

            ParentWithChildren parentObject = new ParentWithChildren();
            parentObject.parentMember1 = "parentMember1";

            x.Serialize(Console.Out, parentObject);
        }

        [TestMethod]
        public void Serialize_and_Deserialize_parent_with_child()
        {
            XmlSerializer x = new XmlSerializer(typeof(Parent));

            Parent parentObject = new Parent();
            parentObject.parentMember1 = "parentMember1";
            parentObject.parentMember2 = new Child();

            x.Serialize(Console.Out, parentObject);
        }

        [TestMethod]
        public void Serialize_and_Deserialize_parent_with_children()
        {
            XmlSerializer x = new XmlSerializer(typeof(ParentWithChildren));

            ParentWithChildren parentObject = new ParentWithChildren();
            Parent parentCopy;
            parentObject.parentMember1 = "parentMember1";
            parentObject.parentMembers3 = new List<Child>();
            parentObject.parentMembers3.Add(new Child());
            parentObject.parentMembers3.Add(new Child());
            parentObject.parentMembers3.Add(new Child());
            parentObject.parentMembers3[0].childMember1 = "You haven't met me...";
            parentObject.parentMembers3[2].childMember1 = "I AM THE ONLY SON!";

            var myXML = new System.IO.MemoryStream();
            x.Serialize(myXML, parentObject);
            myXML.Position = 0;
            object obj = x.Deserialize(myXML);
            parentCopy = (Parent)obj;
            x.Serialize(Console.Out, parentCopy);

            //x.Serialize(Console.Out, parentObject);
        }
    }
}
