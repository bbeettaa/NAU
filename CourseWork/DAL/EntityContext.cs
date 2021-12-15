using System;
using System.Collections.Generic;
using System.Linq;
using ProgramClasses;
using DAL_Classes;
using System.Collections;

namespace DALWorckWithDataBases
{
    public class EntityContext
    {
        readonly List<AbstarctDataProvider> dataProvider = new List<AbstarctDataProvider>();
        public Settings settings = new Settings();
        public Packet packet;

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
            IndexOfDataprovider = settings.NumCurrentFileName;
            Setsettings(settings.appDir, settings.fileNames[IndexOfDataprovider]);
            SaveConfig();
            LoadSettings();

        }



        public String GetObjCategory(object obj)
        { return (obj as AbstractWorkableClass).GroupCollection; }

        public static List<String> GetObjNames(List<Object> objList)
        {
            List<Type> typesArr = AbstractWorkableClass.GetAssemblyTypes();
            List<String> str = new List<String>();

            foreach (var obj in objList)
            {
                foreach (var t in typesArr)
                {
                    if (obj.GetType().Name == t.Name)
                        str.Add((obj as AbstractWorkableClass).HeadingOfObject());
                }
            }

            return str;
        }
        
        
        public static List<String> GetObjNameProps(Object obj)
        { return (obj as AbstractWorkableClass).GetObjNameProp().ToList<String>(); }
        
        public bool IsObjContain(String find, Object obj)
        {
            if ((obj as AbstractClass).IsFindInfo(find))
                return true;
            return false;
        }




        virtual public void Deserialize()
        {
            try
            {
                packet = dataProvider[IndexOfDataprovider].Deserialize();
            }
            catch(System.IO.FileNotFoundException)
            {
                dataProvider[IndexOfDataprovider].CreateDir();
                dataProvider[IndexOfDataprovider].CreateFile();
                Deserialize();
            }


        }
        virtual public List<Object> GetProductObjects() 
        {  return packet.GetProductObj(); }
        virtual public List<Object> GetSupplierObjects()
        { return packet.GetSupplierObj(); }

        public static bool CheckInputInfo(String inputData, int propertyNum, Object obj)
        {
            if ((obj as AbstractClass).ChangeProperties(propertyNum, inputData))
                return true;
            return false;
        }
        public static List<Type> GetAssemblyTypes()
        { return AbstractWorkableClass.GetAssemblyTypes(); }

        public String GetObjValueProp(int propNum, object obj)//!
        {
            try
            {
                return (obj as AbstractClass).GetObjValueProp()[propNum];
            }
            catch
            {
                return null;
            }

        }
        public List<String> GetObjValueProp(Object obj)
        { return (obj as AbstractClass).GetObjValueProp().ToList<String>(); }





        public static Object CreateObject(Type type)
        {
            Object obj = Activator.CreateInstance(type);
            return obj;
        }
       virtual  public void SavePacketIntoDatabase(List<object> objList, List<object> objList1)
        {
            packet.Reset();
            dataProvider[IndexOfDataprovider].CheckFile();
            CreatePacketFromList(objList);
            CreatePacketFromList(objList1);
            dataProvider[IndexOfDataprovider].Serialize(); 
        }
        public void CreatePacketFromList(List<Object> objList)
        { dataProvider[IndexOfDataprovider].SaveListToPacket(objList); }
       


        public void SaveConfig()
        { JsonConfig.SaveSettings(settings); }
        private void LoadSettings()
        {
            try
            {
                settings = JsonConfig.LoadSettings();
            }
            catch
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



        public void SetNumCurrentFileName(int num)
        {
            IndexOfDataprovider = num;
            Setsettings(settings.appDir, settings.fileNames[IndexOfDataprovider]);

            settings.SetNumCurrentFileName(num);
            SaveConfig();
        }
        public void Setsettings(String appDir, String relativePath)
        { dataProvider[IndexOfDataprovider].SetFileName(appDir + relativePath); }




        public List<String> GetGroupsOfObj(List<object> objList)
        {
            List<String> groups = new List<string>();
            foreach (var obj in objList)
                groups.Add((obj as AbstractWorkableClass).GroupCollection);
            
            return groups;
        }

        public void SetGroupToCurrentObject(String group, Object obj)
        {
            (obj as AbstractWorkableClass).GroupCollection = group;
        }

        public List<object> RenameGroupOfObject(String newName, List<object> objList,int index)
        {
            String oldGroup = (objList[index] as AbstractWorkableClass).GroupCollection;
            foreach(var obje in objList)
                if((obje as AbstractWorkableClass).GroupCollection == oldGroup)
                    (obje as AbstractWorkableClass).GroupCollection = newName;

            return objList;
        }

        public List<object> RenameGroupOfObject(String newName, String oldName, List<object> objList)
        {
            foreach (var obje in objList)
                if ((obje as AbstractWorkableClass).GroupCollection == oldName)
                    (obje as AbstractWorkableClass).GroupCollection = newName;

            return objList;
        }



        public String GetObjName(Object obj) 
        {return (obj as AbstractWorkableClass).Name;}

        public String GetObjBrand(Object obj)
        {return (obj as Product).Brand; }

        public String GetObjPrice(Object obj)
        { return (obj as Product).Price;}

        public String GetObjLastName(Object obj)
        {return (obj as Supplier).LastName;}



        public List<String> GetNameSettings()
        { return settings.GetObjValueProp().ToList(); }

        public int GetSerializeNum()
        { return settings.NumCurrentFileName; }
    }
}
