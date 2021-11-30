using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        private static ConsoleMenu consoleMenu;
        static void Main(string[] args)
        {
            consoleMenu = new ConsoleMenu();

            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            consoleMenu.MainMenu();
        }
    }
}
