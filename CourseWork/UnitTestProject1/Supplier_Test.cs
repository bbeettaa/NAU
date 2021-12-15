using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;

namespace DALL.Classes
{
    [TestClass]
    public class Supplier_Test
    {

        [TestMethod]
        public void ChangeProperties_True()
        {
            Supplier supplier = new Supplier() { Name = "F" };

            Assert.IsTrue(supplier.ChangeProperties(0, "First"));
            Assert.IsTrue(supplier.ChangeProperties(1, "Last"));
          

            Assert.AreEqual("First", supplier.Name);
            Assert.AreEqual("Last", supplier.LastName);
        }

        [TestMethod]
        public void ChangeProperties_False()
        {
            Supplier supplier = new Supplier();

            Assert.IsFalse(supplier.ChangeProperties(1, "ABCDEFGHIJKLMNOPQRSTUV"));
            Assert.IsFalse(supplier.ChangeProperties(6, "ABC"));
            Assert.IsFalse(supplier.ChangeProperties(7, "ABCDEFGHIJKLMNOPQRS>=16"));
            Assert.IsFalse(supplier.ChangeProperties(5, "10A01-101"));
            Assert.IsFalse(supplier.ChangeProperties(8, "ABC.DE"));
            Assert.IsFalse(supplier.ChangeProperties(3, "123456789123456789"));
        }

        [TestMethod]
        public void GetObjNameProp()
        {
            Supplier supplier = new Supplier();

            List<String> methods = supplier.GetObjNameProp();

            Assert.AreEqual("Name", methods[1]);
            Assert.AreEqual("LastName", methods[2]);
        }
        [TestMethod]
        public void GetObjValueProp()
        {
            Supplier supplier = new Supplier() { Name = "name", LastName= "Band" };

            List<String> methods = supplier.GetObjValueProp();

            Assert.AreEqual("name", methods[0]);
            Assert.AreEqual("Band", methods[1]);
        }
        [TestMethod]
        public void IsFindInfo_False()
        {
            Supplier supplier = new Supplier();
            supplier.Name = "F";

            bool isFindInfo = supplier.IsFindInfo("123");

            Assert.IsFalse(isFindInfo);
        }
        [TestMethod]
        public void IsFindInfo_True()
        {
            Supplier supplier = new Supplier();
            supplier.Name = "123";

            bool isFindInfo = supplier.IsFindInfo("123");

            Assert.IsTrue(isFindInfo);
        }

    }
}
