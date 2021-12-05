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
        EntityContext entityContext = new EntityContext();
        List<Object> allObjList = new List<Object>();
        List<Object> findObjList = new List<Object>();

        List<String> objNames = new List<String>();
        List<Object> categories = new List<Object>() { "Default" ,true};

        List<String> groups = new List<string>();


        public int IndexOfChosenObj { get; set; } = 0;
        public int PropertyNum { get; set; } = 1;



        public EntityService(EntityContext entityContext)
        {
            this.entityContext = entityContext;
            entityContext.LoadConfig();
            Deserialize();
            SetObjsCategories();
        }
        public EntityService() : this(new EntityContext()) { }



        ////////////////////////////////////////////////////////////////////////////////////
    
        public void RenameGroupOfCurrentObject(String newName)
        {
            if (IndexOfChosenObj >= findObjList.Count || IndexOfChosenObj < 0) return;
            findObjList = entityContext.RenameGroupOfObject(newName, findObjList, IndexOfChosenObj);
            SetObjsCategories();
            SaveObjList();
        }


        public void SetObjsCategories()
        {
           // categories.Clear();
            foreach (var obj in allObjList) 
                AddCategory(entityContext.GetObjCategory(obj));
            DelSimilarCategories();
        }

        public List<Object> GetObjsCategories()
        {
            //SetObjsCategories();
            for (int i = 0; i < categories.Count-2; i += 2)
                if (categories[i].ToString() == categories[i + 2].ToString())
                {
                    categories.RemoveAt(i + 2);
                    categories.RemoveAt(i + 2);
                    i -= 2;
                }
            return categories; 
        }

        public void DelSimilarCategories()
        {
            for (int i = 0; i < categories.Count; i += 2)
                for (int ii = 2 + i; ii < categories.Count; ii += 2)
                {
                    if (i >= 0 && categories[i].ToString() == categories[ii].ToString())
                    {
                        categories.RemoveAt(i);
                        categories.RemoveAt(i);
                        ii = i;
                        i -=2;
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

        public void DeleteCategory(String name)
        {
            for (int i = 0; i < categories.Count; i++)
                if (categories[i].ToString() == name)
                {
                    categories.RemoveAt(i);
                    categories.RemoveAt(i);
                    return;
                }

        }

        public void ChangeShowCatigories(String name, bool isShow)
        {
            for (int i = 0; i < categories.Count; i++)
                if (categories[i].ToString() == name)
                {
                    categories[i + 1] = isShow;
                    return;
                }
        }

        public List<Object> GetFindObjects(String find)
        {
            //entityContext.CheckCurrentSerializeFile();
            CheckIndexOfChoosenObj();
            findObjList = entityContext.FindObjects(find, allObjList);
            return findObjList;
        }


        public Hashtable GetTableOfObjectAndGroup()
        {
            Hashtable h = new Hashtable();
            List<String> groups = GetGroupsOfObj();

            for (int i = 0; i < allObjList.Count; i++)
                h.Add(allObjList[i], groups[i]);

            return h;
        }
        public List<String> GetObjNames(List<Object> objList)
        {
            return EntityContext.GetObjNames(objList);
        }


        ////////////////////////////////////////////////////////////////////////////////////






        public List<String> GetObjNameProps()
        {
            if (IndexOfChosenObj == -1)
                IndexOfChosenObj = 0;

            if (IndexOfChosenObj >= findObjList.Count)
                return new List<String> { "" };

            return EntityContext.GetObjNameProps(findObjList[IndexOfChosenObj]);
        }

        public List<String> GetAllObjValueProp()
        {
            if (IndexOfChosenObj >= findObjList.Count)
                return new List<String> { "" };
            return entityContext.GetObjValueProp(findObjList[IndexOfChosenObj]);
        }

        public String GetObjValueProp(int propNum)
        {
            if (propNum == -1 )
                return "";
            if (IndexOfChosenObj>= findObjList.Count)
                return "";
            return entityContext.GetObjValueProp(propNum, findObjList[IndexOfChosenObj]);
        }




        public List<String> GetMethodsInfo()
        {
            if(IndexOfChosenObj >= findObjList.Count) return new List<String> { "" };

            return EntityContext.GetMethodsInfo(findObjList[IndexOfChosenObj]);
        }
        public static List<Type> GetAssemblyTypes()
        { return EntityContext.GetAssemblyTypes(); }










        public void Deserialize() //!
        {  
            allObjList = entityContext.Deserialize();
        }

        public void SaveObjList()
        {  entityContext.SavePacketIntoDatabase(allObjList);   }
        public void DeleteObj()
        {
            //entityContext.CheckCurrentSerializeFile();

            //entityContext.objList.Remove(objList[IndexOfChosenObj]);
            //allObjList.Remove(allObjList[IndexOfChosenObj]);
            allObjList.Remove(findObjList[IndexOfChosenObj]);
            findObjList.Remove(findObjList[IndexOfChosenObj]);

            SaveObjList();
            CheckIndexOfChoosenObj();
        }
        public void AppendObjectInDatabase(int objNumber)
        {
            //entityContext.CheckCurrentSerializeFile();

            //Deserialize();
            if (objNumber <= GetAssemblyTypes().Count)
            {
                allObjList.Add(EntityContext.CreateObject(GetAssemblyTypes()[objNumber]));
                IndexOfChosenObj = allObjList.Count - 1;
                SaveObjList();
            }
        }



        public bool InputInfoAndSaveObj(String inputData)
        {
            if (PropertyNum >=0 && IndexOfChosenObj < findObjList.Count && 
                EntityContext.CheckInputInfo(inputData, PropertyNum, findObjList[IndexOfChosenObj]))
            {
                SaveObjList();
                return true;
            }
            return false;
        }
        public void WorckWithMethods(int inputKey, bool isWorckableObj)
        {
            MethodInfo[] objInfo = this.allObjList[IndexOfChosenObj].GetType().GetMethods();
            var infos = this.allObjList[IndexOfChosenObj].GetType().GetInterfaces();
            objInfo = (from x in objInfo where x.ToString().Contains("_Object_") select x).ToArray();

            for (int i = 0; i < objInfo.Length; i++)
                if (i == inputKey)
                {
/*                    if (isWorckableObj == false && objInfo[i].Name.Contains("_Object_"))
                    {
                        InvokeSettingsMethod(inputKey);
                        return;
                    }*/
                    //else if (objInfo[i].Name.Contains("_Certain_"))
                    //    InvokeCertainMethod();
                   // else
                        InvokeSimpleMethod(inputKey, objInfo);
                }
            SaveObjList();
        }
       /* private void InvokeSettingsMethod(int Key)
        {
            entityContext.SetSettingsSerialization(Key);
            entityContext.SaveConfig();
            entityContext.LoadConfig();
            objList[0] = entityContext.settings;
        }*/
       /* private static void InvokeCertainMethod()
        {
            //*** Старая реализация *** (для второй лабы)

            //Console.WriteLine("Введіть значення: ");
            //string num = Console.ReadLine();
            //num = Regex.Replace(num, @"[A-Za-z-+=.]", "");
            //if (num == "") return;
            //ConstructorInfo ctor = objList[IndexOfChosenObj].GetType().GetConstructor(new Type[] { });
            //Object result = objList[IndexOfChosenObj];
            //objList[indexOfChosenObj].GetType().GetMethod(objInfo[i].Name).Invoke(result, new object[] { double.Parse(num) });
        }*/
        private void InvokeSimpleMethod(int inputKey, MethodInfo[] objInfo)
        {
            Object result = allObjList[IndexOfChosenObj];
            allObjList[IndexOfChosenObj].GetType().GetMethod(objInfo[inputKey].Name).Invoke(result, Array.Empty<object>());
        }


        




        public void CheckIndexOfChoosenObj()
        {
            if (IndexOfChosenObj > findObjList.Count-1)
                IndexOfChosenObj = findObjList.Count-1;
            if (IndexOfChosenObj < 0)
                IndexOfChosenObj = 0;
            if (findObjList.Count == 0)
                IndexOfChosenObj = 0;
        }





       

        public List<String> GetGroupsOfObj()
        {
            return entityContext.GetGroupsOfObj(allObjList);
        }

        public void SetGroupToCurrentObject_andSave(String group)
        {
            entityContext.SetGroupToCurrentObject(group, findObjList[IndexOfChosenObj]);
            SaveObjList();
        }

        





        public String CountPercentOfFirstCourseArrivalsStudent()
        {
            List<Object> listObj = entityContext.CreateListOfArrivalStudents_and_notArrivalStudents(findObjList);
            bool isCountFromKyiv = true;
            float total = listObj.Count-1;
            float matchStudent = 0;
            float percent = 0;

            for (int i = 0; i < listObj.Count; i++)
            {
                if (!isCountFromKyiv)
                    matchStudent++;

                if (listObj[i] == null)
                    isCountFromKyiv = false;
            }

            if (total >0) 
                percent = 100 / (total / matchStudent);

            string message =
                $"Всього студентів на першому курсі: {total}\n"+
                $"Студентів на першому курсі, що приїхали: {matchStudent}\n" +
                $"Відсоток студентів - {percent}%";

            return message;
        }

        public void HostelArrivalStud()
        {
            //             - собирает в список студентов, те у которых нет записи, записуються по 4 человека в комнату,
            //              в одно общежитие, комнат не больше 20 на этаж, не больше 4 этажей.
            List<Object> listObj = entityContext.CreateListOfArrivalStudentsAndUnsettle(findObjList);

            string room = "1";
            int floor = 10;
            int tryParse = 0;
            for (int i = 0; i < listObj.Count; i++)
            {
                if (i % 2 == 0)
                    if (i >= 10 && i > 0)
                    {
                        int.TryParse(room, out tryParse);
                        room = (tryParse += 1).ToString(); 
                    }
                if (i % 20 == 0 && i >0) 
                {
                    floor+=10;
                    room = "1";
                }

                entityContext.SettleStudentToRoomOfHostel($"{floor}{room}.01",listObj[i]);
            }
        }
    }
}
