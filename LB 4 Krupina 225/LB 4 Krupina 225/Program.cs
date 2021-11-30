
using System;
using System.Collections.Generic;
using System.Text;

namespace LB_4_Krupina_225
{
    /*
1.Описати на мові С# лямбда-вираз чи анонімний метод згідно з варіантом та викликати його
через делегат з відповідним підписом.*
2. Створити компонент багаторазового використання (клас), 
    що містить член-подію.
Для події створити клас-аргумент події. Для опису події створити новий делегат або використати 
наявний делегат бібліотеки FCL (наприклад, EventHandler).**

3. Використовуючи створений у п.3 компонент, створити додаток, у якому визначити метод-
обробник події для цього компонента, що реалізує реакцію додатка на подію (наприклад, повідомлення 
    користувачеві про виникнення події). 
    
    Метод-обробник події повинний отримувати інформацію про
    об’єкт-ініціатор події та аргумент події.**


     * Для оцінки «добре» реалізувати і анонімний метод, і лямбда-вираз 

     ** Для оцінки «відмінно» обов’язково повинні бути: 
     *1) клас-аргумент подій, 
     *2) клас, в якому генерується подія (знаходяться сама подія та метод, що її викликає), 
     *3) клас (-и), в якому є метод (-и) обробники події, 
     *4) метод-обробник події обов’язково повинні мати сигнатуру, як у стандартних подіях
     *.NET Framework. Допускається використовувати generic EventHandler. 

     */


    class Program
    {
        static readonly ArithmeticalOperation arifmetical = new();
        delegate void FindCountOfCharacter(string str, char ch);
        static FindCountOfCharacter findCountOfCharacter;
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            //String str = "2+2"; //4
            //String str = "2+2*2"; //6
            //String str = "(2+2)*2"; //8

            //String str = "10/5,2"; //2,923076923076923

            //String str = "2^|-5|"; //32
            //String str = "2^|-5|2/8^2"; //1

            //String str = "1*|-3|*4/4"; //3
            //String str = "1|-3|4/4"; //3
            //String str = "|-2|+2*4/4"; //4

            //String str = "sqrt49"; //7
            //String str = "sqrt(48+1)"; //7

            //String str = "2+2-4*sqrt(7^2-1)"; //-23,712
            //String str = "(2+2-4)*sqrt(7^2-1)"; //0

            //String str = "2*sqrt(2^|4|+sqrt81)"; //10
            String str = "2*sqrt(2^|4|+sqrt81)/2"; //5

            arifmetical.RegistreteHandler(new EventHandler<ArithmeticEventArgs>(Print));
            arifmetical.Calculate(str);

            Console.WriteLine("Введіть символ для пошуку");
            findCountOfCharacter = FindAmountOfSymbol;
            ConsoleKeyInfo ch = Console.ReadKey();
            findCountOfCharacter(str, ch.KeyChar);

            Console.ReadKey();
        }
        public static void Print(object sender, ArithmeticEventArgs eventArgs) => Console.WriteLine($"{eventArgs.message}");
        public static void FindAmountOfSymbol(string str, char ch)
        {
            int res=0;
            foreach (char chr in str)
                if (chr == ch)
                    res++;

            Console.WriteLine($"\nсимвол {ch} зустрічається {res} раз(и\\ів).");
        }

    }
}