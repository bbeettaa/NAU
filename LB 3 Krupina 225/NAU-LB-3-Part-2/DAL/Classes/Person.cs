using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ProgramClasses
{
    [Serializable]
    public class Person :  AbstractPerson
    {
        override public String[] GetObjInfo()
        {
            String[] arrStr = base.GetObjInfo();

            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.DeclaringType == this.GetType())
                {
                    Array.Resize(ref arrStr, arrStr.Length + 1);
                    arrStr[^1] = $"{prop.Name}".PadRight(20) + $" {prop.GetValue(this)}";
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
