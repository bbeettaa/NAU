using DALWorckWithDataBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BLL
{
    public class EntityService
    {
        public EntityContext entityContext = new();
        List<Object> objList = new ();
        public int IndexOfChosenObj { get; set; } = 0;
        public int PropertyNum { get; set; } = 1;
        public int PropertyNumForConfig { get; set; } = 1;

        int previousIndeOfchoosenObjx = 1;
        int previousPropertyNum = 1;

        public EntityService()
        { entityContext.LoadConfig(); }



        public static List<String> GetObjNames(List<Object> objList)
        { return EntityContext.GetObjNames(objList); }
        public String GetObjectHeading()
        { return EntityContext.GetObjectHeading(objList[IndexOfChosenObj]); }
        public List<String> GetObjectInfo()
        { return EntityContext.GetObjectInfo(objList[IndexOfChosenObj]); }
        public List<String> GetMethodsInfo()
        { return EntityContext.GetMethodsInfo(objList[IndexOfChosenObj]); }
        public static List<Type> GetAssemblyTypes()
        { return EntityContext.GetAssemblyTypes(); }




        private void Deserialize()
        {
            if (entityContext.CheckCurrentSerializeFile() == false) throw new CustomException($"Файл \"{entityContext.GetCurrentFile()}\" не знайдено.");
            objList = entityContext.Deserialize();
        }
        public int ObjListLength()
        { return objList.Count; }
        private void SaveObjList()
        {
            if(entityContext.CheckCurrentSerializeFile() == false) throw new CustomException($"Файл \"{entityContext.GetCurrentFile()}\" не знайдено.");

            entityContext.CreatePacketFromList(objList);
            entityContext.SavePacketIntoDatabase();
        }
        public void DeleteObj()
        {
            if (entityContext.CheckCurrentSerializeFile() == false) throw new CustomException($"Файл \"{entityContext.GetCurrentFile()}\" не знайдено.");

            objList.RemoveAt(IndexOfChosenObj);
            SaveObjList();
            CheckIndexOfChoosenObj();
        }
        public void AppendObjectInDatabase(int keyInfo)
        {
            if (entityContext.CheckCurrentSerializeFile() == false) throw new CustomException($"Файл \"{entityContext.GetCurrentFile()}\" не знайдено.");

            Deserialize();
            if (keyInfo <= GetAssemblyTypes().Count)
            {
                List<Type> list = GetAssemblyTypes();
                objList.Add(EntityContext.CreateObject(list[keyInfo - 1]));
                IndexOfChosenObj = objList.Count - 1;
                SaveObjList();
            }
        }
        public void CreateNewFile()
        { entityContext.CreateFile(); }



        public bool InputInfoAndSaveObj(String inputData)
        {
            if (EntityContext.CheckInputInfo(inputData, PropertyNum, objList[IndexOfChosenObj]))
            {
                SaveObjList();
                return true;
            }
            return false;
        }
        public void WorckWithMethods(ConsoleKey inputKey, bool isWorckableObj)
        {
            MethodInfo[] objInfo = this.objList[IndexOfChosenObj].GetType().GetMethods();
            var infos = this.objList[IndexOfChosenObj].GetType().GetInterfaces();
            objInfo = (from x in objInfo where x.ToString().Contains("_Object_") select x).ToArray();
            //objInfo = (from x in objInfo where x.ToString().Contains("_Certain_") select x).ToArray();

            int key = (int)inputKey - (int)ConsoleKey.F1;

            for (int i = 0; i < objInfo.Length; i++)
                if (i == key)
                {
                    if (isWorckableObj == false && objInfo[i].Name.Contains("_Object_"))
                    {
                        InvokeSettingsMethod(key);
                        return;
                    }
                    else if (objInfo[i].Name.Contains("_Certain_"))
                        InvokeCertainMethod();
                    else
                        InvokeSimpleMethod(key, objInfo);
                }
            SaveObjList();
        }
        private void InvokeSettingsMethod(int Key)
        {
            entityContext.SetSettingsSerialization(Key);
            entityContext.SaveConfig();
            entityContext.LoadConfig();
            objList[0] = entityContext.settings;
        }
        private static void InvokeCertainMethod()
        {
            //*** Старая реализация *** (для второй лабы)

            //Console.WriteLine("Введіть значення: ");
            //string num = Console.ReadLine();
            //num = Regex.Replace(num, @"[A-Za-z-+=.]", "");
            //if (num == "") return;
            //ConstructorInfo ctor = objList[IndexOfChosenObj].GetType().GetConstructor(new Type[] { });
            //Object result = objList[IndexOfChosenObj];
            //objList[indexOfChosenObj].GetType().GetMethod(objInfo[i].Name).Invoke(result, new object[] { double.Parse(num) });
        }
        private void InvokeSimpleMethod(int inputKey, MethodInfo[] objInfo)
        {
            Object result = objList[IndexOfChosenObj];
            objList[IndexOfChosenObj].GetType().GetMethod(objInfo[inputKey].Name).Invoke(result, Array.Empty<object>());
        }




        public List<Object> FindObjects(String find)
        {
            if (entityContext.CheckCurrentSerializeFile() == false) 
                throw new CustomException($"Файл \"{entityContext.GetCurrentFile()}\" не знайдено.");

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



        public int SelectProperty(ConsoleKeyInfo inputKey)
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (!(PropertyNum - 1 < 1))
                        PropertyNum -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    if (PropertyNum + 1 <= GetObjectInfo().Count-1)
                        PropertyNum += 1;
                    break;
            }
            return PropertyNum;
        }
        public void SelectObject(ConsoleKeyInfo inputKey)
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (!(IndexOfChosenObj - 1 < 0))
                        IndexOfChosenObj -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    if (IndexOfChosenObj + 1 < objList.Count)
                        IndexOfChosenObj += 1;
                    break;
            }
        }
        private void CheckIndexOfChoosenObj()
        {
            if (IndexOfChosenObj > objList.Count-1)
                IndexOfChosenObj = objList.Count-1;
            if (IndexOfChosenObj < 0)
                IndexOfChosenObj = 0;
            if (objList.Count == 0)
                IndexOfChosenObj = 0;
        }
        public int CheckIndexOfChoosenObjs(int number)
        {
            int maxNumber = GetAssemblyTypes().Count - 1;

            if (number > maxNumber)
                return maxNumber;
            if (number < 0)
                return 0;
            if (maxNumber == -1)
                IndexOfChosenObj = 0;

            return number;
        }



        public void ChangeSettings()
        {
            objList.Add(entityContext.settings);

            previousIndeOfchoosenObjx = IndexOfChosenObj;
            previousPropertyNum = PropertyNum;
            PropertyNum = PropertyNumForConfig;

            IndexOfChosenObj = objList.Count - 1;
        }
        public void EndOfChangeSettings()
        {
            IndexOfChosenObj = previousIndeOfchoosenObjx;
            PropertyNumForConfig = PropertyNum;
            PropertyNum = previousPropertyNum;

            objList.RemoveAt(objList.Count-1);

            entityContext.SaveConfig();
            entityContext.LoadConfig();
        }


    }
}
