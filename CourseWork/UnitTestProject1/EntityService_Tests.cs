using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALWorckWithDataBases;
using ProgramClasses;
using System.Collections;

namespace BLL.Tests
{

    class NewEntityContextWithoutObj : EntityContext
    {
        public bool needObj = false;
        override public List<Object> GetProductObjects() {return new List<object>();}
        override public List<Object> GetSupplierObjects() { return new List<object>();  }
        override public void SavePacketIntoDatabase(List<object> objList, List<object> objList1) { }
        override public void Deserialize() { }
        public String getGroup(object obj)
        {
            return (obj as AbstractWorkableClass).GroupCollection;
        }
    }
    class NewEntityContextWithObj : EntityContext
    {
        override public List<Object> GetProductObjects()
        {
            return new List<Object> {
                new Product {Name = "First", Brand="Product" },
                new Product {Name = "Second",Brand="Product" },
                new Product {Name = "Third", Brand="Product" },
                new Product {Name = "Forth", Brand="Product" }};
        }
        override public List<Object> GetSupplierObjects()
        {
            return new List<Object> {
                new Supplier {Name = "First", LastName="Supplier" },
                new Supplier {Name = "Second",LastName="Supplier" },
                new Supplier {Name = "Third", LastName="Supplier" },
                new Supplier {Name = "Forth", LastName="Supplier" }};
        }
        override public void SavePacketIntoDatabase(List<object> objList, List<object> objList1) { }
        override public void Deserialize(){}
    }

    [TestClass]
    public class EntityService_Tests
    {
        EntityService service;

        [TestMethod()]
        public void AppendProductInDatabase_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());

            service.AppendProductInDatabase("Product");

