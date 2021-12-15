using DAL_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;

namespace DALL.Classes
{

    [TestClass]
    public class AbstractClass_TEst
    {
        [TestMethod]
        public void ChangeProperty()
        {
            AbstractClass ab = new Product();

            bool result = ab.ChangeProperties(0, "1234567891237878456");

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void ChangeProperty_FirstName()
        {
            AbstractClass ab = new Product();

            bool result = ab.ChangeProperties(0, "1234567891237878456");
            bool result1 = ab.ChangeProperties(0, "123456789123456");
            bool result2 = ab.ChangeProperties(0, "123456789123456");

            Assert.IsFalse(result);
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void HeadingOfObject()
        {
            AbstractClass ab = new Product() {Name = "Name",Brand = "Undefined" };

            String list = ab.HeadingOfObject();

            Assert.AreEqual("Name Undefined", list);
        }
    }
}
