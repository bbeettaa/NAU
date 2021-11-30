using DAL_Classes;
using System;
using System.Reflection;

namespace ProgramClasses
{
    [Serializable]
    public class BasicClass : AbstractcClass
    {
        public BasicClass() : base() { }
        override public String[] GetObjInfo()
        {
            String[] arrStr = base.GetObjInfo();

            

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