            List<Object> testField = service.GetProdObjects();
            Assert.AreEqual(new List<Object> { new Product() }.Count, testField.Count);
        }

        [TestMethod()]
        public void AppendSuppInDatabase_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());

            service.AppendSuppInDatabase("Supplier");

            List<Object> testField = service.GetSuppObjects();
            Assert.AreEqual(new List<Object> { new Supplier() }.Count, testField.Count);
        }




        [TestMethod()]
        public void DeleteProduct_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetIndexOfObjOfProduct(0);
            service.AppendProductInDatabase("Product");
            service.FindWorkableObject("");

            service.DeleteProduct();

            List<Object> testField = service.GetProdObjects();
            Assert.AreEqual(new List<Object> { }.Count, testField.Count);
        }


        [TestMethod()]
        public void DeleteProduct_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetIndexOfObjOfProduct(0);
            service.AppendProductInDatabase("Product");

            service.DeleteProduct();

            List<Object> testField = service.GetProdObjects();
            Assert.AreEqual(new List<Object> { new Supplier() }.Count, testField.Count);
        }

        [TestMethod()]
        public void DeleteSupp_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetIndexOfObjOfSupp(1);
            service.AppendProductInDatabase("Supplier");

            service.DeleteSupp();

            List<Object> testField = service.GetSuppObjects();
            Assert.AreEqual(new List<Object> { }.Count, testField.Count);
        }

        [TestMethod()]
        public void DeleteSupp_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetIndexOfObjOfSupp(2);
            service.AppendSuppInDatabase("Supplier");

            service.DeleteSupp();

            List<Object> testField = service.GetSuppObjects();
            Assert.AreEqual(new List<Object> { new Supplier() }.Count, testField.Count);
        }

        [TestMethod()]
        public void DeleteSupp_WrongIndex_2_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetIndexOfObjOfSupp(-1);
            service.AppendSuppInDatabase("Supplier");

            service.DeleteSupp();

            List<Object> testField = service.GetSuppObjects();
            Assert.AreEqual(new List<Object> { new Supplier() }.Count, testField.Count);
        }

        [TestMethod()]
        public void Deserialize_Test()
        {
            service = new EntityService(new NewEntityContextWithObj());
            service.Deserialize();
            
            List<Object> expectedFieldProduct = new List<object> {
                new Product {Name = "First", Brand="Product" },
                new Product {Name = "Second",Brand="Product" },
                new Product {Name = "Third", Brand="Product" },
                new Product {Name = "Forth", Brand="Product" }};
            List<Object> expectedFieldSupplier = new List<object> {
                new Supplier {Name = "First", LastName="Supplier" },
                new Supplier {Name = "Second",LastName="Supplier" },
                new Supplier {Name = "Third", LastName="Supplier" },
                new Supplier {Name = "Forth", LastName="Supplier" }};

            List<Object> testFieldProduct = service.GetProdObjects();
            List<Object> testFieldSupplier = service.GetSuppObjects();

            Assert.AreEqual(expectedFieldProduct.Count , testFieldProduct.Count);
            Assert.AreEqual(expectedFieldSupplier.Count, testFieldSupplier.Count);

        }

        [TestMethod()]
        public void FindWorkableObject_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> { 
                new Product() {Name = "Hello" }, 
                new Product() { Brand = "Hello_Two" }, 
                new Product() { Name = "World" } });

            List<object> expectedList= new List<object> {
                new Product() {Name = "Hello" },
                new Product() { Brand = "Hello_Two" } } ;

            List<object> TestObjList = service.FindWorkableObject("Hello");

            List<Object> testField = service.GetSuppObjects();
            Assert.AreEqual(expectedList.Count, TestObjList.Count);
        }

        [TestMethod()]
        public void FindSupplierObject_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Supplier() {Name = "Hello" },
                new Supplier() { LastName = "Hello_Two" },
                new Supplier() { Name = "World" } });

            List<object> expectedList = new List<object> {
                new Supplier() {Name = "Hello" },
                new Supplier() { LastName = "Hello_Two" } };

            List<object> TestObjList = service.FindWorkableObject("Hello");

            List<Object> testField = service.GetProdObjects();
            Assert.AreEqual(expectedList.Count, TestObjList.Count);
        }

        [TestMethod()]
        public void GetAllObjValueProp_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } });
            List<String> expectedList = new List<String>() { "Product", "FF", "Hello_Two", "10.10", "11" };
            service.FindWorkableObject("");

            service.SetIndexOfObjOfProduct(1);
            List<String> testObjList = service.GetAllObjValuePropProduct();

            Assert.AreEqual(expectedList.Count, testObjList.Count);
        }
        [TestMethod()]
        public void GetAllObjValueProp_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } });
            List<String> expectedList = new List<String>() { "" };
            service.FindWorkableObject("");

            service.SetIndexOfObjOfProduct(11);
            List<String> testObjList = service.GetAllObjValuePropProduct();

            Assert.AreEqual(expectedList.Count, testObjList.Count);
        }
        [TestMethod()]
        public void SaveObjList_Test()
        {
            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<Exception>(() => service.SaveObjList()));
        }
        [TestMethod()]
        public void Main_Test()
        {
            Assert.ThrowsException<AssertFailedException>(() => Assert.ThrowsException<Exception>(() => Program.Main()));
        }

        [TestMethod()]
        public void GetNamePropsOfCurrentSupp_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } });
            service.FindWorkableObject("");
            List<String> expectedList = new List<String>() { "Product", "Name", "Brand", "Price", "InStock" };

            service.SetIndexOfObjOfProduct(1);
            List<String> testObjList = service.GetNameProductOfCurrentSupp();

            Assert.AreEqual(expectedList.Count, testObjList.Count);
        }
        [TestMethod()]
        public void GetNamePropsOfCurrentSupp_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } });
            service.FindWorkableObject("");
            List<String> expectedList = new List<String>() { "" };

            service.SetIndexOfObjOfProduct(71);
            List<String> testObjList = service.GetNameProductOfCurrentSupp();

            Assert.AreEqual(expectedList.Count, testObjList.Count);
        }

        [TestMethod()]
        public void GetObjNames_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            List<object> listobj= new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } };
            List<String> expectedList = new List<String>() { "Hello", "FF", "World" };


            List<String> testObjList = service.GetObjNames(listobj);

            Assert.AreEqual(expectedList.Count, testObjList.Count);
        }


        [TestMethod()]
        public void GetObjValueProp_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } });
            service.FindWorkableObject("");
            List<String> expectedList = new List<String>() { "FF", "Hello_Two", "10.10", "11" };


            service.SetIndexOfObjOfProduct(1);
            String testObjValueProp = service.GetObjValueProp(1);


            Assert.AreEqual("Hello_Two", testObjValueProp);
        }
        [TestMethod()]
        public void GetObjValueProp_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } });
            service.FindWorkableObject("");
            List<String> expectedList = new List<String>() { "" };


            service.SetIndexOfObjOfProduct(21);
            String testObjValueProp = service.GetObjValueProp(1);


            Assert.AreEqual("", testObjValueProp);
        }
        [TestMethod()]
        public void GetObjValueProp_WrongIndex_minus1_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" },
                new Product() {Name="FF", Brand = "Hello_Two",Price="10.10",InStock="11" },
                new Product() { Name = "World",Brand="RR",Price="121.10",InStock="121" } });
            service.FindWorkableObject("");
            List<String> expectedList = new List<String>() { "" };


            service.SetIndexOfObjOfProduct(-1);
            String testObjValueProp = service.GetObjValueProp(-1);


            Assert.AreEqual("", testObjValueProp);
        }
        [TestMethod()]
        public void GetTableOfObjectAndGroup_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            Product product1 = new Product() { Name = "Hello", Brand = "FF", Price = "120.10", InStock = "141" };
            Product product2 = new Product() { Name = "FF", Brand = "Hello_Two", Price = "10.10", InStock = "11" };
            Product product3 = new Product() { Name = "World", Brand = "RR", Price = "121.10", InStock = "121" };          

            service.SetObjListProduct(new List<object> { product1,product2,product3 });
            service.FindWorkableObject("");

            Hashtable expectedTable = new Hashtable();
            expectedTable.Add(product1,"Default");
            expectedTable.Add(product2, "Default");
            expectedTable.Add(product3, "Default");


            service.SetIndexOfObjOfProduct(1);
            Hashtable testObjHashtable = service.GetTableOfObjectAndGroup();


            Assert.AreEqual(expectedTable.Count, testObjHashtable.Count);
        }
        [TestMethod()]
        public void GetTableOfObjectAndGroup_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            Product product1 = new Product() { Name = "Hello", Brand = "FF", Price = "120.10", InStock = "141" };
            Product product2 = new Product() { Name = "FF", Brand = "Hello_Two", Price = "10.10", InStock = "11" };
            Product product3 = new Product() { Name = "World", Brand = "RR", Price = "121.10", InStock = "121" };

            service.SetObjListProduct(new List<object> { product1, product2, product3 });
            service.FindWorkableObject("");

            Hashtable expectedTable = new Hashtable();
            expectedTable.Add(product1, "Default");
            expectedTable.Add(product2, "Default");
            expectedTable.Add(product3, "Default");


            service.SetIndexOfObjOfProduct(21);
            Hashtable testObjHashtable = service.GetTableOfObjectAndGroup();


            Assert.AreEqual(expectedTable.Count, testObjHashtable.Count);
        }


        [TestMethod()]
        public void InputInfoAndSaveObj_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" } });

            service.FindWorkableObject("");
            service.SetIndexOfObjOfProduct(0);
            service.SetIndexOfPropOfProduct(1);

            service.InputInfoAndSaveObj("FfF");

            Assert.AreEqual("FfF", service.GetObjValueProp(1));
        }

        [TestMethod()]
        public void InputInfoAndSaveSupp_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListSupp(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" } });

            service.FindSupplierObject("");
            service.SetIndexOfObjOfSupp(0);
            service.SetIndexOfPropOfSupp(1);


            service.InputInfoAndSaveSupp("FfF");


            String getVlaue = service.GetSuppValueProp(1);
            Assert.AreEqual("FfF", getVlaue);
        }
        [TestMethod()]
        public void InputInfoAndSaveSupp_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListSupp(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" } });

            service.FindSupplierObject("");
            service.SetIndexOfObjOfSupp(0);
            service.SetIndexOfPropOfSupp(91);


            service.InputInfoAndSaveSupp("FfF");


            String getVlaue = service.GetSuppValueProp(1);
            Assert.AreEqual("FF", getVlaue);
        }
        [TestMethod()]
        public void InputInfoAndSaveObj_WrongIndex_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());
            service.SetObjListProduct(new List<object> {
                new Product() {Name = "Hello",Brand="FF",Price="120.10",InStock="141" } });
            service = new EntityService();
            service.FindWorkableObject("");
            service.SetIndexOfObjOfProduct(-1);
            service.SetIndexOfPropOfProduct(-1);

            bool result = service.InputInfoAndSaveObj("");

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void GetNameSettings_Test()
        {
            service = new EntityService(new NewEntityContextWithoutObj());

            List<String> nameSett = service.GetNameSettings();

            Assert.AreEqual(4, nameSett.Count);
        }
        [TestMethod()]
        public void SetIndexOfPropOfSupp()
        {
            service = new EntityService(new NewEntityContextWithoutObj());

            service.SetIndexOfPropOfSupp(5);

            Assert.AreEqual(5, service.GetIndexOfPropOfSupp());
        }
        [TestMethod()]
        public void SetGroupToCurrentObject_andSave()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);
            service.SetObjListProduct(new List<object> {
                new Product() {GroupCollection="Def" } });

            service.FindWorkableObject("");
            service.SetIndexOfObjOfProduct(0);
            service.SetIndexOfPropOfProduct(0);


            service.SetGroupToCurrentObject_andSave("newGR");

            Assert.AreEqual("newGR", ent.getGroup(service.GetProdObjects()[0]));
        }

        [TestMethod()]
        public void RenameGroupOfCurrentObjectAndDellOldGroup()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);
            service.SetObjListProduct(new List<object> {
                new Product() {GroupCollection="Def" },new Product() {GroupCollection="Def" } });

            service.FindWorkableObject("");
            service.SetIndexOfObjOfProduct(0);
            service.SetIndexOfPropOfProduct(0);


            service.RenameGroupOfCurrentObjectAndDellOldGroup("newGR");

            Assert.AreEqual("newGR", ent.getGroup(service.FindWorkableObject("")[0]));
        }
        [TestMethod()]
        public void SetNumCurrentFileName()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);

            service.SetNumCurrentFileName(2);
            int curentSerializer = service.GetSerializeNum();

            Assert.AreEqual(2, curentSerializer);
        }


        [TestMethod()]
        public void GetObjNamePropsOfCurrentObj()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);
            service.SetObjListProduct(new List<object> {
                new Product() {GroupCollection="Def" },new Product() {GroupCollection="Def" } });

            service.FindWorkableObject("");
            service.SetIndexOfObjOfProduct(-1);


           List<String> listProps = service.GetObjNamePropsOfCurrentObj();


            Assert.AreEqual(5, listProps.Count);
        }

        [TestMethod()]
        public void GetNamePropsOfCurrentSupp()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);
            service.SetObjListSupp(new List<object> {
                new Product() {GroupCollection="Def" },new Product() {GroupCollection="Def" } });

            service.FindSupplierObject("");
            service.SetIndexOfObjOfSupp(-1);


            List<String> listProps = service.GetNamePropsOfCurrentSupp();


            Assert.AreEqual(5, listProps.Count);
        }
        [TestMethod()]
        public void GetAllSuppValueProp()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);
            service.SetObjListSupp(new List<object> {
                new Product() {GroupCollection="Def" },new Product() {GroupCollection="Def" } });

            service.FindSupplierObject("");
            service.SetIndexOfObjOfSupp(0);


            List<String> listProps = service.GetAllSuppValueProp();


            Assert.AreEqual(5, listProps.Count);
        }

        [TestMethod()]
        public void GetIndexOfObjOfProduct()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);

            service.SetIndexOfObjOfProduct(-1);


            int index = service.GetIndexOfObjOfProduct();


            Assert.AreEqual(-1, index);
        }
        [TestMethod()]
        public void GetIndexOfPropOfProduct()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);

            service.SetIndexOfPropOfProduct(-1);


            int index = service.GetIndexOfPropOfProduct();


            Assert.AreEqual(-1, index);
        }

        [TestMethod()]
        public void GetFindSupp()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);
            service.SetObjListSupp(new List<object> {
                new Product() {GroupCollection="Def" },new Product() {GroupCollection="Def" } });
            service.FindSupplierObject("");
            List<Object> findList= service.GetFindSupp();


            Assert.AreEqual(2, findList.Count);
        }
      

        [TestMethod()]
        public void GetFindObjects()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);
            service.SetObjListProduct(new List<object> {
                new Product() {GroupCollection="Def" },new Product() {GroupCollection="Def" } });
            service.FindWorkableObject("");
            List<Object> findList = service.GetFindObjects();


            Assert.AreEqual(2, findList.Count);
        }
        [TestMethod()]
        public void GetAssemblyTypes()
        {
            NewEntityContextWithoutObj ent = new NewEntityContextWithoutObj();
            service = new EntityService(ent);


            List<Type> findList = EntityService.GetAssemblyTypes();


            Assert.AreEqual(2, findList.Count);
        }
    }
}