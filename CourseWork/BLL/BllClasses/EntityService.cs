using DALWorckWithDataBases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BLL
{
    public class EntityService
    {
        static EntityContext context = new EntityContext();


        public Sorting sorting = new Sorting(context);
        public Category category { get;  } = new Category(context);



        DataSetObjects dataSetProduct = new DataSetObjects(context);
        DataSetObjects dataSetSupplier = new DataSetObjects(context);

        public EntityService(EntityContext entityContext)
        {
            context = entityContext;
            entityContext.LoadConfig();
            Deserialize();
            category.SetObjsCategories(dataSetProduct.getObjList());
        }
        public EntityService() : this(new EntityContext()) { }




        public void RenameGroupOfCurrentObjectAndDellOldGroup(String newName)
        {
            category.RenameGroupOfCurrentObjectAndDellOldGroup(
                newName,
                dataSetProduct.GetFindObjects(),
                dataSetProduct.GetIndexObj()
                );
            category.SetObjsCategories(dataSetProduct.getObjList());
        }

        public List<Object> GetProdObjects()
        { return dataSetProduct.getObjList(); }

        public List<Object> FindWorkableObject(String find)
        { return dataSetProduct.FindWorkableObject(find); }

        public List<Object> GetFindObjects()
        {return dataSetProduct.GetFindObjects(); }

        public Hashtable GetTableOfObjectAndGroup()
        {return dataSetProduct.GetTableOfObjectAndGroup(category); }
        public List<String> GetObjNames(List<Object> objList)
        {return dataSetProduct.GetObjNames(objList);}

        public void SetObjListProduct(List<Object> objList) 
        {dataSetProduct.setObjList(objList);}

        //public void SetObjListSupplier(List<Object> objList){dataSetSupplier.setObjList(objList);}

        public bool InputInfoAndSaveObj(String inputData)
        {
            if (dataSetProduct.InputInfoAndSaveObj(inputData))
            {
                SaveObjList();
                return true;
            }
            return false;
        }

        public List<String> GetObjNamePropsOfCurrentObj()
        { return dataSetProduct.GetObjNamePropsOfCurrentObj();}

        public List<String> GetAllObjValuePropProduct()
        {return dataSetProduct.GetAllObjValueProp(); }

        public List<String> GetNameProductOfCurrentSupp()
        { return dataSetProduct.GetObjNamePropsOfCurrentObj(); }

        /*public List<String> GetAllObjValuePropSupliers()
        {
            if (IndexOfChosenSupplier >= findSupplieObjList.Count)
                return new List<String> { "" };
            return context.GetObjValueProp(findSupplieObjList[IndexOfChosenSupplier]);
        }*/

        /* public String GetAllObjValuePropSuppliers(int propNum)
         {

             if (propNum == -1)
                 return "";
             if (IndexOfChosenSupplier >= findSupplieObjList.Count)
                 return "";
             return context.GetObjValueProp(propNum, findSupplieObjList[IndexOfChosenSupplier]);
         }*/

        public String GetObjValueProp(int propNum)
        {return dataSetProduct.GetObjValueProp(propNum);}

        public void AppendProductInDatabase(string name)
        {
            dataSetProduct.AppendProductInDatabase(name);
           SaveObjList();
        }

        public void DeleteProduct()
        {
            dataSetProduct.DeleteProduct();
            SaveObjList();
        }



        public static List<Type> GetAssemblyTypes()
        { return EntityContext.GetAssemblyTypes(); }



        public void Deserialize() //!
        {  
            context.Deserialize();
            dataSetProduct.setObjList(context.GetProductObjects());
            dataSetSupplier.setObjList(context.GetSupplierObjects());

        }

        public void SaveObjList()
        { context.SavePacketIntoDatabase(dataSetProduct.getObjList(), dataSetSupplier.getObjList());}
        public void SetGroupToCurrentObject_andSave(String group)
        {
            category.SetGroupToCurrentObject_andSave(group, dataSetProduct.getCurrentObject());
            //context.SetGroupToCurrentObject(group, findWorkableObjList[IndexOfChosenObj]);
            SaveObjList();
        }

        public void SetIndexOfObjOfProduct(int index)
        { dataSetProduct.SetIndexObj(index); }

        public void SetIndexOfPropOfProduct(int index)
        { dataSetProduct.SetIndexProprety(index); }

        public int GetIndexOfObjOfProduct()
        { return dataSetProduct.GetIndexObj(); }

        public int GetIndexOfPropOfProduct()
        { return dataSetProduct.GetIndexProprety(); }







        public List<Object> FindSupplierObject(string find)
        { return dataSetSupplier.FindWorkableObject(find);}

        public String GetSuppValueProp(int propNum)
        { return dataSetSupplier.GetObjValueProp(propNum); }

        public List<String> GetNamePropsOfCurrentSupp()
        { return dataSetSupplier.GetObjNamePropsOfCurrentObj(); }

        public List<String> GetAllSuppValueProp()
        { return dataSetSupplier.GetAllObjValueProp(); }

        public void AppendSuppInDatabase(string name)
        {
            dataSetSupplier.AppendProductInDatabase(name);
            SaveObjList();
        }

        public bool InputInfoAndSaveSupp(String inputData)
        {
            if (dataSetSupplier.InputInfoAndSaveObj(inputData))
            {
                SaveObjList();
                return true;
            }
            return false;
        }
        public List<Object> GetSuppObjects()
        { return dataSetSupplier.getObjList();}

        public List<Object> GetFindSupp()
        { return dataSetSupplier.GetFindObjects(); }

        public void DeleteSupp()
        {
            if(GetIndexOfObjOfSupp() >= 0)
                if(GetIndexOfObjOfSupp() < dataSetSupplier.getObjList().Count)
            dataSetSupplier.DeleteProduct();
            SaveObjList();
        }

        public void SetIndexOfObjOfSupp(int index)
        { dataSetSupplier.SetIndexObj(index); }

        public void SetIndexOfPropOfSupp(int index)
        { dataSetSupplier.SetIndexProprety(index); }

        public int GetIndexOfObjOfSupp()
        { return dataSetSupplier.GetIndexObj(); }

        public int GetIndexOfPropOfSupp()
        { return dataSetSupplier.GetIndexProprety(); }

        public void SetObjListSupp(List<Object> objList)
        { dataSetSupplier.setObjList(objList); }










        public List<String> GetNameSettings() 
        { return context.GetNameSettings(); }

        public void SetNumCurrentFileName(int num)
        {
            context.SetNumCurrentFileName(num);
            SaveObjList();
        }

        public int GetSerializeNum()
        { return context.GetSerializeNum(); }
    }
}
