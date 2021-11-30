using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using InputOutput;

namespace ConsoleApp1
{
    
    class Program
    {

        static public InputOutput.DopClass dop = new DopClass();

        static void Main(string[] args)
        {

            dop.Print();
            dop.PrintCustom("Nice");
            Console.ReadKey();
        }
    }
}
