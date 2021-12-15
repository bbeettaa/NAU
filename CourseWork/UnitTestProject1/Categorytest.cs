using BLL;
using DALWorckWithDataBases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;

namespace BLL.Tests
{


    [TestClass]
    public class Categorytest
    {
        Category category = new Category(new NewEntityContextWithoutObj());


        [TestMethod]
        public void AddCategory()
        {
            category = new Category(new NewEntityContextWithoutObj());

            category.AddCategory("F");
            category.AddCategory("F");
            category.AddCategory("D");
            category.AddCategory("");

            Assert.AreEqual(6, category.GetObjsCategories().Count);
        }

        [TestMethod]
        public void DellSimilarCategory()
        {
            category = new Category(new NewEntityContextWithoutObj());

            category.AddCategory("F");
            category.AddCategory("F");
            category.AddCategory("D");
            category.DelSimilarCategories();

            Assert.AreEqual(6,category.GetObjsCategories().Count);
        }
        [TestMethod]
        public void DellSimilarCategory_NothingToDell()
        {
            category = new Category(new NewEntityContextWithoutObj());

            category.AddCategory("F");
            category.AddCategory("R");
            category.AddCategory("D");
            category.DelSimilarCategories();

            Assert.AreEqual(8, category.GetObjsCategories().Count);
        }
        [TestMethod]
        public void GetGroupsOfObj()
        {
            category = new Category(new NewEntityContextWithoutObj());
             List<Object> product = new List<object> {
                            new Product {GroupCollection = "A" },
                            new Product {GroupCollection = "B"},
                            new Product {GroupCollection = "C" },
                            new Product {GroupCollection = "D"}};
            category.SetObjsCategories(product);

            List<String> list = category.GetGroupsOfObj(product);

            Assert.AreEqual(4, list.Count);
        }
        [TestMethod]
        public void GetGroupsOfObj_SimilarGroups()
        {
            category = new Category(new NewEntityContextWithoutObj());
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "A" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "B"}};
            category.SetObjsCategories(product);

            List<String> list = category.GetGroupsOfObj(product);

            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void DeleteCategory()
        {
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "A" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "B"}};
            category.SetObjsCategories(product);
            category.DeleteCategory("A",product);

            List<String> list = category.GetGroupsOfObj(product);

            Assert.AreEqual(4, list.Count);


        }
        [TestMethod]
        public void DeleteCategory_Default()
        {
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "Default" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "C"}};
            category.SetObjsCategories(product);

            category.DeleteCategory("Default", product);

            List<object> list = category.GetObjsCategories();
            Assert.AreEqual(8, list.Count);
        }

        [TestMethod]
        public void GetObjsCategories()
        {
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "Default" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "C"}};
            category.SetObjsCategories(product);


            List<object> list = category.GetObjsCategories();

            Assert.AreEqual(8, list.Count);
        }
        [TestMethod]
        public void GetObjsCategories_Similar()
        {
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "A" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "B"}};
            category.SetObjsCategories(product);


            List<object> list= category.GetObjsCategories();

            Assert.AreEqual(6, list.Count);
        }
        [TestMethod]
        public void RenameGroupOfCurrentObjectAndDellOldGroup()
        {
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "A" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "B"}};

            category.RenameGroupOfCurrentObjectAndDellOldGroup("BC",product,0);


            category.SetObjsCategories(product);
            List<object> list = category.GetObjsCategories();
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void RenameGroupOfCurrentObjectAndDellOldGroup_WrongIndex_1()
        {
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "A" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "B"}};

            category.RenameGroupOfCurrentObjectAndDellOldGroup("BC", product, 22);


            category.SetObjsCategories(product);
            List<object> list = category.GetObjsCategories();
            Assert.AreEqual(6, list.Count);
        }
        [TestMethod]
        public void RenameGroupOfCurrentObjectAndDellOldGroup_WrongIndex_2()
        {
            List<Object> product = new List<object> {
                            new Product {GroupCollection = "A" },
                            new Product {GroupCollection = "A"},
                            new Product {GroupCollection = "B" },
                            new Product {GroupCollection = "B"}};

            category.RenameGroupOfCurrentObjectAndDellOldGroup("BC", product, -1);


            category.SetObjsCategories(product);
            List<object> list = category.GetObjsCategories();
            Assert.AreEqual(6, list.Count);
        }

    }
}
