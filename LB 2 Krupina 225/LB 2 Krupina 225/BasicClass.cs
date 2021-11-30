using System;
using System.Reflection;

namespace ProgramClasses
{
    class BasicClass : AbstractBasicClass
    {
        public BasicClass() : base() { }
        override public String[] GetObjInfo()
        {
            String[] arrStr = base.GetObjInfo();

            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.DeclaringType == this.GetType())
                {
                    Array.Resize(ref arrStr, arrStr.Length + 1);
                    arrStr[arrStr.Length - 1] = $"{prop.Name}".PadRight(20) + $" {prop.GetValue(this)}";
                }
            }

            return arrStr;
        }
        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = this.GetType().GetProperties();

            if (propertyNum < typeof(BasicClass).GetProperties().Length)
                base.ChangeProperties(propertyNum, value);
            else
            {
                propertyNum -= typeof(BasicClass).GetProperties().Length;

                if (info[propertyNum].DeclaringType == this.GetType())
                {
                    info[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
        }
    }
}
