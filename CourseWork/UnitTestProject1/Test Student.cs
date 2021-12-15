using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DALL.Classes
{

    [TestClass]
    public class Test_Product
    {

        [TestMethod]
        public void ChangeProperties_True()
        {
            Product product = new Product() {Name = "F" };

            Assert.IsTrue(product.ChangeProperties(0, "First"));
            Assert.IsTrue(product.ChangeProperties(1, "Last"));
            Assert.IsTrue(product.ChangeProperties(3, "1000"));


            Assert.IsTrue(product.ChangeProperties(2, "1"));
            Assert.IsTrue(product.ChangeProperties(2, "1.0"));
            Assert.IsTrue(product.ChangeProperties(2, ".01"));
            Assert.IsTrue(product.ChangeProperties(2, "012"));
            Assert.IsTrue(product.ChangeProperties(2, "0.000"));

            Assert.IsTrue( product.ChangeProperties(3, ""));

            Assert.IsTrue(product.ChangeProperties(3, "01"));
            Assert.IsTrue(product.ChangeProperties(3, "012345"));


            Assert.AreEqual("First", product.Name);
            Assert.AreEqual("Last", product.Brand);
        }

        [TestMethod]
        public void ChangeProperties_False()
        {
            Product product = new Product();
            product.Brand = "1";


            Assert.IsFalse(product.ChangeProperties(1, "ABCDEFGHIJKLMNOPQRSTUV"));
            
            Assert.IsFalse(product.ChangeProperties(6, "ABC"));
            Assert.IsFalse(product.ChangeProperties(7, "ABCDEFGHIJKLMNOPQRS>=16"));
            Assert.IsFalse(product.ChangeProperties(5, "10A01-101"));
            Assert.IsFalse(product.ChangeProperties(8, "ABC.DE"));

            Assert.IsFalse(product.ChangeProperties(3, "123456789123456789"));

        }

        [TestMethod]
        public void GetObjNameProp()
        {
            Product product = new Product();

            List<String> methods = product.GetObjNameProp().ToList();

            Assert.AreEqual("Name", methods[1]);
            Assert.AreEqual("Brand", methods[2]);
        }
        [TestMethod]
        public void GetObjValueProp()
        {
            Product product = new Product() {Name="name",Brand="Band",Price="price",InStock="Stock" };


            List<String> methods = product.GetObjValueProp().ToList();

            Assert.AreEqual("name", methods[0]);
        }
        [TestMethod]
        public void IsFindInfo_False()
        {
            Product product = new Product();
            product.Name = "F";

            bool isFindInfo = product.IsFindInfo("123");

            Assert.IsFalse(isFindInfo);
        }
        [TestMethod]
        public void IsFindInfo_True()
        {
            Product product = new Product();
            product.Name = "123";

            bool isFindInfo = product.IsFindInfo("123");

            Assert.IsTrue(isFindInfo);
        }


       
    }
}
