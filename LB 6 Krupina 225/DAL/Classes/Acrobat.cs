using System;
using System.Reflection;
using System.Text.RegularExpressions;
using DAL.Classes;
using ProgramInterfaces;

namespace ProgramClasses
{
    [Serializable]
    public class Acrobat : Person , IDancable , IGetDriverLicense
    {
        public Acrobat() : base()
        {
            AcrobatTechnique = "Flip";
            FavoriteDance = "Unknown";
            DriverLicense = "00000-000";
            Age = "18";
        }


        public String AcrobatTechnique { get; set; }
        public String FavoriteDance { get ; set ; }
        public string Age { get; set; }
        public string DriverLicense { get; set; }


        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = this.GetType().GetProperties();

            if (propertyNum < 0)
                return false;


            if (propertyNum < typeof(Person).GetProperties().Length)
            {
                if (base.ChangeProperties(propertyNum, value))
                    return true;
            }
            else if (propertyNum > typeof(Acrobat).GetProperties().Length)
                return false;
            else
            {
                propertyNum -= typeof(Person).GetProperties().Length;

                if (info[propertyNum].Name.ToLower() == "License".ToLower())
                {
                    if (ChangeProperty_License(value))
                        return true;
                }
                else if (propertyNum < typeof(Acrobat).GetProperties().Length)
                {
                    PropertyInfo[] propInfo = typeof(Acrobat).GetProperties();
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
        }


        public void Operation_Object_Dance()
        {
            Random rand = new Random();
            switch (rand.Next(4))
            {
                case 1:
                    FavoriteDance = "Acrobatic Dance";
                    break;

                case 2:
                    FavoriteDance = "Flak Dance";
                    break;

                case 3:
                    FavoriteDance = "Tango";
                    break;
            }
        }

        public void Operation_Object_Get_Driver_License()
        {
            if (int.Parse(this.Age) >= 18 && this.DriverLicense == "00000-000")
            {
                Random rand = new Random(int.Parse(DateTime.Now.ToString().Replace(".", "").Replace(" ", "").Replace(":", "").Remove(3, 4)));

                ChangeProperty_License(rand.Next(10000000, 99999999).ToString());
            }
        }

        private bool ChangeProperty_License(String value)
        {
            value = value.Replace("-", "");
            value += "000000000";
            value = value.Remove(8);

            string pattern = @"(?<first>[0-9]{5})(?<second>[0-9]{3})";


            if (Regex.IsMatch(value, pattern))
            {
                value = Regex.Replace(value, pattern, "${first}-${second}");
                DriverLicense = value;
                return true;
            }
            pattern = @"^[A-z]{0,16}?$";
            if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
            {
                value = "000000000";
                value = value.Remove(8);
                DriverLicense = value;
                return true;
            }
            return false;
        }

        public void Operation_Object_Show_Technique()
        {
            Random rand = new Random();
            switch (rand.Next(5))
            {
                case 1:
                    AcrobatTechnique = "Acrobatic jumps";
                    break;

                case 2:
                    AcrobatTechnique = "Flak";
                    break;

                case 3:
                    AcrobatTechnique = "Rondat";
                    break;

                case 4:
                    AcrobatTechnique = "Somersault";
                    break;

            }
        }
    }
}