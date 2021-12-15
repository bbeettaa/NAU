using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;

namespace BLL.Tests
{
    [TestClass]
    public class SortingTest
    {
        DALWorckWithDataBases.EntityContext context = new DALWorckWithDataBases.EntityContext();
        Sorting sort = new Sorting(new DALWorckWithDataBases.EntityContext());
        [TestMethod]
        public void SortName_ASC()
        {
            List<Object> product = new List<object> {
                            new Product {Name = "D" },
                            new Product {Name = "C"},
                            new Product {Name = "B"},
                            new Product {Name = "A"}};
            sort.sortingOreder = Sorting.SortingOreder.ASC;

            List<Object> actualList = sort.SortName(product);


            Assert.AreEqual("A", context.GetObjName(actualList[0]));
            Assert.AreEqual("B", context.GetObjName(actualList[1]));
            Assert.AreEqual("C", context.GetObjName(actualList[2]));
            Assert.AreEqual("D", context.GetObjName(actualList[3]));
        }


        [TestMethod]
        public void SortName_DESC()
        {
            List<Object> product = new List<object> {
                            new Product {Name = "C" },
                            new Product {Name = "D"},
                            new Product {Name = "A"},
                            new Product {Name = "B"}};
            sort.sortingOreder = Sorting.SortingOreder.DESC;

            List<Object> actualList = sort.SortName(product);


            Assert.AreEqual("D", context.GetObjName(actualList[0]));
            Assert.AreEqual("C", context.GetObjName(actualList[1]));
            Assert.AreEqual("B", context.GetObjName(actualList[2]));
            Assert.AreEqual("A", context.GetObjName(actualList[3]));
        }



        [TestMethod]
        public void SortBrand_ASC()
        {
            List<Object> product = new List<object> {
                            new Product {Brand = "C" },
                            new Product {Brand = "D"},
                            new Product {Brand = "A"},
                            new Product {Brand = "B"}};
            sort.sortingOreder = Sorting.SortingOreder.ASC;

            List<Object> actualList = sort.SortBrand(product);


            Assert.AreEqual("A",context.GetObjBrand( actualList[0]));
            Assert.AreEqual("B", context.GetObjBrand(actualList[1]));
            Assert.AreEqual("C", context.GetObjBrand(actualList[2]));
            Assert.AreEqual("D", context.GetObjBrand(actualList[3]));
        }

        [TestMethod]
        public void SortBrand_DESC()
        {
            List<Object> product = new List<object> {
                            new Product {Brand = "C" },
                            new Product {Brand = "D"},
                            new Product {Brand = "A"},
                            new Product {Brand = "B"}};
            sort.sortingOreder = Sorting.SortingOreder.DESC;

            List<Object> actualList = sort.SortBrand(product);


            Assert.AreEqual("D", context.GetObjBrand(actualList[0]));
            Assert.AreEqual("C", context.GetObjBrand(actualList[1]));
            Assert.AreEqual("B", context.GetObjBrand(actualList[2]));
            Assert.AreEqual("A", context.GetObjBrand(actualList[3]));
        }


        [TestMethod]
        public void SortPrice_ASC()
        {
            List<Object> product = new List<object> {
                            new Product {Price = "5.00" },
                            new Product {Price = "4.00"},
                            new Product {Price = "10.00"},
                            new Product {Price = "0.10"}};
            sort.sortingOreder = Sorting.SortingOreder.ASC;

            List<Object> actualList = sort.SortPrice(product);


            Assert.AreEqual("0.10", context.GetObjPrice(actualList[0]));
            Assert.AreEqual("4.00", context.GetObjPrice(actualList[1]));
            Assert.AreEqual("5.00", context.GetObjPrice(actualList[2]));
            Assert.AreEqual("10.00", context.GetObjPrice(actualList[3]));
        }
        [TestMethod]
        public void SortLastNameSupp_ASC()
        {
            List<Object> product = new List<object> {
                            new Supplier {LastName = "B" },
                            new Supplier {LastName = "A"},
                            new Supplier {LastName = "D"},
                            new Supplier {LastName = "C"}};
            sort.sortingOreder = Sorting.SortingOreder.ASC;

            List<Object> actualList = sort.SortLastNameSupp(product);


            Assert.AreEqual("A", context.GetObjLastName(actualList[0]));
            Assert.AreEqual("B", context.GetObjLastName(actualList[1]));
            Assert.AreEqual("C", context.GetObjLastName(actualList[2]));
            Assert.AreEqual("D", context.GetObjLastName(actualList[3]));
        }
    }
}
