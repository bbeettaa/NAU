using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramClasses
{
    [Serializable]
    public class Supplier : AbstractWorkableClass
    {
        public Supplier()
        {
            LastName = "Undefined";
        }

        public String LastName { get; set; }

        [ExcludeFromCodeCoverage]
        override public List<String> GetObjNameProp()
        {
            List<String> arrStr = base.GetObjNameProp();

            foreach (var prop in this.GetType().GetProperties())
            {
                arrStr.Add($"{prop.Name}");
                if (prop.DeclaringType != this.GetType())
                    arrStr.RemoveAt(arrStr.Count-1);
            }

            return arrStr;
        }
        override public List<String> GetObjValueProp()
        {
            List<String> arrStr = base.GetObjValueProp();

            foreach (var prop in this.GetType().GetProperties())
                arrStr.Add($"{prop.GetValue(this)}");

            return arrStr;
        }
        [ExcludeFromCodeCoverage]
        override public String HeadingOfObject()
        { return $"{this.Name} {this.LastName}"; }
        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = this.GetType().GetProperties();

            if (propertyNum < typeof(Supplier).GetProperties().Length)
            {
                if (propertyNum < typeof(AbstractWorkableClass).GetProperties().Length)
                    if (base.ChangeProperties(propertyNum, value))
                        return true;

                propertyNum -= typeof(AbstractWorkableClass).GetProperties().Length;


                if (info[propertyNum].Name.ToLower() == "LastName".ToLower())
                {
                    if (ChangeProperty_LastName(value))
                        return true;
                }
            }

            return false;

        }
        
        private bool ChangeProperty_LastName(String value)
        {
            bool returnVal = false;
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        LastName = value;
                        returnVal= true;
                    }
            }
            return returnVal;
        }
    }
}