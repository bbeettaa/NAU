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
            if (CurrentFileName == null) CurrentFileName = JsonFileName;

            fileNames = new List<String>
            {
                JsonFileName,
                XmlFileName,
                BinaryFileName,
                CustomFileName
            };

            CurrentFileName = fileNames[NumCurrentFileName];

            //"C:\\Users\\bedu_s_bashkoy\\source\\repos\\LB 3 Krupina 225\\MainProgram\\bin\\Debug\\net5.0"
            appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            appDir = appDir.Replace("\\bin\\Debug\\net5.0", "");
            int pos = appDir.LastIndexOf("\\");
            appDir=appDir.Remove(pos,appDir.Length-pos) + "\\";
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
                    if (CurrentFileName == info[propertyNum].GetValue(this).ToString())
                        setCurrentFile = true;

                    if (ChangeProperty_JSONFileName(value))
                    {
                        if(setCurrentFile)
                            CurrentFileName = JsonFileName;
                        return true;
                    }
                }
                if (info[propertyNum].Name.ToLower().Contains("Xml".ToLower()))
                {
                    if (CurrentFileName == info[propertyNum].GetValue(this).ToString())
                        setCurrentFile = true;

                    if (ChangeProperty_XmlFileName(value))
                    {
                        if (setCurrentFile)
                            CurrentFileName = XmlFileName;
                        return true;
                    }
                }
                if (info[propertyNum].Name.ToLower().Contains("Binary".ToLower()))
                {
                    if (CurrentFileName == info[propertyNum].GetValue(this).ToString())
                        setCurrentFile = true;

                    if (ChangeProperty_BinaryFileName(value))
                    {
                        if (setCurrentFile)
                            CurrentFileName = BinaryFileName;
                        return true;
                    }
                }
                if (info[propertyNum].Name.ToLower().Contains("Custom".ToLower()))
                {
                    if (CurrentFileName == info[propertyNum].GetValue(this).ToString())
                        setCurrentFile = true;

                    if (ChangeProperty_CustomFileName(value))
                    {
                        if (setCurrentFile)
                            CurrentFileName = BinaryFileName;
                        return true;
                    }
                }
                else if (propertyNum < typeof(Settings).GetProperties().Length)
                {
                    PropertyInfo[] propInfo = typeof(Settings).GetProperties();
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
        }
        private bool ChangeProperty_JSONFileName(String value)
        {
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        JsonFileName = value + ".json";
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_XmlFileName(String value)
        {
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        XmlFileName = value + ".xml";
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_BinaryFileName(String value)
        {
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        BinaryFileName = value + ".dat";
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_CustomFileName(String value)
        {
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        CustomFileName = value + ".van";
                        return true;
                    }
            }
            return false;
        }
        override public String HeadingOfObject()
        { return $"Зміна файлу Конфігураціїї"; }

        /*override public String[] GetObjNameProp()
        {
            String[] arrStr = Array.Empty<string>();

            Array.Resize(ref arrStr, arrStr.Length + 1);
            arrStr[0] = $"\nCurrent file: ".PadRight(25)+$"{this.GetType().GetField("CurrentFileName").GetValue(this)}";
            foreach (var prop in this.GetType().GetProperties())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[arrStr.Length-1] = $"{prop.Name}" + "\t" + $" {prop.GetValue(this)}";
            }

            return arrStr;
        }*/
        public static void Set_Database_Object_To_Json() { }
        public static void Set_Database_Object_To_Xml() { }
        public static void Set_Database_Object_To_Binary() { }
        public static void Set_Database_Object_To_Custom() { }
    }
}
