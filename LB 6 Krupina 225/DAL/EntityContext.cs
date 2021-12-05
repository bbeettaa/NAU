using System;
using System.Collections.Generic;
using System.Linq;
using ProgramClasses;
using DAL_Classes;

namespace DALWorckWithDataBases
{
    public class EntityContext
    {
        //String fileName;
        //Type[] typesArr = new Type[] { };
        public List<Object> objList = new List<Object>();
        readonly List<AbstarctDataProvider> dataProvider = new List<AbstarctDataProvider>();
        public Settings settings = new Settings();



        public int IndexOfDataprovider { get; set; } = 0;
        readonly JsonProvider JsonConfig = new JsonProvider();

        public EntityContext()
        {  
            JsonProvider Json = new JsonProvider();
            dataProvider.Add(Json);
            XML_Provider xml = new XML_Provider();
            dataProvider.Add(xml);
            BinaryProvider binary = new BinaryProvider();
            dataProvider.Add(binary);
            CustomProvider custom = new CustomProvider();
            dataProvider.Add(custom);

            IndexOfDataprovider = settings.NumCurrentFileName;

            LoadSettings();
            Setsettings(settings.appDir, settings.fileNames[IndexOfDataprovider]);
            SaveConfig();
            LoadSettings();
        }



        public String GetObjCategory(object obj)
        { return (obj as AbstractPerson).GroupCollection; }










        public static List<String> GetObjNames(List<Object> objList)
        {
            List<Type> typesArr = AbstractPerson.GetAssemblyTypes();
            List<String> str = new List<String>();

            foreach (var obj in objList)
            {
                foreach (var t in typesArr)
                {
                    if (obj.GetType().Name == t.Name)
                        str.Add((obj as Person).HeadingOfObject());
                }
            }

            return str;
        }
        public static String GetObjectHeading(Object obj)
        { return (obj as AbstractClass).HeadingOfObject(); }
        public static List<String> GetObjNameProps(Object obj)
        { return (obj as AbstractClass).GetObjNameProp().ToList<String>(); }
        public static List<String> GetMethodsInfo(Object obj)
        { return (obj as AbstractClass).GetMethodsInfo().ToList<String>(); }
        public List<Object> FindObjects(String find, List<Object> objList)
        {
 
            List<Object> list = new List<Object>();
            foreach (var obj in objList)
                if ((obj as AbstractClass) != null &&
                    (obj as AbstractClass).IsFindInfo(find) == true)
                        list.Add(obj);
                
            return list;
        }




        virtual public List<Object> Deserialize()
        {
            objList = dataProvider[IndexOfDataprovider].Deserialize();
            return objList;
        }
        public static bool CheckInputInfo(String inputData, int propertyNum, Object obj)
        {
            if ((obj as AbstractClass).ChangeProperties(propertyNum, inputData))
                return true;
            return false;
        }
        public static List<Type> GetAssemblyTypes()
        { return AbstractPerson.GetAssemblyTypes(); }





        public String GetObjNameProp(int propNum, object obj)
        {
            String str = (obj as AbstractClass).GetObjNameProp()[propNum+1];
            str = str.Remove(0,str.LastIndexOf("\t ")+2);
            return str;
        }
        public static List<String> GetObjNameProp(Object obj)
        { return (obj as AbstractClass).GetObjNameProp().ToList<String>(); }

        public String GetObjValueProp(int propNum, object obj)//!
        {
            try
            {
                return (obj as AbstractClass).GetObjValueProp()[propNum];
            }
            catch (NullReferenceException ex1)
            {
                return null;
            }
            catch (Exception ex)
            {
                return GetObjValueProp(--propNum, obj);
            }
        }
        public List<String> GetObjValueProp(Object obj)
        { return (obj as AbstractClass).GetObjValueProp().ToList<String>(); }





        public static Object CreateObject(Type type)
        {
            Object obj = Activator.CreateInstance(type);
            return obj;
        }
        public void SavePacketIntoDatabase(List<object> objList)
        {
            dataProvider[IndexOfDataprovider].CheckFile();
            CreatePacketFromList(objList);
            dataProvider[IndexOfDataprovider].Serialize(); 
        }
        public void CreatePacketFromList(List<Object> objList)
        { dataProvider[IndexOfDataprovider].SaveListToPacket(objList); }
        public bool CheckCurrentSerializeFile()
        {
            if (dataProvider[IndexOfDataprovider].CheckFile() == false)
                CreateFile();
            return dataProvider[IndexOfDataprovider].CheckFile();
        }


        public void CreateFile()
        { dataProvider[IndexOfDataprovider].CreateFile(); }
        

        public String GetCurrentFile()
        { return settings.appDir+settings.CurrentFileName; }


        public void SaveConfig()
        { JsonConfig.SaveSettings(settings); }
        private void LoadSettings()
        {
            try
            {
                settings = JsonConfig.LoadSettings();
            }
            catch (Exception ex)
            {
                JsonConfig.CreateSettings();
            }
        }
        public void LoadConfig()
        {
            LoadSettings();
            settings.RebuildSettings(); 
            IndexOfDataprovider = settings.NumCurrentFileName;
            Setsettings(settings.appDir, settings.GetCurrentFileName());
        }



        public void SetSettingsSerialization(int num)
        {
            IndexOfDataprovider = num;
            settings.SetNumCurrentFileName(num);
        }
        public void Setsettings(String appDir, String relativePath)
        { dataProvider[IndexOfDataprovider].SetFileName(appDir + relativePath); }




        public List<String> GetGroupsOfObj(List<object> objList)
        {
            List<String> groups = new List<string>();
            foreach (var obj in objList)
                groups.Add((obj as AbstractPerson).GroupCollection);
            
            return groups;
        }

        public void SetGroupToCurrentObject(String group, Object obj)
        {
            (obj as AbstractPerson).GroupCollection = group;
        }

        public List<object> RenameGroupOfObject(String newName, List<object> objList,int index)
        {
            String oldGroup = (objList[index] as AbstractPerson).GroupCollection;
            foreach(var obje in objList)
                if((obje as AbstractPerson).GroupCollection == oldGroup)
                    (obje as AbstractPerson).GroupCollection = newName;

            return objList;
        }

        public void HideGroups(List<Object> objList)
        {
            foreach (var obje in objList)
                    (obje as AbstractPerson).GroupCollection = null;
        }







        public List<Object> CreateListOfArrivalStudents_and_notArrivalStudents(List<Object> objList)
        {
            objList = objList.Where(x => x is Student && (x as Student).Course == "1").ToList();
            List<Object> studentsFromKyiv = objList.Where(x => (x as Student).ArivalCity == "Kyiv").ToList();
            List<Object> studentsNotFromKyiv = objList.Where(x => (x as Student).ArivalCity != "Kyiv").ToList();

            objList = studentsFromKyiv;
            objList.Add(null);
            if (studentsNotFromKyiv.Count > 0)
                studentsNotFromKyiv.ForEach(x => objList.Add(x));

            return objList;
        }

        public List<Object> CreateListOfArrivalStudentsAndUnsettle(List<Object> objList)
        {
            objList = objList.Where(x => x is Student && (x as Student).ArivalCity != "Kyiv" && (x as Student).Hostel == "000.00").ToList();
            return objList;
        }

        public void SettleStudentToRoomOfHostel(String roomHostel, object stud)
        {
            if (stud is Student)
                (stud as Student).Hostel = roomHostel;
        }

    }
}
