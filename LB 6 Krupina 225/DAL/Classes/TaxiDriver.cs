using DAL.Classes;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ProgramClasses
{
    [Serializable]
    public class TaxiDriver : Person , IGetDriverLicense
    {
        public TaxiDriver() : base()
        {
            DriverLicense = "00000-000";
            CarriesPassenger = "No";
            Age = "18";
        }
        
        public String DriverLicense { get; set; }
        public String CarriesPassenger { get; set; }
        public string Age { get; set; }

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
            else if (propertyNum > typeof(TaxiDriver).GetProperties().Length)
                return false;
            else
            {
                propertyNum -= typeof(Person).GetProperties().Length;

                if (info[propertyNum].Name.ToLower() == "License".ToLower())
                {
                    if (ChangeProperty_License(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "CarriesPassenger".ToLower())
                {
                    if (ChangeProperty_CarriesPassenger(value))
                        return true;
                }
                else if (propertyNum < typeof(TaxiDriver).GetProperties().Length)
                {
                    PropertyInfo[] propInfo = typeof(TaxiDriver).GetProperties();
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
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
        private bool ChangeProperty_CarriesPassenger(String value)
        {
            if (value[value.Length-1] == 'y' || value[value.Length - 1] == 'Y')
            {
                CarriesPassenger = "Yes";
                return true;
            }
            else if (value[value.Length - 1] == 'n' || value[value.Length - 1] == 'N')
            {
                CarriesPassenger = "No";
                return true;
            }

            return false;
        }
        public void Operation_Object_Drive()
        {
            if (DriverLicense == "-1")
                DriverLicense = "UnderArrest";

            Random rand = new Random();

            if(DriverLicense != "License")
            switch (rand.Next(6))
            {
                case 1:
                    base.ArivalCity="Kyiv";
                    CarriesPassenger = "Yes";
                    break;

                case 2:
                    base.ArivalCity = "Lugansk";
                    CarriesPassenger = "Yes";
                    break;

                case 3:
                    base.ArivalCity = "Uzhorod";
                    CarriesPassenger = "No";
                    break;

                case 4:
                    base.ArivalCity = "Lviv";
                    CarriesPassenger = "Yes";
                    break;

                case 5:
                    base.ArivalCity = "Odessa";
                    CarriesPassenger = "Yes";
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
    }
}