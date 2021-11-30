using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BLL;
using ProgramClasses;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Collections;

namespace UnitTestProject1
{
    [TestClass]
    public class TesT_BLL
    {
        EntityService service = new EntityService(new DALWorckWithDataBases.EntityContext());



        [TestMethod]
        public void Test_ProvideEntityService()
        {
            service = new EntityService(new DALWorckWithDataBases.EntityContext());
            File.Delete("C:\\Users\\bedu_s_bashkoy\\source\\repos\\LB 5 Krupina 225 t\\UnitTestProject1\\bin\\JsonDataBase.json");

            service.AppendObjectInDatabase(0);
            service.AppendObjectInDatabase(1);
            service.AppendObjectInDatabase(2);

            Assert.AreEqual(service.objList[0].GetType(), typeof(Student));
            Assert.AreEqual(service.objList[1].GetType(), typeof(Acrobat));
            Assert.AreEqual(service.objList[2].GetType(), typeof(TaxiDriver));
        }

        [TestMethod]
        public void Test_AppendObjectInDatabase()
        {
            service = new EntityService();
            File.Delete("C:\\Users\\bedu_s_bashkoy\\source\\repos\\LB 5 Krupina 225 t\\UnitTestProject1\\bin\\JsonDataBase.json");

            service.AppendObjectInDatabase(0);
            service.AppendObjectInDatabase(1);
            service.AppendObjectInDatabase(2);

            Assert.AreEqual(service.objList[0].GetType(), typeof(Student));
            Assert.AreEqual(service.objList[1].GetType(), typeof(Acrobat));
            Assert.AreEqual(service.objList[2].GetType(), typeof(TaxiDriver));
        }
        [TestMethod]
        public void CheckIndexOfChoosenObjs_indexLessThenZero()
        {
            service.objList = new List<object>();

            service.objList.Add(new object());
            service.objList.Add(new object());
            service.IndexOfChosenObj = -1;

            service.CheckIndexOfChoosenObj();

            Assert.AreEqual(0, service.IndexOfChosenObj);
        }
        [TestMethod]
        public void CheckIndexOfChoosenObjs_objLengthZero()
        {
            service.objList = new List<object>();
            service.CheckIndexOfChoosenObj();
            Assert.AreEqual(0, service.IndexOfChosenObj);
        }
        [TestMethod]
        public void CheckIndexOfChoosenObjs_indexMoreThenMaxElement()
        {
            service.objList = new List<object>();
            service.objList.Add(new object());
            service.objList.Add(new object());

            service.IndexOfChosenObj = 100;
            service.CheckIndexOfChoosenObj();

            Assert.AreEqual(1, service.IndexOfChosenObj);
        }

        [TestMethod]
        public void Test_CountPercentOfFirstCourseArrivalsStudent()
        {
            service = new EntityService(new DALWorckWithDataBases.EntityContext());
            string expectedMessage = $"Всього студентів на першому курсі: 2\n" +
                $"Студентів на першому курсі, що приїхали: 1\n" +
                $"Відсоток студентів - 50%";
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "Fff";
            stud.Course = "1";
            stud.ArivalCity = "Kyiv";
            Student stud1 = new Student();
            stud1.FirstName = "Lll";
            stud1.Course = "1";
            stud1.ArivalCity = "NoKyiv";
            Student stud2 = new Student();
            stud2.FirstName = "wqe";
            stud2.Course = "2";
            stud2.ArivalCity = "Kyiv";

            service.objList.Add(stud);
            service.objList.Add(stud1);
            service.objList.Add(stud2);


            string percentMessage = service.CountPercentOfFirstCourseArrivalsStudent();

            Assert.AreEqual(expectedMessage, percentMessage);
        }
        [TestMethod]
        public void Test_CountPercentOfFirstCourseArrivalsStudent_NAN()
        {
            service = new EntityService();
            string expectedMessage = $"Всього студентів на першому курсі: 0\n" +
                $"Студентів на першому курсі, що приїхали: 0\n" +
                $"Відсоток студентів - 0%";
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "Fff";
            stud.Course = "2";
            stud.ArivalCity = "Kyiv";
            Student stud1 = new Student();
            stud1.FirstName = "Lll";
            stud1.Course = "2";
            stud1.ArivalCity = "Kyiv";
            Student stud2 = new Student();
            stud2.FirstName = "wqe";
            stud2.Course = "2";
            stud2.ArivalCity = "Kyiv";

            service.objList.Add(stud);
            service.objList.Add(stud1);
            service.objList.Add(stud2);


            string percentMessage = service.CountPercentOfFirstCourseArrivalsStudent();

            Assert.AreEqual(expectedMessage, percentMessage);
        }

