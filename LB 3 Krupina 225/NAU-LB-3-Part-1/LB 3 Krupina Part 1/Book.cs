using DAL_Classes;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ProgramClasses
{
    [Serializable]
    public class Book : BasicClass
    {
        public Book() : base()
        {
            YearOfPublication = "Undefined";
            SerialNumber = "Undefined";
            Price = "Undefined";
            NumberOfSamples = "Undefined";
        }

        public void Increase_The_Object_Cost_By_A_Certain_Percent(double percent)
        {
            string tempPrice = this.Price.Remove(this.Price.Length - 2);
            double num = double.Parse(tempPrice);

            double temp = percent*(num / 100);
            double result = num + temp;
            ChangeProperty_Price(result.ToString());
        }
        /*public void Test_Object_Method()
        {
            ChangeProperty_NumberOfSamples("9876");
        }*/
        public String Name { get; set; }
        public String YearOfPublication { get; set; }
        public String SerialNumber { get; set; }
        public String Price { get; set; }
        public String NumberOfSamples { get; set; }
        public String TotalEditionCost { get; set; }

        override public String HeadingOfObject()
        {
            return $"{this.GetType().Name} {this.Name} {this.YearOfPublication}";
        }
        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = this.GetType().GetProperties();

            if (propertyNum < typeof(BasicClass).GetProperties().Length)
            {
                base.ChangeProperties(propertyNum, value);
                return true;
            }
            else if (propertyNum > typeof(Book).GetProperties().Length)
                return false;
            else
            {
                propertyNum -= typeof(BasicClass).GetProperties().Length;

                if (info[propertyNum].Name.ToLower() == "YearOfPublication".ToLower())
                {
                    if (ChangeProperty_YearOfPublication(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "SerialNumber".ToLower())
                {
                    if (ChangeProperty_SerialNumber(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "Price".ToLower())
                {
                    if (ChangeProperty_Price(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "NumberOfSamples".ToLower())
                {
                    if (ChangeProperty_NumberOfSamples(value))
                        return true;
                }
                else if (info[propertyNum].Name.ToLower() == "TotalEditionCost".ToLower())
                {
                    if(ChangeProperty_TotalEditionCost(value))
                        return true;
                }
                else if (propertyNum < typeof(BasicClass).GetProperties().Length)
                {
                    PropertyInfo[] propInfo = typeof(AbstractcClass).GetProperties();
                    propInfo[propertyNum].SetValue(this, value);
                    return true;
                }
            }

            return false;
        }
        private bool ChangeProperty_YearOfPublication(String value)
        {
            if (value.Length <= 8 && value.Length >= 0)
            {
                value += "000000000";
                value = value.Remove(8);

                string pattern = @"(?<day>[0-3]{1})(?<day1>[0-9]{1})(?<month>[0-1]{1})(?<month1>[0-9]{1})(?<year>\d{4})";

                if (Regex.IsMatch(value, pattern))
                {
                    value = Regex.Replace(value, pattern, "${day}${day1}-${month}${month1}-${year}");
                    YearOfPublication=value;
                    return true;
                }
            }
            return false;
        }
        private bool ChangeProperty_SerialNumber( String value)
        {
            if (value.Length <= 8 && value.Length >= 0)
            {
                value += "000000000";
                value = value.Remove(8);
                string pattern = @"^[0-9]{0,8}?$";

                if (value.Length <= 8)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        SerialNumber=value;
                        return true;
                    }
            }
            return false;
        }
        private bool ChangeProperty_Price(String value)
        {
            string pattern = @"^[0-9,]{0,8}?$";
            
            if(Regex.IsMatch(value, @",[0-9]{3,}"))
            value = value.Substring(0, value.LastIndexOf(',') + 3);

            if (value.Length <= 8)
                if (Regex.IsMatch(value, pattern))
                {
                    Price = value + " ₴";
                    return true;
                }
            return false;
        }
        private bool ChangeProperty_NumberOfSamples( String value)
        {
            string pattern = @"^[0-9]{0,8}?$";

            if (value.Length <= 5)
                if (Regex.IsMatch(value, pattern))
                {
                    NumberOfSamples=value;
                    return true;
                }
            return false;
        }
        private bool ChangeProperty_TotalEditionCost(String value)
        {
            string pattern = @"^[0-9,]{0,8}?$";

            if (Regex.IsMatch(value, @",[0-9]{3,}"))
                value = value.Substring(0, value.LastIndexOf(',') + 3);

            if (value.Length <= 8)
                if (Regex.IsMatch(value, pattern))
                {
                    TotalEditionCost = value + " ₴";
                    return true;
                }
            return false;
        }
    }
   
}