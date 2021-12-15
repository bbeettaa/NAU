using DALWorckWithDataBases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class DataSetObjects
    {
        EntityContext context;
        List<Object> allWorkableObjList = new List<Object>();
        List<Object> findWorkableObjList = new List<Object>();
        public int IndexOfChosenObj { get; set; } = 0;
        public int PropertyNum { get; set; } = 1;

        public DataSetObjects(EntityContext context)
        { this.context = context; }

        public void setObjList(List<Object> allWorkableObjList) 
        {
            this.allWorkableObjList = allWorkableObjList;
        }

        public List<Object> getObjList()
        {
            return allWorkableObjList;
        }

        public Object getCurrentObject() 
        { return findWorkableObjList[IndexOfChosenObj]; }

        public void SetIndexObj(int index)
        { this.IndexOfChosenObj = index; }

        public void SetIndexProprety(int index)
        { this.PropertyNum = index; }

        public int GetIndexObj()
        { return this.IndexOfChosenObj; }

        public int GetIndexProprety()
        { return this.PropertyNum; }

        public List<Object> FindWorkableObject(String find)
        {
            //entityContext.CheckCurrentSerializeFile();
            findWorkableObjList.Clear();
            CheckIndexOfChoosenObj();
            foreach (var obj in allWorkableObjList)
                if (context.IsObjContain(find, obj))
                    findWorkableObjList.Add(obj);
            return findWorkableObjList;
        }

        public List<Object> GetFindObjects()
        {
            return findWorkableObjList;
        }

        public Hashtable GetTableOfObjectAndGroup(Category category)
        {
            Hashtable h = new Hashtable();
            List<String> groups = category.GetGroupsOfObj(allWorkableObjList);

            for (int i = 0; i < allWorkableObjList.Count; i++)
                h.Add(allWorkableObjList[i], groups[i]);

            return h;
        }

        public List<String> GetObjNames(List<Object> objList)
        {
            return EntityContext.GetObjNames(objList);
        }





        public void CheckIndexOfChoosenObj()
        {
            if (IndexOfChosenObj > findWorkableObjList.Count - 1)
                IndexOfChosenObj = findWorkableObjList.Count - 1;
            if (IndexOfChosenObj < 0)
                IndexOfChosenObj = 0;
            if (findWorkableObjList.Count == 0)
                IndexOfChosenObj = 0;
        }

        public bool InputInfoAndSaveObj(String inputData)
        {
            if (PropertyNum >= 0 && IndexOfChosenObj < findWorkableObjList.Count &&
                EntityContext.CheckInputInfo(inputData, PropertyNum, findWorkableObjList[IndexOfChosenObj]))
            {
                //SaveObjList();
                return true;
            }
            return false;
        }

        public List<String> GetObjNamePropsOfCurrentObj()
        {
            if (IndexOfChosenObj == -1)
                IndexOfChosenObj = 0;

            if (IndexOfChosenObj >= findWorkableObjList.Count)
                return new List<String> { "" };

            return EntityContext.GetObjNameProps(findWorkableObjList[IndexOfChosenObj]);
        }

        public List<String> GetAllObjValueProp()
        {
            if (IndexOfChosenObj >= findWorkableObjList.Count)
                return new List<String> { "" };
            return context.GetObjValueProp(findWorkableObjList[IndexOfChosenObj]);
        }

        public String GetObjValueProp(int propNum)
        {
            if (propNum == -1)
                return "";
            if (IndexOfChosenObj >= findWorkableObjList.Count)
                return "";
            return context.GetObjValueProp(propNum, findWorkableObjList[IndexOfChosenObj]);
        }

        public void AppendProductInDatabase(string name)
        {
            int objNumber = GetAssemblyTypes().Select(t => t.Name).ToList().IndexOf(name);

            if (objNumber <= GetAssemblyTypes().Count)
            {
                allWorkableObjList.Add(EntityContext.CreateObject(GetAssemblyTypes()[objNumber]));
                IndexOfChosenObj = allWorkableObjList.Count - 1;
                //SaveObjList();
            }
        }

        public void DeleteProduct()
        {
            if (IndexOfChosenObj >= findWorkableObjList.Count) return;
            allWorkableObjList.Remove(findWorkableObjList[IndexOfChosenObj]);
            findWorkableObjList.Remove(findWorkableObjList[IndexOfChosenObj]);

            //SaveObjList();
            CheckIndexOfChoosenObj();
        }

        public static List<Type> GetAssemblyTypes()
        { return EntityContext.GetAssemblyTypes(); }

        
    }
}
