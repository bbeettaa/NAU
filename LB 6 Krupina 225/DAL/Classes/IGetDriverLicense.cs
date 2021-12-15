using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    interface IGetDriverLicense
    {
        String Age { get; set; }
        String DriverLicense { get; set; }
        void Operation_Object_Get_Driver_License();
    }
}
