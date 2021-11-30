using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramClasses
{
    class TaxiDriver : Person
    {
        public TaxiDriver() : base()
        {
            License = "-1";
            CarriesPassenger = "No";
        }
        
        public String License { get; set; }
        public String CarriesPassenger { get; set; }
        public void Operation_Object_Drive()
        {
            if (License == "-1")
                License = "UnderArrest";

            Random rand = new Random();

            if(License != "License")
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
    }
}