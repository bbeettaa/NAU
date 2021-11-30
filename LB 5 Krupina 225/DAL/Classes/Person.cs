using System;
using System.Reflection;

namespace ProgramClasses
{
    [Serializable]
    public class Person :  AbstractPerson
    {
        override public String[] GetObjNameProp()
        {
            String[] arrStr = base.GetObjNameProp();

            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.DeclaringType == this.GetType())
                {
                    Array.Resize(ref arrStr, arrStr.Length + 1);
                    arrStr[arrStr.Length - 1] = $"{prop.Name}";
                }
            }

            return arrStr;
        }
        override public String[] GetObjValueProp()
        {
            String[] arrStr = base.GetObjValueProp();

            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.DeclaringType == this.GetType())
                {
                    Array.Resize(ref arrStr, arrStr.Length + 1);
                    arrStr[arrStr.Length - 1] = $"{prop.GetValue(this)}";
                }
            }

            return arrStr;
        }

        override public bool ChangeProperties(int propertyNum, String value)
        {

            PropertyInfo[] info = this.GetType().GetProperties();

            if (propertyNum > info.Length)
                return false;

            if (propertyNum < typeof(Person).GetProperties().Length)
            {
                if (base.ChangeProperties(propertyNum, value))
                    return true;
            }
            else
            {
                propertyNum -= typeof(Person).GetProperties().Length;

                if (info[propertyNum].DeclaringType == this.GetType())
                    info[propertyNum].SetValue(this, value);
                return true;
            }
            return false;
        }
    }
}
