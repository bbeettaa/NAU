using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DAL_Classes
{
    [Serializable]
    public class Settings : AbstractClass
    {
        public String appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public String JsonFileName { get; set; } = "\\JsonDataBase.json";
        public String XmlFileName { get; set; } = "\\XmlDataBase.xml";
        public String BinaryFileName { get; set; } = "\\BinaryDataBase.dat";
        public String CustomFileName { get; set; } = "\\CustomDataBase.van";
        [JsonIgnore]
        private String CurrentDir = "DataBases";
        [JsonIgnore]
        public String CurrentFileName = "";
        [JsonInclude]
        public int NumCurrentFileName = 0;

        [NonSerialized]
        public List<String> fileNames = new List<String>();
        public Settings()
        { RebuildSettings(); }
        public void SetNumCurrentFileName(int num)
        { 
            this.NumCurrentFileName = num;
            CurrentFileName = fileNames[NumCurrentFileName];
        }
        public String GetCurrentFileName()
        { return CurrentFileName ; }
        public void RebuildSettings()
        {
            if (CurrentFileName == null) 
                CurrentFileName = JsonFileName;

            fileNames = new List<String>
            {
                JsonFileName,
                XmlFileName,
                BinaryFileName,
                CustomFileName
            };

            CurrentFileName = fileNames[NumCurrentFileName];

            appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            appDir = appDir.Replace("\\bin\\Debug\\net5.0", "");
            appDir = appDir.Replace("\\bin\\Debug","");
            int pos = appDir.LastIndexOf("\\");
            appDir=appDir.Remove(pos,appDir.Length-pos) + "\\";
            appDir += CurrentDir;
        }
        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = this.GetType().GetProperties();
            bool setCurrentFile = false;

            if (propertyNum > info.Length)
                return false;
            else
            {
                if (info[propertyNum].Name.ToLower().Contains("Json".ToLower()))
                {

                    if (ChangeProperty_JSONFileName(value))
                        return true;
                    
                }
                if (info[propertyNum].Name.ToLower().Contains("Xml".ToLower()))
                {

                    if (ChangeProperty_XmlFileName(value))
                        return true;
                    
                }
                if (info[propertyNum].Name.ToLower().Contains("Binary".ToLower()))
                {
                    if (ChangeProperty_BinaryFileName(value))
                        return true;
                    
                }
                if (info[propertyNum].Name.ToLower().Contains("Custom".ToLower()))
                {
                    if (ChangeProperty_CustomFileName(value))
                        return true;
                    
                }
            }

            return false;
        }
        private bool ChangeProperty_JSONFileName(String value)
        {
            bool retVal = false;
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        JsonFileName = value + ".json";
                        retVal = true;
                    }
            }
            return retVal;
        }
        private bool ChangeProperty_XmlFileName(String value)
        {
            bool retVal = false;
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        XmlFileName = value + ".xml";
                        retVal = true;
                    }
            }
            return retVal;
        }
        private bool ChangeProperty_BinaryFileName(String value)
        {
            bool retVal = false;
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        BinaryFileName = value + ".dat";
                        retVal = true;
                    }
            }
            return retVal;
        }
        private bool ChangeProperty_CustomFileName(String value)
        {
            bool retVal = false;
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        CustomFileName = value + ".van";
                        retVal = true;
                    }
            }
            return retVal;
        }

    }
}
