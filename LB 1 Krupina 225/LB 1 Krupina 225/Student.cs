using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramInterfaces;

namespace ProgramClasses
{
    class Student : Person, IDancable
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
        public void Operation_Object_Dance()
        {
            if (FavoriteDance != "Not predisposed to")
            {
                Random rand = new Random();
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
        public void Transfer_Next_Object_To_The_Next_Сourse()
        {
            int tempCourse = int.Parse(Course) + 1;
            Course = tempCourse.ToString();
        }
    }
}