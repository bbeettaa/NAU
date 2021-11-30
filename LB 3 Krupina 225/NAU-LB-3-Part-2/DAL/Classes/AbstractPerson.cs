using DAL_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramClasses
{
    [Serializable]
    public abstract class AbstractPerson : AbstractClass
    {
        public AbstractPerson()
        {
            FirstName = "Undefined";
            LastName = "Undefined";
            ArivalCity = "Undefined";
            PassportSer = "Undefined";
            PassportNo = "Undefined";
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String ArivalCity { get; set; }
        public String PassportSer { get; set; }
        public String PassportNo { get; set; }

        override public String[] GetObjInfo()
        {
            String[] arrStr = Array.Empty<string>();

            Array.Resize(ref arrStr, arrStr.Length + 1);
            arrStr[0] = $"Tип об'єкту".PadRight(23) + $" {GetType().Name}";

            foreach (var prop in typeof(Person).GetProperties())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[^1] = $"{prop.Name}".PadRight(20) + $" {prop.GetValue(this)}";
            }

            return arrStr;
        }
        override public String[] GetMethodsInfo()
        {
            String[] arrStr = Array.Empty<string>();

            foreach (var prop in this.GetType().GetMethods())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[^1] = prop.Name;
            }

            arrStr = (from x in arrStr where x.Contains("_Object_") select x).ToArray();

            return arrStr;
        }

        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = typeof(AbstractPerson).GetProperties();

            if (propertyNum < 0)
                return false;

            if (propertyNum > typeof(AbstractPerson).GetProperties().Length)
                return false;
            else
            {
                //propertyNum -= typeof(AbstractPerson).GetProperties().Length;

                if (info[propertyNum].Name.ToLower() == "FirstName".ToLower())
                {
                    if (ChangeProperty_FirstName(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "LastName".ToLower())
                {
                    if (ChangeProperty_LastName(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "ArivalCity".ToLower())
                {
                    if (ChangeProperty_ArivalCity(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "PassportSer".ToLower())
                {
                    if (ChangeProperty_PassportSer(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "PassportNo".ToLower())
                {
                    if (ChangeProperty_PassportNo(value))
                        return true;
                }
                else if (propertyNum < typeof(Student).GetProperties().Length)
                {
                    PropertyInfo[] propInfo = typeof(Person).GetProperties();
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
        }
        private bool ChangeProperty_FirstName(String value)
        {
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        FirstName = value;
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_LastName(String value)
        {
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        LastName = value;
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_ArivalCity(String value)
        {
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        ArivalCity = value;
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_PassportSer(String value)
        {
            value += "000000000";
            value = value.Remove(8);

            string pattern = @"^[0-9]{8}?$";

            if (value.Length <= 8)
                if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                {
                    PassportSer = value;
                    return true;
                }
            return false;
        }
        private bool ChangeProperty_PassportNo(String value)
        {
            value += "000000000";
            value = value.Remove(8);

            string pattern = @"(?<first>[0-9]{5})(?<second>[0-9]{3})";

            if (Regex.IsMatch(value, pattern))
            {
                value = Regex.Replace(value, pattern, "${first}-${second}");
                PassportNo = value;
                return true;
            }
            return false;
        }

        override public String HeadingOfObject()
        {
            return $"{this.GetType().Name} {this.FirstName} {this.LastName}";
        }
    }
}