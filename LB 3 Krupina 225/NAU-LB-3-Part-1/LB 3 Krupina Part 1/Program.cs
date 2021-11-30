using System;
using System.IO;
using System.Text;
using BLL;
using ProgramClasses;

namespace Program
{
    class Program
    {
        //  Частина 1. Дослідження механізму сериалізації. Усі завдання частини 1 допускається
        //  реалізовувати в одному проекті одного рішення

        //      1. Описати клас, заданий варіантом(табл 1), та передбачити для нього можливість серіалізації. +
        //      2. Створити масив об’єктів класу, вказаного в п.1, та серіалізувати їх у файл. +
        //      3. Створити новий масив та відновити в ньому значення серіалізованих об’єктів. +
        //      4. Виконати сериалізацію та десериалізацію об‘єктів будь-якої колекції. Порівняти з масивом. +
        //      5. Продемонструвати використання бінарної серіалізації, XML-серіалізації, JSON та користувацької серіалізації:+
        //  - для отримання оцінки «задовільно» реалізувати будь-яку сериалізацію +
        //  - для отримання оцінки «добре» реалізувати бінарну, користувацьку, XML та JSON сериалізацію. +

        static EntityService service = new();

        static readonly Book book0 = new();
        static readonly Book book1 = new();
        static readonly Book book2 = new();
        static readonly Book book3 = new();

        static readonly bool addNewElements = true;
        static readonly bool deleteDatabases = true;

        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            if (deleteDatabases)
            {
                DeleteFiles();
            }

            CreateObjects();

            DemonstrateSerializer(0, "Json",    1);
            DemonstrateSerializer(1, "Xml",     0);
            DemonstrateSerializer(2, "Binary",  3);
            DemonstrateSerializer(3, "Custom",  2);

        }
        static public void DemonstrateSerializer(int indexOfDataprovider,String serializator,int numberObject)
        {
            Console.WriteLine($"-----------Деморнстрація Серіалізатору {serializator}-----------");
            service.entityContext.IndexOfDataprovider = indexOfDataprovider; // Установка сериализатора 
            service.entityContext.SetSettingsSerialization(indexOfDataprovider);

            if (addNewElements)
            {
                service.AppendObjectInDatabase(book0);
                service.AppendObjectInDatabase(book1);
                service.AppendObjectInDatabase(book2);
                service.AppendObjectInDatabase(book3);
            }
            service = new();

            service.entityContext.IndexOfDataprovider = indexOfDataprovider; // Установка сериализатора 
            service.entityContext.SetSettingsSerialization(indexOfDataprovider);

            service.Deserialize();
            service.PrintObjects();

            service.IndexOfChosenObj = numberObject; // Удаляемая книга 

            Console.WriteLine($"Натисніть будь-яку клавішу, щоб видалити об'єкт під порядковим номером\" {numberObject} \"...");
            Console.ReadKey();
            Console.WriteLine($"-----------Видалення \" {numberObject} \" об'єкту-----------");

            service.DeleteObj();

            service.Deserialize();
            service.PrintObjects();

            if (indexOfDataprovider != 3)
                Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити демонстрацію серіалізаторів...");
            else
                Console.WriteLine("****** Натисніть будь-яку клавішу, щоб вийти ******");
            
            Console.ReadKey();
            Console.Clear();
        }
        static public void CreateObjects()
        {
            book0.Name = "FirstBook";
            book0.NumberOfSamples = "1";
            book0.Price = "10";
            book0.SerialNumber = "11-11-11";
            book0.TotalEditionCost = "111";
            book0.YearOfPublication = "11.11.1111";


            book1.Name = "SecondBook";
            book1.NumberOfSamples = "2";
            book1.Price = "20";
            book1.SerialNumber = "22-22-22";
            book1.TotalEditionCost = "222";
            book1.YearOfPublication = "22.22.2222";

            book2.Name = "ThirdBook";
            book2.NumberOfSamples = "3";
            book2.Price = "30";
            book2.SerialNumber = "33-33-33";
            book2.TotalEditionCost = "333";
            book2.YearOfPublication = "33.33.3333";


            book3.Name = "FourthBook";
            book3.NumberOfSamples = "4";
            book3.Price = "40";
            book3.SerialNumber = "44-44-44";
            book3.TotalEditionCost = "444";
            book3.YearOfPublication = "44.44.4444";
        }
        static public void DeleteFiles()
        {
            File.Delete($@"C:\Users\bedu_s_bashkoy\Desktop\Lab 3\LB 3 Part 1\LB 3 Krupina Part 1\{service.entityContext.settings.JsonFileName}");
            File.Delete($@"C:\Users\bedu_s_bashkoy\Desktop\Lab 3\LB 3 Part 1\LB 3 Krupina Part 1\{service.entityContext.settings.XmlFileName}");
            File.Delete($@"C:\Users\bedu_s_bashkoy\Desktop\Lab 3\LB 3 Part 1\LB 3 Krupina Part 1\{service.entityContext.settings.BinaryFileName}");
            File.Delete($@"C:\Users\bedu_s_bashkoy\Desktop\Lab 3\LB 3 Part 1\LB 3 Krupina Part 1\{service.entityContext.settings.CustomFileName}");
        }
    }
}