using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ProgramClasses
{
    abstract class AbstractBasicClass : IComparable<AbstractBasicClass>, IEnumerable
    {
        public AbstractBasicClass()
        {
            Name = "Unknown";
        }
        public String Name { get; set; }

        public int CompareTo(AbstractBasicClass other)
        {
            int index = 0;
            if (this.Name.Length < other.Name.Length) index = this.Name.Length;
            else index = other.Name.Length;

            for (int i = 0; i < index; i++)
            {
                if (this.Name[i] < other.Name[i]) return -1;
                if (this.Name[i] > other.Name[i]) return 1;
            }
            return 0;
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        virtual public String[] GetObjInfo()
        {
            String[] arrStr = new String[] { };

            Array.Resize(ref arrStr, arrStr.Length + 1);
            arrStr[0] = $"Tип об'єкту".PadRight(23) + $" {GetType().Name}";

            foreach (var prop in typeof(AbstractBasicClass).GetProperties())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[arrStr.Length - 1] = $"{prop.Name}".PadRight(20) + $" {prop.GetValue(this)}";
            }

            return arrStr;
        }
        virtual public String[] GetMethodsInfo()
        {
            String[] arrStr = new String[] { };

            foreach (var prop in this.GetType().GetMethods())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[arrStr.Length - 1] = prop.Name;
            }

            arrStr = (from x in arrStr where x.Contains("_Object_") select x).ToArray();

            return arrStr;
        }
        public String GetDataForDatabase()
        {
            String str;
            PropertyInfo[] arrProp = this.GetType().GetProperties();
            str = $"{GetType().Name} {Name}\n" + "{";
            foreach (var prop in arrProp)
            {
                if (prop != arrProp.Last())
                    str += $"“{prop.Name.ToLower()}”: “{prop.GetValue(this)}”,\n";
                else
                    str += $"“{prop.Name.ToLower()}”: “{prop.GetValue(this)}”" + "}";
            }
            return str;
        }
        virtual public void AssignValue(String str)
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                if (str.Contains(prop.Name.ToLower()))
                    prop.SetValue(this, EraseStr(str));
            }
        }
        virtual public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] propInfo = typeof(AbstractBasicClass).GetProperties();

            if (propertyNum < typeof(AbstractBasicClass).GetProperties().Length)
                if (propInfo[propertyNum].Name.ToLower() == "Name".ToLower())
                {
                    if (ChangeProperty_Name(propertyNum, value, propInfo))
                        return true;
                }
                else if (propInfo[propertyNum].Name.ToLower() == "Name".ToLower())
                {
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            return false;
        }
        private bool ChangeProperty_Name(int propertyNum, String value, PropertyInfo[] info)
        {
            //string pattern = @"^[a-z0-9_- ]{0,32}$";

            if (value.Length <= 32)
                if (Regex.IsMatch(value, @"^[\sa-z0-9-_]{0,32}$", RegexOptions.IgnoreCase))
                {
                    info[propertyNum].SetValue(this, value);
                    return true;
                }
            return false;
        }
        public bool IsFindInfo(String str)
        {
            foreach (var prop in this.GetType().GetProperties())
                if (prop.GetValue(this).ToString().Contains(str))
                    return true;

            return false;
        }
        protected String EraseStr(String str)
        {
            String outStr = "";
            for (int i = 0; i < str.Length; i++)
                if (i > 3 && str[i - 3] == ':' && str[i - 1] == '“')
                    while (str[i] != '”')
                        outStr += str[i++];

            return outStr;
        }

        virtual public String HeadingOfObject()
        {
            return $"{this.GetType().Name} {this.Name}";
        }
    }
}
