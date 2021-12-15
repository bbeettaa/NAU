using DALWorckWithDataBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Category
    {
        readonly List<Object> categories = new List<Object>() { "Default", true };
        EntityContext context;
        public Category(EntityContext context) { this.context = context; }

        public void RenameGroupOfCurrentObjectAndDellOldGroup(String newName, List<Object> findWorkableObjList, int IndexOfChosenObj)
        {
            if (IndexOfChosenObj >= findWorkableObjList.Count) return;
            if (IndexOfChosenObj < 0) return;
            String oldGr = context.GetObjCategory(findWorkableObjList[IndexOfChosenObj]);
            findWorkableObjList = context.RenameGroupOfObject(newName, findWorkableObjList, IndexOfChosenObj);
            DeleteCategory(oldGr, findWorkableObjList);
        }


        public void SetObjsCategories(List<Object> allWorkableObjList)
        {
            foreach (var obj in allWorkableObjList)
                AddCategory(context.GetObjCategory(obj));
            DelSimilarCategories();
        }


        public List<Object> GetObjsCategories()
        {
            DelSimilarCategories();
/*            for (int i = 0; i < categories.Count - 2; i += 2)
                if (categories[i].ToString() == categories[i + 2].ToString())
                {
                    categories.RemoveAt(i + 2);
                    categories.RemoveAt(i + 2);
                    i -= 2;
                }*/
            return categories;
        }

        public void DelSimilarCategories()
        {
            for (int i = 0; i < categories.Count; i += 2)
                for (int ii = 2 + i; ii < categories.Count; ii += 2)
                {
                    if (categories[i].ToString() == categories[ii].ToString())
                        if(i >= 0 )
                    {
                        categories.RemoveAt(i);
                        categories.RemoveAt(i);
                        ii = i;
                        i -= 2;
                    }
                }
        }

        public void AddCategory(String name)
        {
            if (name == "") return;
            categories.Add(name);
            categories.Add(true);

            DelSimilarCategories();
        }

        public void DeleteCategory(String name, List<Object> findWorkableObjList)
        {
            if (name == "Default") return;
            findWorkableObjList = context.RenameGroupOfObject("Default", name, findWorkableObjList);

            for (int i = 0; i < categories.Count; i++)
                if (categories[i].ToString() == name)
                {
                    categories.RemoveAt(i);
                    categories.RemoveAt(i);
                }
        }

       

        public List<String> GetGroupsOfObj(List<Object> allWorkableObjList)
        {
            return context.GetGroupsOfObj(allWorkableObjList);
        }

        public void SetGroupToCurrentObject_andSave(String group,Object currentObj)
        {
            context.SetGroupToCurrentObject(group, currentObj);
        }


    }
}
