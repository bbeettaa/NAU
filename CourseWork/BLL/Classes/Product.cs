using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace ProgramClasses
{
    [Serializable]
    public class Product : AbstractWorkableClass
    {
        public Product()
        {
            Brand = "Undefined";
            Price = "0.00";
            InStock = "0";
        }

        public String Brand { get; set; }
        public String Price { get; set; }
        public String InStock { get; set; }

        [ExcludeFromCodeCoverage]
        override public List<String> GetObjNameProp()
        {
            List<String> arrStr = base.GetObjNameProp();

            foreach (var prop in this.GetType().GetProperties())
            {
                arrStr.Add($"{prop.Name}");
                if (prop.DeclaringType != this.GetType())
                    arrStr.RemoveAt(arrStr.Count - 1);
            }

            return arrStr;
        }
        override public List<String> GetObjValueProp()
        {
            List<String> arrStr = base.GetObjValueProp();

            foreach (var prop in this.GetType().GetProperties())
            {
                arrStr.Add( $"{prop.GetValue(this)}");
            }

            return arrStr;
        }
        [ExcludeFromCodeCoverage]
        override public String HeadingOfObject()
        { return $"{this.Name} {this.Brand}"; }
        override public bool ChangeProperties(int propertyNum, String value)
        {
            PropertyInfo[] info = this.GetType().GetProperties();
            bool returnVal = false;


            if (propertyNum < typeof(Product).GetProperties().Length)
            {
                if (propertyNum < typeof(AbstractWorkableClass).GetProperties().Length)
                    if (base.ChangeProperties(propertyNum, value))
                    {
                        return true;
                    }else return false;




                propertyNum -= typeof(AbstractWorkableClass).GetProperties().Length;


                switch(info[propertyNum].Name){
                    case ("Brand"):
                        if (ChangeProperty_Brand(value))
                            returnVal = true;
                        break;
                    case ("Price"):
                        if (ChangeProperty_Price(value))
                            returnVal = true;
                        break;
                    case ("InStock"):
                        if (ChangeProperty_InStock(value))
                            returnVal = true;
                        break;
                }


            }

            return returnVal;

        }
        private bool ChangeProperty_Brand(String value)
        {
            bool returnVallue = false;
            if (value.Length <= 16 && value.Length >= 0)
            {
                string pattern = @"^[A-z]{0,16}?$";

                if (value.Length <= 16)
                    if (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                    {
                        Brand = value;
                        returnVallue = true;
                    }
            }
            return returnVallue;
        }
        private bool ChangeProperty_Price(String value)
        {
            bool returnVallue = false;
            if (value.Length == 1)
                value = "0.000";

            string pattern = @"(^[0-9]{1,8}\.{1}[0-9]{1}$)";
            if (Regex.IsMatch(value, pattern))
            {
                Price = value + "0";
                returnVallue = true;
            }

            pattern = @"(^[0-9]{1,8}\.{1}[0-9]{3}$)";
            if (Regex.IsMatch(value, pattern))
            {
                value = value.Remove(value.Length - 1);
                Price = value;
                returnVallue = true;
            }

            pattern = @"(^\.{1}[0-9]{2}$)";
            if (Regex.IsMatch(value, pattern))
            {
                value = value.Insert(0, "0");
                Price = value;
                returnVallue = true;
            }

            pattern = @"(^[0]{1}?[0-9]{1,8})";
            if (Regex.IsMatch(value, pattern))
            {
                value = value.Remove(0, 1);
                Price = value;
                returnVallue = true;
            }

            pattern = @"(^[0-9]{1,8}\.{1}[0-9]{2}$)";
            if (Regex.IsMatch(value, pattern))
            {
                Price = value;
                returnVallue = true;
            }

            return returnVallue;
        }
        private bool ChangeProperty_InStock(String value)
        {
            bool returnVallue = false;
            if (value.Length == 0)
                value = "0";

            string pattern ;

            pattern = @"(^[0]{1}$)";
            if (Regex.IsMatch(value, pattern))
            {
 
                InStock = value;
                returnVallue = true;
            }

            pattern = @"(^[1-9]{1}[0-9]{0,8}$)";
            if (Regex.IsMatch(value, pattern))
            {

                InStock = value;
                returnVallue = true;
            }

            pattern = @"(^[0]{1}[0-9]{0,8}$)";
            if (Regex.IsMatch(value, pattern))
            {
                value=value.Remove(0, 1);
                InStock = value;
                returnVallue = true;
            }

            return returnVallue ;
        }

    }
}
