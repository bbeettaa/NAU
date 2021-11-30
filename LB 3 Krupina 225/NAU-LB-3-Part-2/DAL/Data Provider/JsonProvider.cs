using DAL_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DALWorckWithDataBases
{
    class JsonProvider : IDataProvider
    {
        String fileName = "";
        List<Object> objList = new ();
        Packet packet = new ();

        //Settings//
        readonly String configDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        readonly String configName = "\\settings.json";

        public JsonProvider()
        {
            //configDir = configDir.Replace("Program\\bin\\Debug\\net5.0", "");


            configDir = configDir.Replace("\\bin\\Debug\\net5.0", "");
            int pos = configDir.LastIndexOf("\\");
            configDir= configDir.Remove(pos, configDir.Length - pos);

            configName = configDir + configName;
        }
        public void Serialize()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, };
            string jsonString = JsonSerializer.Serialize(packet, options);
            File.WriteAllText(fileName, jsonString);
        }
        public List<Object> Deserialize()
        {
            string jsonString = File.ReadAllText(fileName);
            packet = JsonSerializer.Deserialize<Packet>(jsonString);

            objList = packet.GetList();
            return objList;
        }
        public void SaveListToPacket(List<Object> objList)
        {
            packet.Reset();
            foreach (var obj in objList)
                packet.AddToPacket(obj);
        }

        public void SetFileName(String fileName)
        {
            this.fileName = fileName;
            CheckFile();
        }



        public bool CheckFile()
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            return true;
        }
        public void CreateFile()
        {
            FileStream file = new(fileName, FileMode.Create);
            StreamWriter writer = new(file, Encoding.Unicode);
            writer.Close();
            file.Close();

            packet = new Packet();
            Serialize();
        }




        public void SaveSettings(Settings settings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, };
            string jsonString = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(configName, jsonString);
        }
        public Settings LoadSettings()
        {
            string jsonString = File.ReadAllText(configName);
            Settings settings = JsonSerializer.Deserialize<Settings>(jsonString);

            return settings;
        }
    }
}
