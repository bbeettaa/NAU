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
        public EntityContext entityContext = new EntityContext();
        public List<Object> objList = new List<Object>();
        List<Object> objFindList = new List<Object>();
        public int IndexOfChosenObj { get; set; } = 0;
        public int PropertyNum { get; set; } = 1;
        


        public List<String> groups = new List<string>();

        public EntityService(EntityContext entityContext)
        {
            this.entityContext = entityContext;
            entityContext.LoadConfig();
        }
        public EntityService(): this(new EntityContext()) {}



        public List<String> GetObjNames()
        { 
            return EntityContext.GetObjNames(objList); 
        }
/*        public String GetObjectHeading()
        { return EntityContext.GetObjectHeading(objList[IndexOfChosenObj]); }*/
        public List<String> GetObjNameProps()
        {
            if (IndexOfChosenObj == -1)
                IndexOfChosenObj = 0;

            if (IndexOfChosenObj >= objList.Count)
                return new List<String> { "" };

            return EntityContext.GetObjNameProps(objList[IndexOfChosenObj]);
        }
/*        public String GetObjNameProp(int propNum)
        {
            if (propNum == -1)
                return "";
            return entityContext.GetObjNameProp(propNum, objList[IndexOfChosenObj]);
        }*/
        public List<String> GetObjValueProp()
        {
            if (IndexOfChosenObj >= objList.Count)
                return new List<String> { "" };
            return entityContext.GetObjValueProp(objList[IndexOfChosenObj]);
        }
        public String GetObjValueProp(int propNum)//!
        {
            if (propNum == -1 )
                return "";
            if (IndexOfChosenObj>=objList.Count)
                return "";
            return entityContext.GetObjValueProp(propNum, objList[IndexOfChosenObj]);
        }




        public List<String> GetMethodsInfo()
        {
            if(IndexOfChosenObj >= objList.Count)
                return new List<String> { "" };

            return EntityContext.GetMethodsInfo(objList[IndexOfChosenObj]);
        }
        public static List<Type> GetAssemblyTypes()
        { return EntityContext.GetAssemblyTypes(); }










        public void Deserialize()
        {
            entityContext.CheckCurrentSerializeFile();
                
            objList = entityContext.Deserialize();
        }

        public void SaveObjList()
        { 
            entityContext.SavePacketIntoDatabase(); 
        }
        public void DeleteObj()
        {
            entityContext.CheckCurrentSerializeFile();

            entityContext.objList.Remove(objList[IndexOfChosenObj]);
            objList.Remove(objList[IndexOfChosenObj]);

            SaveObjList();
            CheckIndexOfChoosenObj();
        }
        public void AppendObjectInDatabase(int keyInfo)
        {
            entityContext.CheckCurrentSerializeFile();

            Deserialize();
            if (keyInfo <= GetAssemblyTypes().Count)
            {
                List<Type> list = GetAssemblyTypes();
                objList.Add(EntityContext.CreateObject(list[keyInfo]));
                IndexOfChosenObj = objList.Count - 1;
                SaveObjList();
            }
        }
       // public void CreateNewFile() { entityContext.CreateFile(); }


        public bool InputInfoAndSaveObj(String inputData)
        {
            if (PropertyNum >=0 && IndexOfChosenObj < objList.Count && EntityContext.CheckInputInfo(inputData, PropertyNum, objList[IndexOfChosenObj]))
            {
                SaveObjList();
                return true;
            }
            return false;
        }
        public void WorckWithMethods(int inputKey, bool isWorckableObj)
        {
            MethodInfo[] objInfo = this.objList[IndexOfChosenObj].GetType().GetMethods();
            var infos = this.objList[IndexOfChosenObj].GetType().GetInterfaces();
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
            Object result = objList[IndexOfChosenObj];
            objList[IndexOfChosenObj].GetType().GetMethod(objInfo[inputKey].Name).Invoke(result, Array.Empty<object>());
        }


        public List<Object> FindObjects(String find)
        {
            entityContext.CheckCurrentSerializeFile();

            objList = entityContext.FindObjects(find);
            CheckIndexOfChoosenObj();
            return objList;
        }





        public void CheckIndexOfChoosenObj()
        {
            if (IndexOfChosenObj > objList.Count-1)
                IndexOfChosenObj = objList.Count-1;
            if (IndexOfChosenObj < 0)
                IndexOfChosenObj = 0;
            if (objList.Count == 0)
                IndexOfChosenObj = 0;
        }
/*        public int CheckIndexOfChoosenObjs(int number)
        {
            int maxNumber = GetAssemblyTypes().Count - 1;

            if (number > maxNumber)
                return maxNumber;
            if (number < 0)
                return 0;
            if (maxNumber == -1)
                IndexOfChosenObj = 0;

            return number;
        }*/







        public Hashtable GetTableOfObjectAndGroup()
        {
            Hashtable h = new Hashtable();
            List<String> groups = GetGroupsOfObj();

            for (int i = 0; i < objList.Count; i++)
                    h.Add(objList[i], groups[i]);


            return h;
        }

        public List<String> GetGroupsOfObj()
        {
            return entityContext.GetGroupsOfObj(objList);
        }

        public void SetGroupToCurrentObject_andSave(String group)
        {
            entityContext.SetGroupToCurrentObject(group, objList[IndexOfChosenObj]);
            SaveObjList();
        }

        public void RenameGroupOfCurrentObject(String newName) 
        {
            entityContext.RenameGroupOfObject(newName,objList,IndexOfChosenObj);
        }





        public String CountPercentOfFirstCourseArrivalsStudent()
        {
            List<Object> listObj = entityContext.CreateListOfArrivalStudents_and_notArrivalStudents(objList);
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
            List<Object> listObj = entityContext.CreateListOfArrivalStudentsAndUnsettle(objList);

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
