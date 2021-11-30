using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramInterfaces;

namespace ProgramClasses
{
    [Serializable]
    public class Acrobat : Person , IDancable
    {
        public Acrobat() : base()
        {
            AcrobatTechnique = "Flip";
            FavoriteDance = "Unknown";
        }


        public String AcrobatTechnique { get; set; }
        public String FavoriteDance { get ; set ; }

        public void Operation_Object_Dance()
        {
            Random rand = new();
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

        public void Operation_Object_Show_Technique()
        {
            Random rand = new();
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