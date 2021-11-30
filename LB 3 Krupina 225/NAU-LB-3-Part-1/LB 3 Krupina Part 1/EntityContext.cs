using System;
using System.Collections.Generic;
using System.Linq;
using ProgramClasses;
using DAL_Classes;
using DAL_Worck_With_DataBases;

namespace NamespaceEntityContext
{
    public class EntityContext
    {
        //String fileName;
        //Type[] typesArr = new Type[] { };
        List<Object> objList = new();
        readonly List<IDataProvider> dataProvider = new();
        public Settings settings = new ();
        public int IndexOfDataprovider { get; set; } = 0;
        readonly JsonProvider JsonConfig = new ();

        public EntityContext()
        {
            JsonProvider Json = new();
            dataProvider.Add(Json);
            XML_Provider xml = new();
            dataProvider.Add(xml);
            BinaryProvider binary = new();
            dataProvider.Add(binary);
            CustomProvider custom = new();
            dataProvider.Add(custom);
        }



        public static List<String> GetObjNames(List<Object> objList)
        {
            List<Type> typesArr = AbstractcClass.GetAssemblyTypes();
            List<String> str = new();

            foreach (var obj in objList)
            {
                foreach (var t in typesArr)
                {
                    if (obj.GetType().Name == t.Name)
                        str.Add((obj as AbstractcClass).HeadingOfObject());
                }
            }

            return str;
        }
        public static String GetObjectHeading(Object obj)
        { return (obj as AbstractcClass).HeadingOfObject(); }
        public static List<String> GetObjectInfo(Object obj)
        { return (obj as AbstractcClass).GetObjInfo().ToList<String>(); }
        public static List<String> GetMethodsInfo(Object obj)
        { return (obj as AbstractcClass).GetMethodsInfo().ToList<String>(); }



        public List<Object> FindObjects(String find)
        {
            objList = Deserialize();

            List<Object> list = new();
            foreach (var obj in objList)
                if ((obj as AbstractcClass) != null &&
                    (obj as AbstractcClass).IsFindInfo(find) == true)
                    list.Add(obj);
            objList = list;
            return objList;
        }
        public List<Object> Deserialize()
        {
            settings.SetNumCurrentFileName(IndexOfDataprovider);
            RebuildSettings();
            objList = dataProvider[IndexOfDataprovider].Deserialize();
            return objList;
        }
        public static bool CheckInputInfo(String inputData, int propertyNum, Object obj)
        {
            if ((obj as AbstractcClass).ChangeProperties(propertyNum - 1, inputData))
                return true;
            return false;
        }
        public static List<Type> GetAssemblyTypes()
        { return AbstractcClass.GetAssemblyTypes(); }




        public void SavePacketIntoDatabase()
        {
            CreatePacketFromList(objList);

                Setsettings(settings.appDir, settings.fileNames[IndexOfDataprovider]);
                dataProvider[IndexOfDataprovider].CheckFile();
                dataProvider[IndexOfDataprovider].Serialize();
        }
        public void CreatePacketFromList(List<Object> ObjList)
        {
            dataProvider[IndexOfDataprovider].SaveListToPacket(ObjList); 
        }
        private void RebuildSettings()
        { Setsettings(settings.appDir, settings.GetCurrentFileName()); }



        public void SaveConfig()
        { JsonConfig.SaveSettings(settings); }
        private void LoadSettings()
        { settings = JsonConfig.LoadSettings(); }
        public void LoadConfig()
        {
            LoadSettings();
            settings.RebuildSettings();
            IndexOfDataprovider = settings.NumCurrentFileName;
            //String fileName = settings.fileNames[indexOfDataprovider];
            //settings.RebuildSettings(fileName);
            RebuildSettings();
            //settings.SetNumCurrentFileName(indexOfDataprovider);
        }



        public void SetSettingsSerialization(int num)
        {
            IndexOfDataprovider = num;
            settings.SetNumCurrentFileName(num);
        }
        public void Setsettings(String appDir, String relativePath)
        { dataProvider[IndexOfDataprovider].SetSettings(appDir + relativePath); }
    }
}
