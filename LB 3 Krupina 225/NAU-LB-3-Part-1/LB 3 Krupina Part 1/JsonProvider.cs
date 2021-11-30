using DAL_Classes;
using DAL_Worck_With_DataBases;
using ProgramClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL_Worck_With_DataBases
{
    class JsonProvider : IDataProvider
    {
        String fileName = "";
        List<Object> objList = new ();
        Packet packet = new ();

        //Settings//
        readonly String confDir;
        readonly String confName = "settings.json";

        public JsonProvider()
        {
            confDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            confDir = confDir.Replace(@"\bin\Debug\net5.0", "");
            int pos = confDir.LastIndexOf("\\");
            confDir = confDir.Remove(pos, confDir.Length - pos);
            confName = confDir+"\\"+confName;

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

        public void SetSettings(String fileName)
        {
            this.fileName = fileName;
            CheckFile();
        }

        public void CheckFile()
        {
            if (File.Exists(fileName))
                return;
            else
            {
                FileStream file = new (fileName, FileMode.Create);
                StreamWriter writer = new (file, Encoding.Unicode);
                writer.Close();
                file.Close();

                packet = new ();
                Serialize();
            }
        }

        public void SaveSettings(Settings settings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, };
            string jsonString = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(confName, jsonString);
        }
        public Settings LoadSettings()
        {
            CheckConfig();
            string jsonString = File.ReadAllText(confName);
            Settings settings = JsonSerializer.Deserialize<Settings>(jsonString);

            return settings;
        }
        public void CheckConfig()
        {
            if (File.Exists(confName))
                return;
            else
            {
                FileStream file = new(confName, FileMode.Create);
                StreamWriter writer = new(file, Encoding.Unicode);
                writer.Close();
                file.Close();

                Settings settings = new();
                SaveSettings(settings);
            }
        }
    }
}
