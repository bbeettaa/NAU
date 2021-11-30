using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgramClasses
{
    class Person :  AbstractPerson
    {
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
        override public void ChangeProperties(int propertyNum, String value)
        {
            propertyNum--;

            PropertyInfo[] info = this.GetType().GetProperties();

            if (propertyNum < typeof(Person).GetProperties().Length)
                base.ChangeProperties(propertyNum, value);
            else
            {
                propertyNum -= typeof(Person).GetProperties().Length;

                if (info[propertyNum].DeclaringType == this.GetType())
                    info[propertyNum].SetValue(this, value);

            }
        }
    }

    class Test : Person 
    {
        public Test() : base()
        {
            TestStr = "test";
        }
        public void Operation_Object_Lalal()
        {
            TestStr = "LaLa";
        }
    public String TestStr { get; set; }
    }
}
