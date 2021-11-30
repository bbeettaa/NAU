using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgramClasses
{
    abstract class AbstractPerson
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

        virtual public String[] GetObjInfo()
        {
            String[] arrStr = new String[] { };

            Array.Resize(ref arrStr, arrStr.Length + 1);
            arrStr[0] = $"Tип об'єкту".PadRight(23) + $" {GetType().Name}";

            foreach (var prop in typeof(Person).GetProperties())
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
            str = $"{GetType().Name} {FirstName}{LastName}\n" + "{";
            foreach (var prop in arrProp)
            {
                if (prop != arrProp.Last())
                    str += $"“{prop.Name.ToLower()}”: “{prop.GetValue(this)}”,\n";
                else
                    str += $"“{prop.Name.ToLower()}”: “{prop.GetValue(this)}”" + "}";
            }
            return str;
        }
        public void AssignValue(String str)
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                if (str.Contains(prop.Name.ToLower()))
                    prop.SetValue(this, EraseStr(str));
            }
        }
        virtual public void ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] propInfo = typeof(Person).GetProperties();
            propInfo[propertyNum].SetValue(this, value);
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
    }
}