using PL;
using System;
using System.Text;

namespace Program
{
    class Program
    {
        [STAThreadAttribute]
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            PL.Program.Main();
        }
    }
}
