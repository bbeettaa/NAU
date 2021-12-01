using DAL_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DALWorckWithDataBases
{
    class JsonProvider : AbstarctDataProvider
    {
        //Settings//
        readonly String configDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        readonly string configName = "\\settings.json";

        public JsonProvider()
        {
            configDir = configDir.Replace("Program\\bin\\Debug\\net5.0", "");

            configDir = configDir.Replace("\\bin\\Debug\\net5.0", "");
            int pos = configDir.LastIndexOf("\\");
            configDir = configDir.Remove(pos, configDir.Length - pos);

            configName = configDir + configName;
        }
        override public void Serialize()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, };
            string jsonString = JsonSerializer.Serialize(packet, options);
            File.WriteAllText(fileName, jsonString);
        }
        override public List<Object> Deserialize()
        {
            string jsonString = File.ReadAllText(fileName);
            packet = JsonSerializer.Deserialize<Packet>(jsonString);

            return packet.GetList();
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
        public void CreateSettings() 
        {

                FileStream file = new FileStream(configName, FileMode.Create);
                StreamWriter writer = new StreamWriter(file, Encoding.Unicode);
                writer.Close();
                file.Close();

            Settings settings = new Settings();
            SaveSettings(settings);

            
        }
    }
}
