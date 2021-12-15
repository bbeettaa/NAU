using DAL_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;

namespace DALL.Classes
{
    [TestClass]
    public class Settings
    {
        [TestMethod]
        public void ChangeProperties_False()
        {
            DAL_Classes.Settings set = new DAL_Classes.Settings();


            Assert.IsTrue(set.ChangeProperties(0, "Json"));
            Assert.IsTrue(set.ChangeProperties(1, "XML"));
            Assert.IsTrue(set.ChangeProperties(2, "Binary"));
            Assert.IsTrue(set.ChangeProperties(3, "Custom"));


            Assert.IsFalse(set.ChangeProperties(0, "123456789012345678"));
            Assert.IsFalse(set.ChangeProperties(1, "123456789012345678"));
            Assert.IsFalse(set.ChangeProperties(2, "123456789012345678"));
            Assert.IsFalse(set.ChangeProperties(3, "123456789012345678"));

            Assert.IsFalse(set.ChangeProperties(45, "123456789012345678"));
        }

        [TestMethod]
        public void RebuildSettings()
        {
            DAL_Classes.Settings set = new DAL_Classes.Settings();
            set.CurrentFileName = null;

            set.RebuildSettings();


        }
    }
}
