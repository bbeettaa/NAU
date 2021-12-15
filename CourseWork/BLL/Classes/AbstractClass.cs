using ProgramClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;


namespace DAL_Classes
{
    [Serializable]
    public abstract class AbstractClass
    {
        virtual public List<String> GetObjValueProp()
        {
            List<String> arrStr = new List<String>();

            foreach (var prop in this.GetType().GetProperties())
                arrStr.Add( $"{prop.GetValue(this)}");

            return arrStr;
        }


        abstract public bool ChangeProperties(int propertyNum, String value);
        virtual public bool IsFindInfo(String str)
        {
            foreach (var prop in this.GetType().GetProperties())
                if (prop.GetValue(this).ToString().Contains(str))
                    return true;

            return false;
        }
       
        public static List<Type> GetAssemblyTypes()
        {
            List<Type> list = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "ProgramClasses").ToList();

            list = (from x in list where !typeof(AbstractClass).Name.Contains(x.Name) select x).ToList();
            list = (from x in list where !typeof(AbstractWorkableClass).Name.Contains(x.Name) select x).ToList();
            list = (from x in list where !x.Name.Contains("<>c") select x).ToList();

            return list;
        }
        [ExcludeFromCodeCoverage]
        virtual public String HeadingOfObject()
        {
            return $"jjj";
        }
    }
}
