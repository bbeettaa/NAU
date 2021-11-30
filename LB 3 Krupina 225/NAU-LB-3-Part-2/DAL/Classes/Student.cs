using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProgramInterfaces;

namespace ProgramClasses
{
    [Serializable]
    public class Student : Person, IDancable
    {
        public Student() : base()
        {
            StudentID = "-1";
            Course = "0";
            FavoriteDance = "Unknown";
        }
        public String StudentID { get; set; }
        public String Course { get; set; }
        public String FavoriteDance { get; set; }
        public void Transfer_Next_Object_To_The_Next_Сourse()
        {
            int tempCourse = int.Parse(Course) + 1;
            Course = tempCourse.ToString();
        }
        public void Operation_Object_Dance()
        {
            if (FavoriteDance != "Not predisposed to")
            {
                Random rand = new();
                switch (rand.Next(3))
                {
                    case 1:
                        FavoriteDance = "StreetDance";
                        break;

                    case 2:
                        FavoriteDance = "Not predisposed to";
                        break;

                }
            }
        }
        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = this.GetType().GetProperties();

            if (propertyNum < typeof(Person).GetProperties().Length)
            {
                if(base.ChangeProperties(propertyNum, value))
                return true;
            }
            else if (propertyNum > typeof(Student).GetProperties().Length)
                return false;
            else
            {
                propertyNum -= typeof(Person).GetProperties().Length;

                if (info[propertyNum].Name.ToLower() == "StudentID".ToLower())
                {
                    if (ChangeProperty_StudentID(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "Course".ToLower())
                {
                    if (ChangeProperty_Course(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "FavoriteDance".ToLower())
                {
                    if (ChangeProperty_FavoriteDance(value))
                        return true;
                }
                else if (propertyNum < typeof(Student).GetProperties().Length)
                {
                    PropertyInfo[] propInfo = typeof(Student).GetProperties();
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
        }
        private bool ChangeProperty_StudentID(String value)
        {
            if (value.Length <= 8 && value.Length >= 0)
            {
                value += "000000000";
                value = value.Remove(8);

                string pattern = @"(?<first>[0-9]{5})(?<second>[0-9]{3})";

                if (Regex.IsMatch(value, pattern))
                {
                    value = Regex.Replace(value, pattern, "${first}-${second}");
                    StudentID = value;
                    return true;
                }
            }
            return false;
        }
        private bool ChangeProperty_Course(String value)
        {
            if (value.Length <= 8 && value.Length >= 0)
            {
                value += "00";
                value = value.Remove(1);
                string pattern = @"^[0-6]{1}?$";

                if (value.Length <= 8)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        Course = value;
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_FavoriteDance(String value)
        {
            if (value.Length <= 16)
                if (Regex.IsMatch(value, @"^[\sA-z,]{0,16}?$"))
                {
                    FavoriteDance = value;
                    return true;
                }
            return false;
        }
    }
}