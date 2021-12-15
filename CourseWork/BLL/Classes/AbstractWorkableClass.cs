using DAL_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ProgramClasses
{
    [Serializable]
    public abstract class AbstractWorkableClass : AbstractClass
    {
        public AbstractWorkableClass()
        {
            Name = "Undefined";

            GroupCollection = "Default";
        }

        public String Name { get; set; }
   
        [JsonInclude]
        public String GroupCollection;

        [ExcludeFromCodeCoverage]
        virtual public List<String> GetObjNameProp()
        {
            //return base.GetObjNameProp();
            List<String> arrStr = new List<String>();

            arrStr.Add($"{GetType().Name}");

            foreach (var prop in typeof(AbstractWorkableClass).GetProperties())
           //     if (prop.DeclaringType == this.GetType())
                    arrStr.Add($"{prop.Name}");


            return arrStr;
        }
        override public List<String> GetObjValueProp()
        {
            //return  base.GetObjValueProp();
            List<String> arrStr = new List<String>();

            foreach (var prop in typeof(AbstractWorkableClass).GetProperties())
            {
                arrStr.Add($"{prop.GetValue(this)}");
            }

            return arrStr;
        }



        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = typeof(AbstractWorkableClass).GetProperties();

            if (info[propertyNum].Name.ToLower() == "Name".ToLower())
                    if (ChangeProperty_FirstName(value))
                        return true;

         

            return false;
        }
        private bool ChangeProperty_FirstName(String value)
        {
            bool returnVal = false;
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        Name = value;
                        returnVal= true;
                    }
            }
            return returnVal;
        }

        [ExcludeFromCodeCoverage]
        override public String HeadingOfObject()
        {return $"{this.Name}"; }
    }
}