        [TestMethod]
        public void DeleteObj()
        {
            service.objList = new List<object>();
            service.objList.Add(new object());
            service.objList.Add(new object());
            service.IndexOfChosenObj = 0;

            service.DeleteObj();

            Assert.AreEqual(1, service.objList.Count);
        }
        [TestMethod]
        public void SaveObjList_GetInstanceOfType()
        {
            List<object> objs = new List<object>();

            Student stud = new Student();
            stud.FirstName = "Fff";
            stud.Course = "1";
            stud.ArivalCity = "Kyiv";
            Student stud1 = new Student();
            stud1.FirstName = "Lll";
            stud1.Course = "1";
            stud1.ArivalCity = "NoKyiv";
            Student stud2 = new Student();
            stud1.FirstName = "wqe";
            stud1.Course = "2";
            stud1.ArivalCity = "Kyiv";
            Acrobat acro1 = new Acrobat();
            Acrobat acro2 = new Acrobat();
            Acrobat acro3 = new Acrobat();
            TaxiDriver driver1 = new TaxiDriver();
            TaxiDriver driver2 = new TaxiDriver();
            TaxiDriver driver3 = new TaxiDriver();

            objs.Add(stud);
            objs.Add(stud1);
            objs.Add(stud2);
            objs.Add(acro1);
            objs.Add(acro2);
            objs.Add(acro3);
            objs.Add(driver1);
            objs.Add(driver2);
            objs.Add(driver3);
            service.objList = objs;

            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<Exception>(() => service.SaveObjList()));
        }

        [TestMethod]
        public void FindObjects_Length()
        {
            List<object> objs = new List<object>();

            Student stud = new Student();
            stud.FirstName = "Fff";
            stud.Course = "1";
            stud.ArivalCity = "Kyiv";
            Student stud1 = new Student();
            stud1.FirstName = "Lll";
            stud1.Course = "1";
            stud1.ArivalCity = "NoKyiv";
            Student stud2 = new Student();
            stud1.FirstName = "wqe";
            stud1.Course = "2";
            stud1.ArivalCity = "Kyiv";
            Acrobat acro1 = new Acrobat();
            Acrobat acro2 = new Acrobat();
            Acrobat acro3 = new Acrobat();
            TaxiDriver driver1 = new TaxiDriver();
            TaxiDriver driver2 = new TaxiDriver();
            TaxiDriver driver3 = new TaxiDriver();

            objs.Add(stud);
            objs.Add(stud1);
            objs.Add(stud2);
            objs.Add(acro1);
            objs.Add(acro2);
            objs.Add(acro3);
            objs.Add(driver1);
            objs.Add(driver2);
            objs.Add(driver3);


            service.Deserialize();


            for (int i = 0; i < service.objList.Count; i++)
                Assert.AreEqual(objs[i].GetType(), service.objList[i].GetType());
        }

        [TestMethod]
        public void FindObjects_CurrentObject()
        {
            service.objList = new List<object>();
            List<object> objs = new List<object>();

            Student expectedStud = new Student();
            expectedStud.FirstName = "Fff";

            Student stud = new Student();
            stud.FirstName = "Fff";
            stud.Course = "1";
            stud.ArivalCity = "Kyiv";
            Student stud1 = new Student();
            stud1.FirstName = "Lll";
            stud1.Course = "1";
            stud1.ArivalCity = "NoKyiv";
            Student stud2 = new Student();
            stud1.FirstName = "wqe";
            stud1.Course = "2";
            stud1.ArivalCity = "Kyiv";

            objs.Add(stud);
            objs.Add(stud1);
            objs.Add(stud2);

            service.entityContext.objList = objs;
            service.SaveObjList();


            objs = service.FindObjects("Fff");

            Assert.AreEqual((expectedStud as Student).FirstName, (objs[0] as Student).FirstName);
        }
        [TestMethod]
        public void FindObjects_AllObjects()
        {
            List<object> objs = new List<object>();

            Student stud = new Student();
            stud.FirstName = "Fff";
            stud.Course = "1";
            stud.ArivalCity = "Kyiv";
            Student stud1 = new Student();
            stud1.FirstName = "Lll";
            stud1.Course = "1";
            stud1.ArivalCity = "NoKyiv";
            Student stud2 = new Student();
            stud1.FirstName = "wqe";
            stud1.Course = "2";
            stud1.ArivalCity = "Kyiv";
            Acrobat acro1 = new Acrobat();
            Acrobat acro2 = new Acrobat();
            Acrobat acro3 = new Acrobat();
            TaxiDriver driver1 = new TaxiDriver();
            TaxiDriver driver2 = new TaxiDriver();
            TaxiDriver driver3 = new TaxiDriver();

            objs.Add(stud);
            objs.Add(stud1);
            objs.Add(stud2);
            objs.Add(driver1);
            objs.Add(driver2);
            objs.Add(driver3);
            objs.Add(acro1);
            objs.Add(acro2);
            objs.Add(acro3);

            service.entityContext.objList = objs;



            service.SaveObjList();
            service.FindObjects("");


            for (int i = 0; i < objs.Count; i++)
                Assert.AreEqual(objs[i].GetType(), service.objList[i].GetType());

        }

