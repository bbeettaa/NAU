using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALWorckWithDataBases;
using ProgramClasses;

namespace BLL.Tests
{
    [TestClass]
    public class EntityService_Tests
    {

        EntityService service = new EntityService();

        [TestMethod()]
        public void AddCategory_Test_One_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);


            service.AddCategory("First");


            List<Object> objs = (List<object>)privateObject.GetField("categories");
            Assert.AreEqual("First", objs[0]);
        }


        [TestMethod()]
        public void AddCategory_Test_No_One_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);

            service.AddCategory("");

            List<Object> objs = (List<object>)privateObject.GetField("categories");
            Assert.AreEqual(0, objs.Count);

        }

        [TestMethod()]
        public void AddCategory_Test_Two_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);


            service.AddCategory("First");
            service.AddCategory("Second");

            List<Object> objs = (List<object>)privateObject.GetField("categories");

            Assert.AreEqual("First", objs[0]);
            Assert.AreEqual(true, objs[1]);
            Assert.AreEqual("Second", objs[2]);
            Assert.AreEqual(true, objs[3]);
        }

        [TestMethod()]
        public void AppendObjectInDatabase_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ChangeShowCatigories_Test()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ChangeShowCatigories_Test_Different_Categories()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "2";
            privateObject.SetField("objList", new List<object> { st, st1 });
            service.SetObjsCategories();

            service.ChangeShowCatigories("1", false);
            List<Object> objs = (List<object>)privateObject.GetField("categories");

            Assert.AreEqual(false, objs[1]);
            Assert.AreEqual(true, objs[3]);

        }
        [TestMethod]
        public void ChangeShowCatigories_Test_NoOne_CAtegories()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "2";
            privateObject.SetField("objList", new List<object> { st, st1 });
            service.SetObjsCategories();

            service.ChangeShowCatigories("3", false);
            List<Object> objs = (List<object>)privateObject.GetField("categories");

            Assert.AreEqual(true, objs[1]);
            Assert.AreEqual(true, objs[3]);
        }

        [TestMethod()]
        public void CheckIndexOfChoosenObj_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CountPercentOfFirstCourseArrivalsStudent_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteCategory_Test_NoOne_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();

            service.SetObjsCategories();

            service.DeleteCategory("1");

            Assert.AreEqual(0, service.GetObjsCategories().Count);
        }

        [TestMethod()]
        public void DeleteCategory_Test_One_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            privateObject.SetField("objList", new List<object> { st });
            service.SetObjsCategories();

            service.DeleteCategory("1");

            Assert.AreEqual(0, service.GetObjsCategories().Count);
        }

        [TestMethod()]
        public void DeleteCategory_Test_Two_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "2";
            privateObject.SetField("objList", new List<object> { st, st1 });
            service.SetObjsCategories();

            service.DeleteCategory("2");


            Assert.AreEqual(2, service.GetObjsCategories().Count);
        }

        [TestMethod()]
        public void DeleteObj_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteCategory_Test_Similar_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "1";
            privateObject.SetField("objList", new List<object> { st, st1 });
            service.SetObjsCategories();

            service.DeleteCategory("1");

            Assert.AreEqual(0, service.GetObjsCategories().Count);
        }

        [TestMethod()]
        public void DelSimilarCategories_Test_Different_Categories()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "2";
            privateObject.SetField("objList", new List<object> { st, st1 });
            service.SetObjsCategories();

            service.DelSimilarCategories();
            List<Object> objs = (List<object>)privateObject.GetField("categories");

            Assert.AreEqual("1", objs[0]);
            Assert.AreEqual("2", objs[2]);
        }

        [TestMethod()]
        public void DelSimilarCategories_Test_One_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            privateObject.SetField("objList", new List<object> { st });
            service.SetObjsCategories();

            service.DelSimilarCategories();
            List<Object> objs = (List<object>)privateObject.GetField("categories");

            Assert.AreEqual("1", objs[0]);
        }

        [TestMethod()]
        public void DelSimilarCategories_Test_No_One_Category()
        {
            PrivateObject privateObject = new PrivateObject(service);
            privateObject.SetField("objList", new List<object> { });
            service.SetObjsCategories();

            service.DelSimilarCategories();

            Assert.AreEqual(0, service.GetObjsCategories().Count);
        }

        [TestMethod()]
        public void DelSimilarCategories_Test_Similar_Categories()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "1";
            privateObject.SetField("objList", new List<object> { st, st1 });
            service.SetObjsCategories();

            service.DelSimilarCategories();
            List<Object> objs = (List<object>)privateObject.GetField("categories");

            Assert.AreEqual("1", objs[0]);
            Assert.AreEqual(2, objs.Count);
        }

        [TestMethod()]
        public void Deserialize_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EntityService_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EntityService_Test1()
        {
            Assert.Fail();
        }


        [TestMethod()]
        public void WorckWithMethods_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindObjects_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllObjValueProp_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAssemblyTypes_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetGroupsOfObj_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMethodsInfo_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetObjNameProps_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetObjNames_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetObjsCategories_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetObjsCategories_Test1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetObjValueProp_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTableOfObjectAndGroup_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HostelArrivalStud_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InputInfoAndSaveObj_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RenameGroupOfCurrentObject_Test()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "1";
            Student st2 = new Student();
            st2.GroupCollection = "2";
            Student st3 = new Student();
            st3.GroupCollection = "2";
            privateObject.SetField("objList", new List<object> { st, st1, st2, st3 });
            service.SetObjsCategories();



            service. IndexOfChosenObj=1;
            service.RenameGroupOfCurrentObject("New");
            List<Object> objs = (List<object>)privateObject.GetField("categories");


            Assert.AreEqual("New", objs[0]);
            Assert.AreEqual("2", objs[2]);
        }

        [TestMethod()]
        public void RenameGroupOfCurrentObject_Test_Index_More_Then_ListCount()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "1";
            Student st2 = new Student();
            st2.GroupCollection = "2";
            Student st3 = new Student();
            st3.GroupCollection = "2";
            privateObject.SetField("objList", new List<object> { st, st1, st2, st3 });
            service.SetObjsCategories();



            service.IndexOfChosenObj = 5;
            service.RenameGroupOfCurrentObject("New");
            List<Object> objs = (List<object>)privateObject.GetField("categories");


            Assert.AreEqual("1", objs[0]);
            Assert.AreEqual("2", objs[2]);
        }

        [TestMethod()]
        public void RenameGroupOfCurrentObject_Test_Index_Less_Then_0()
        {
            PrivateObject privateObject = new PrivateObject(service);
            Student st = new Student();
            st.GroupCollection = "1";
            Student st1 = new Student();
            st1.GroupCollection = "1";
            Student st2 = new Student();
            st2.GroupCollection = "2";
            Student st3 = new Student();
            st3.GroupCollection = "2";
            privateObject.SetField("objList", new List<object> { st, st1, st2, st3 });
            service.SetObjsCategories();



            service.IndexOfChosenObj = -1;
            service.RenameGroupOfCurrentObject("New");
            List<Object> objs = (List<object>)privateObject.GetField("categories");


            Assert.AreEqual("1", objs[0]);
            Assert.AreEqual("2", objs[2]);
        }

        [TestMethod()]
        public void SaveObjList_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetGroupToCurrentObject_andSave_Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetObjNames_Test()
        {
            Assert.Fail();
        }

       
    }
}