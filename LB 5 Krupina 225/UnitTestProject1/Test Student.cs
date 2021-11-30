using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    /// <summary>
    /// Сводное описание для Test_Student
    /// </summary>
    [TestClass]
    public class Test_Student
    {

        [TestMethod]
        public void ChangeProperties_True()
        {
            Student student = new Student();
            student.FirstName = "F";

            Assert.IsTrue( student.ChangeProperties(0, "F"));
            Assert.IsTrue(student.ChangeProperties(1, "L"));
            Assert.IsTrue(student.ChangeProperties(2, "K"));
            Assert.IsTrue(student.ChangeProperties(3, "01110010"));
            Assert.IsTrue(student.ChangeProperties(4, "01110-010"));
            Assert.IsTrue(student.ChangeProperties(5, "10001-101"));
            Assert.IsTrue(student.ChangeProperties(6, "5"));
            Assert.IsTrue(student.ChangeProperties(7, "ACR"));
            Assert.IsTrue(student.ChangeProperties(8, "666.13"));



            Assert.AreEqual("F", student.FirstName);
            Assert.AreEqual("L", student.LastName);
            Assert.AreEqual("K", student.ArivalCity);
            Assert.AreEqual("01110-010", student.PassportNo);
            Assert.AreEqual("01110010", student.PassportSer);
            Assert.AreEqual("10001-101", student.StudentID);
            Assert.AreEqual("5", student.Course);
            Assert.AreEqual("ACR", student.FavoriteDance);
            Assert.AreEqual("666.13", student.Hostel);
        }

        [TestMethod]
        public void ChangeProperties_False()
        {
            Student student = new Student();
            student.Course = "1";


            Assert.IsFalse(student.ChangeProperties(6, "ABC"));
            Assert.IsFalse(student.ChangeProperties(7, "ABCDEFGHIJKLMNOPQRS>=16"));
            Assert.IsFalse(student.ChangeProperties(5, "10A01-101"));
            Assert.IsFalse(student.ChangeProperties(8, "ABC.DE"));

        }

        [TestMethod]
        public void GetMethodsInfo()
        {
            Student student = new Student();

            List<String> methods = student.GetMethodsInfo().ToList();

            Assert.AreEqual("Transfer_Object_To_The_Next_Сourse", methods[0]);
        }
        [TestMethod]
        public void GetObjNameProp()
        {
            Student student = new Student();

            List<String> methods = student.GetObjNameProp().ToList();

            Assert.AreEqual("Student", methods[0]);
            Assert.AreEqual("FirstName", methods[1]);
        }
        [TestMethod]
        public void GetObjValueProp()
        {
            Student student = new Student();
            student.FirstName = "F";

            List<String> methods = student.GetObjValueProp().ToList();

            Assert.AreEqual("F", methods[0]);
        }
        [TestMethod]
        public void IsFindInfo_False()
        {
            Student student = new Student();
            student.FirstName = "F";

            bool isFindInfo = student.IsFindInfo("123");

            Assert.IsFalse(isFindInfo);
        }
        [TestMethod]
        public void IsFindInfo_True()
        {
            Student student = new Student();
            student.FirstName = "123";

            bool isFindInfo = student.IsFindInfo("123");

            Assert.IsTrue(isFindInfo);
        }
        [TestMethod]
        public void Operation_Object_Dance()
        {
            Student student = new Student();

            string beforeDance = student.FavoriteDance;
            student.Operation_Object_Dance();
            string afterDance = student.FavoriteDance;

            Assert.AreNotEqual(beforeDance, afterDance);
        }
        [TestMethod]
        public void Transfer_Object_To_The_Next_Сourse()
        {
            Student student = new Student();
            student.Course = "1";

            student.Transfer_Object_To_The_Next_Сourse();

            Assert.AreEqual("2", student.Course);
        }
    }
}