        [TestMethod]
        public void WorckWithMethods_invoke_TransferToNextCourse()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "Fff";
            stud.Course = "1";
            service.IndexOfChosenObj = 0;
            service.objList.Add(stud);

            service.WorckWithMethods(1, true);
            stud.Course = "2";

            Assert.AreEqual(stud.Course, (service.objList[0] as Student).Course);
        }
        [TestMethod]
        public void HostelArrivalStud()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.Hostel = "301.12";
            service.objList.Add(stud);

            for (int i = 0; i < 25; i++)
                service.objList.Add(new Student());



            service.HostelArrivalStud();


            Assert.AreEqual("301.12", (service.objList[0] as Student).Hostel);
            Assert.AreEqual("203.01", (service.objList[service.objList.Count - 1] as Student).Hostel);
            for (int i = 0; i < service.objList.Count; i++)
                Assert.AreNotEqual("000.00", (service.objList[i] as Student).Hostel);
        }
        [TestMethod]
        public void RenameGroupOfCurrentObject()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.GroupCollection = "Default";
            Student stud0 = new Student();
            stud0.GroupCollection = "Default";
            Student stud1 = new Student();
            stud1.GroupCollection = "Not Default";
            Student stud2 = new Student();
            stud2.GroupCollection = "Not Default";

            service.objList.Add(stud);
            service.objList.Add(stud0);
            service.objList.Add(stud1);
            service.objList.Add(stud2);

            service.IndexOfChosenObj = 2;
            service.RenameGroupOfCurrentObject("New Group");


            Assert.AreEqual("Default", (service.objList[0] as Student).GroupCollection);
            Assert.AreEqual("Default", (service.objList[1] as Student).GroupCollection);
            Assert.AreEqual("New Group", (service.objList[2] as Student).GroupCollection);
            Assert.AreEqual("New Group", (service.objList[3] as Student).GroupCollection);
        }
        [TestMethod]
        public void SetGroupToCurrentObject_andSave()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.GroupCollection = "Default";
            Student stud0 = new Student();
            stud0.GroupCollection = "Default";
            Student stud1 = new Student();
            stud1.GroupCollection = "Not Default";
            Student stud2 = new Student();
            stud2.GroupCollection = "Not Default";

            service.objList.Add(stud);
            service.objList.Add(stud0);
            service.objList.Add(stud1);
            service.objList.Add(stud2);

            service.IndexOfChosenObj = 1;


            service.SetGroupToCurrentObject_andSave("Not Default");


            Assert.AreEqual("Default", (service.objList[0] as Student).GroupCollection);
            Assert.AreEqual("Not Default", (service.objList[1] as Student).GroupCollection);
            Assert.AreEqual("Not Default", (service.objList[2] as Student).GroupCollection);
            Assert.AreEqual("Not Default", (service.objList[3] as Student).GroupCollection);
        }
        [TestMethod]
        public void GetGroupsOfObj()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.GroupCollection = "Default";
            Student stud0 = new Student();
            stud0.GroupCollection = "Default";
            Student stud1 = new Student();
            stud1.GroupCollection = "Not Default";
            Student stud2 = new Student();
            stud2.GroupCollection = "Not Default";

            service.objList.Add(stud);
            service.objList.Add(stud0);
            service.objList.Add(stud1);
            service.objList.Add(stud2);


            List<String> groups = service.GetGroupsOfObj();



            Assert.AreEqual("Default", groups[0]);
            Assert.AreEqual("Default", groups[1]);
            Assert.AreEqual("Not Default", groups[2]);
            Assert.AreEqual("Not Default", groups[3]);
        }
        [TestMethod]
        public void GetTableOfObjectAndGroup()
        {
            service.objList = new List<object>();
            Hashtable hashtable = new Hashtable();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            Student stud1 = new Student();
            stud1.FirstName = "Second";
            stud1.LastName = "Stud";
            stud1.GroupCollection = "Not Default";

            service.objList.Add(stud);
            service.objList.Add(stud1);

            hashtable.Add(stud, stud.GroupCollection);
            hashtable.Add(stud1, stud1.GroupCollection);



            Hashtable returnedHash = service.GetTableOfObjectAndGroup();



            Assert.AreEqual("Default", hashtable[service.objList[0]]);
            Assert.AreEqual("Not Default", hashtable[service.objList[1]]);
        }

        [TestMethod]
        public void GetObjNameProps()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = 0;
            service.objList.Add(stud);


            List<String> names = service.GetObjNameProps();


            Assert.AreEqual("FirstName", names[1]);
            Assert.AreEqual("LastName", names[2]);
            Assert.AreEqual("ArivalCity", names[3]);
            Assert.AreEqual("Course", names[7]);
        }
        [TestMethod]
        public void GetObjNameProps_negativeIndex()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = -1;
            service.objList.Add(stud);


            List<String> names = service.GetObjNameProps();


            Assert.AreEqual("FirstName", names[1]);
            Assert.AreEqual("LastName", names[2]);
            Assert.AreEqual("ArivalCity", names[3]);
            Assert.AreEqual("Course", names[7]);
        }
        [TestMethod]
        public void GetObjNameProps_indexOutOfRange()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = 5;
            service.objList.Add(stud);


            List<String> names = service.GetObjNameProps();


            Assert.AreEqual("", names[0]);
        }
        [TestMethod]
        public void GetObjValueProp()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = 0;
            service.objList.Add(stud);


            List<String> value = service.GetObjValueProp();


            Assert.AreEqual("First", value[0]);
            Assert.AreEqual("Stud", value[1]);
            Assert.AreEqual("0_0", value[2]);
            Assert.AreEqual("666", value[6]);
        }
        [TestMethod]
        public void GetObjValueProp_indexOutOfRange()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = 5;
            service.objList.Add(stud);


            List<String> value = service.GetObjValueProp();


            Assert.AreEqual("", value[0]);

        }
        [TestMethod]
        public void GetObjNames()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            Student stud1 = new Student();
            stud1.FirstName = "Second";
            stud1.LastName = "Stud";
            stud1.GroupCollection = "Not Default";

            service.objList.Add(stud);
            service.objList.Add(stud1);



            List<String> names = service.GetObjNames();


            Assert.AreEqual("Student Second Stud", names[1]);
        }
        [TestMethod]
        public void InputInfoAndSaveObj()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.entityContext.objList.Add(stud);



            service.IndexOfChosenObj = 0;
            service.PropertyNum = 0;
            service.SaveObjList();
            service.Deserialize();
            service.InputInfoAndSaveObj("Changed");



            Assert.AreEqual("Changed", (service.objList[0] as Student).FirstName);
        }
        [TestMethod]
        public void InputInfoAndSaveObj_negativeIndex()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";
            service.entityContext.objList.Add(stud);



            service.IndexOfChosenObj = 0;
            service.PropertyNum = -1;
            service.SaveObjList();
            service.Deserialize();
            bool isSucceed = service.InputInfoAndSaveObj("Changed");


            Assert.IsFalse(isSucceed);
            Assert.AreEqual("First", (service.objList[0] as Student).FirstName);
        }
        [TestMethod]
        public void GetMethodsInfo_EmptyList()
        {
            service.objList = new List<object>();


            List<String> methods = service.GetMethodsInfo();


            Assert.AreEqual("", methods[0]);
        }
        [TestMethod]
        public void GetMethodsInfo()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            service.objList.Add(stud);



            service.IndexOfChosenObj = 0;
            List<String> methods = service.GetMethodsInfo();


            Assert.AreEqual("Transfer_Object_To_The_Next_Сourse", methods[0]);
        }

        [TestMethod]
        public void MainTest()
        {
            BLL.Program.Main();
        }

        [TestMethod]
        public void GetObjValueProp_WithParametr()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = 0;
            service.objList.Add(stud);


            String prop = service.GetObjValueProp(0);


            Assert.AreEqual("First", prop);
        }
        [TestMethod]
        public void GetObjValueProp_WithParametr_negativeIndex()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = 0;
            service.objList.Add(stud);


            String prop = service.GetObjValueProp(-1);


            Assert.AreEqual("", prop);
        }
        [TestMethod]
        public void GetObjValueProp_WithParametr_indexOutOfRange()
        {
            service.objList = new List<object>();

            Student stud = new Student();
            stud.FirstName = "First";
            stud.LastName = "Stud";
            stud.GroupCollection = "Default";
            stud.Course = "666";
            stud.ArivalCity = "0_0";

            service.IndexOfChosenObj = 55;
            service.objList.Add(stud);


            String prop = service.GetObjValueProp(0);


            Assert.AreEqual("", prop);
        }
        [TestMethod]
        public void Deserialize()
        {
            File.Delete("C:\\Users\\bedu_s_bashkoy\\source\\repos\\LB 5 Krupina 225 t\\UnitTestProject1\\bin\\JsonDataBase.json");

            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<Exception>(() => service.Deserialize()));
        }

    }
}
