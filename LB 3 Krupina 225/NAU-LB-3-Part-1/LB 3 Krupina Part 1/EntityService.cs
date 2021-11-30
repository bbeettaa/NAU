using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NamespaceEntityContext;

namespace BLL
{
    public class EntityService
    {
        public EntityContext entityContext = new();
        public List<Object> objList = new();
        public int IndexOfChosenObj { get; set; } = 0;
        public int PropertyNum { get; set; } = 1;
        public int PropertyNumForConfig { get; set; } = 1;

        int previousIndeOfchoosenObjx = 1;
        int previousPropertyNum = 1;

        public EntityService()
        {
            entityContext.LoadConfig();
        }

        
        public List<String> GetObjectInfo()
        { return EntityContext.GetObjectInfo(objList[IndexOfChosenObj]); }
        public static List<Type> GetAssemblyTypes()
        { return EntityContext.GetAssemblyTypes(); }


        public void SaveObjList()
        {
            //entityContext.CreatePacketFromList(objList);
            entityContext.SavePacketIntoDatabase();
        }
        public void DeleteObj()
        {
            objList.RemoveAt(IndexOfChosenObj);
            SaveObjList();
            CheckIndexOfChoosenObj();
        }
        public void AppendObjectInDatabase(Object obj)
        {
            Deserialize();
            objList.Add(obj);
            SaveObjList();
        }


       

        public void Deserialize()
        { objList = entityContext.Deserialize(); }



        public List<Object> FindObjects(String find)
        {
            objList = entityContext.FindObjects(find);
            CheckIndexOfChoosenObj();
            return objList;
        }
        public static String CheckStr(ConsoleKeyInfo inputKey, String str)
        {
            if (inputKey.Key == ConsoleKey.Backspace)
            {
                if (str.Length > 0)
                    str = str.Remove(str.Length - 1);
            }
            else
            {
                str += inputKey.KeyChar;
                str = Regex.Replace(str, @"[\[\]\^А-Яа-я]", "");
            }
            return str;
        }



       
        private void CheckIndexOfChoosenObj()
        {
            if (IndexOfChosenObj > objList.Count - 1)
                IndexOfChosenObj = objList.Count - 1;
            if (IndexOfChosenObj < 0)
                IndexOfChosenObj = 0;
            if (objList.Count == 0)
                IndexOfChosenObj = 0;
        }
       
        public void PrintObjects()
        { 
        for (int i = 0; i < objList.Count; i++)
            {
                IndexOfChosenObj = i;
                List<String> str = GetObjectInfo();
                Console.Write($"{i}) ");
                foreach (var s in str)
                    Console.WriteLine(s);
                Console.Write("\n");
            }
        }

    }
}
