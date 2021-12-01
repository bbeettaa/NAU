using ProgramClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace DAL_Classes
{
    [Serializable]
    public abstract class AbstractClass
    {
        virtual public String[] GetObjNameProp()
        {
            String[] arrStr = Array.Empty<string>();

            Array.Resize(ref arrStr, arrStr.Length + 1);
            arrStr[0] = $"Tип об'єкту".PadRight(23) + $" {GetType().Name}";

            foreach (var prop in this.GetType().GetProperties())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[arrStr.Length - 1] = $"{prop.Name}".PadRight(0) + $"\t {prop.GetValue(this)}";
            }

            return arrStr;
        }
        virtual public String[] GetObjValueProp()
        {
            String[] arrStr = Array.Empty<string>();

            Array.Resize(ref arrStr, arrStr.Length + 1);
            arrStr[0] = $"Tип об'єкту".PadRight(23) + $" {GetType().Name}";

            foreach (var prop in this.GetType().GetProperties())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[arrStr.Length - 1] = $"{prop.Name}".PadRight(0) + $"\t {prop.GetValue(this)}";
            }

            return arrStr;
        }


        virtual public String[] GetMethodsInfo()
        {
            String[] arrStr = Array.Empty<string>();

            foreach (var prop in this.GetType().GetMethods())
            {
                Array.Resize(ref arrStr, arrStr.Length + 1);
                arrStr[arrStr.Length - 1] = prop.Name;
            }

            arrStr = (from x in arrStr where x.Contains("_Object_") select x).ToArray();

            return arrStr;
        }
       
        virtual public bool ChangeProperties(int propertyNum, String value)
        {
            if (propertyNum > this.GetType().GetProperties().Length)
                return false;
            else
            {
               if (propertyNum < this.GetType().GetProperties().Length)
                {
                    PropertyInfo[] propInfo = this.GetType().GetProperties();
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
        }
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
            list = (from x in list where !typeof(AbstractPerson).Name.Contains(x.Name) select x).ToList();
            list = (from x in list where !typeof(Person).Name.Contains(x.Name) select x).ToList();
            list = (from x in list where !x.Name.Contains("<>c") select x).ToList();

            return list;
        }
        virtual public String HeadingOfObject()
        {
            return $"jjj";
        }
    }
}
