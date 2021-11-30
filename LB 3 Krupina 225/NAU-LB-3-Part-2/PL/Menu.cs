using BLL;
using System;
using System.Collections.Generic;

//Задача 1
//          реализовать меню +
//          реализовать выбора объекта +
//          реализовать базовый сериализатор +
//          Реализовать сериализацию через масивы через класс Packet +
//Задача 2
//          передать имя в меню из класса +
//          реализовать поиск +
//          реализовать выбор на стрелочки +
//          Реализовать отдельную сериализацию DataProvider для класса EntityContext +
//          Реализовать меню для работи с объeктом +
//          Реализовать работу с методами +
//          Реализовать работу со свойствами +
//          Реализовать меню добавления объeкта +
//              Реализовать добавление объекта +
//          Перенести функционал работи\изменения в EntityService +
//          Реализовать сериализацию объектов JSON +
//          Реализовать удаление объекта +
//          Исправить - вывод количества найденых объектов постоянный +
//          Довбавить в поиске объекта его фамилию +
//          Выход из "меню добавления объекта" через ESC +
//          Выбор стрелочками в "меню добавления объекта" +
//Задача 3
//          Исправить поведение индексатора при поиске объектов +
//
//          Реализовать настройки +
//              Реализовать класс настройки, где методи - способ сериализации, свойства - название хранимого файла +
//          Реализовать автоматическое применение настроек +
//          Исправить - не сохраняет методы +
//
//          Реализовать сериализацию XML +
//          Реализовать сериализацию Binary +
//          Реализовать АБСТРАКЦИЮ DataProvider где определяються методы +
//          Унаследывать сериализаторы +
//          Реализовать выбор сериализатора +
//Задача 4
//Задача 5
//          Реализовать сериализацию Custom +
//
//          Упроститть функции в меню +
//          Упростить функции в EntitySrvice +
//          Упростить функции в EntityContext +
//          
//          Исправит - меняеться в настройках сериализатор один раз +
//
//          Исправить - повторяет тип объэкта в меню работы с объектом +
//          исправит - индекс объектов при удалении объекта +
//          исправит - индекс свойств в объекте конфиге +
//
//          Решить 8 предупреждений +
//          Решить 128 сообщений +
//Задача 6
//          Реализовать свой класс exception +

namespace PL
{
    public static class Menu
    {
        static private ConsoleColor consoleColor;
        static readonly EntityService service = new();

        static Menu()
        {
            consoleColor = ConsoleColor.Green;
        }
        static public void MainMenu()
        {
            String[] mainMenuSection = new String[] { "Головне меню", "Знайти об'єкт для роботи", "Добавити об'єкт в базу даних", "Налаштування", "Вийти з програми" };
            PrintMainMenuSections(mainMenuSection);

            while (true)
            {
                PrintMainMenuSections(mainMenuSection);
                MainMenuChooseSections();
            }
        }
        static private void PrintMainMenuSections(String[] mainMenuSection)
        {
            Console.Clear();
            Console.WriteLine(mainMenuSection[0].PadLeft(17, '*').PadRight(22, '*'));
            for (int i = 1; i < mainMenuSection.Length; i++)
                Console.WriteLine(i + ") " + mainMenuSection[i]);
        }
        static private void MainMenuChooseSections()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("...\n");
                    WorckWithObjMenu();
                    break;

                case ConsoleKey.D2:
                    Console.Clear();
                    AddObject();
                    break;

                case ConsoleKey.D3:
                    Console.Clear();
                    SettingsMenu();
                    break;

                case ConsoleKey.Escape:
                case ConsoleKey.D4:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("... Невідома команда\n");
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    break;
            }
        }



        static private void WorckWithObjMenu()
        {
            try
            {
                String find = "";
                while (true)
                {
                    service.FindObjects(find);
                    PrintFindObjs(find);
                    ConsoleKeyInfo inputKey = Console.ReadKey();

                    switch (inputKey.KeyChar)
                    {
                        case (char)0:
                            service.SelectObject(inputKey);
                            break;

                        case (char)13: /*Enter*/
                            if (service.ObjListLength() > 0)
                            {
                                service.PropertyNum = 1;
                                WorckWithObj(true);
                            }
                            break;

                        case (char)27: /*Esc*/
                            Console.Clear();
                            return;

                        default:
                            find = EntityService.CheckStr(inputKey, find);
                            service.FindObjects(find);
                            break;
                    }
                }
            }
            catch (Exception e) { 
                service.CreateNewFile();
                Console.WriteLine($"{e.Message}\n*** Створення нового файлу ***\n\nНатисність клавішу, щоб продовжити...");
                Console.ReadKey();
                WorckWithObjMenu();
            }
        }
        static private void PrintFindObjs(String find)
        {
            List<Object> objList = null;
            try
            { objList = service.FindObjects(find); }
            catch (Exception e)
            {
                service.CreateNewFile();
                Console.WriteLine($"{e.Message}\n*** Створення нового файлу ***\n\nНатисність клавішу, щоб продовжити...");
                Console.ReadKey();
                PrintFindObjs(find);
            }

            List<String> names = EntityService.GetObjNames(objList);

            Console.Clear();
            Console.Write("Знайти об'єкт: ");
            Console.Write(find);
            Console.WriteLine($"\n\nОб'єктів знайдено {names.Count}:\n");

            switch (objList.Count)
            {
                case 0:
                    Console.WriteLine("Об'єкт не знайдено...");
                    break;

                default:

                    for (int i = 0; i < objList.Count; i++)
                    {
                        if (service.IndexOfChosenObj == i)
                            Console.ForegroundColor = consoleColor;

                        Console.WriteLine($"Об'єкт {names[i]}");
                        Console.ResetColor();
                    }
                    break;
            }
        }
        static private void InputInfoAndSaveObj()
        {
            String inputData = "";
            ConsoleKeyInfo inputKey;
            bool isLoop = true;

            while (isLoop == true)
            {
                consoleColor = ConsoleColor.DarkRed;
                PrintWorckWithObjSection(true);
                inputKey = Console.ReadKey();
                switch (inputKey.Key)
                {
                    case ConsoleKey.Enter:
                        isLoop = false;
                        consoleColor = ConsoleColor.Green;
                        break;

                    default:
                        inputData = EntityService.CheckStr(inputKey, inputData);
                        if (!service.InputInfoAndSaveObj(inputData))
                            if (inputData.Length > 0)
                                inputData = inputData.Remove(inputData.Length - 1);


                        break;
                }
            }
        }



        static private void WorckWithObj(bool isWorckableObj)
        {
            bool loop = true;
            while (loop)
            {
                PrintWorckWithObjSection(isWorckableObj);
                loop = ChooseSectionWorckWithObj(isWorckableObj);
            }
        }
        static private bool ChooseSectionWorckWithObj(bool isWorckableObj)
        {
            ConsoleKeyInfo inputKey = Console.ReadKey();

            if (ChooseMethods(inputKey, isWorckableObj))
                return true;
            switch (inputKey.Key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                    service.SelectProperty(inputKey);
                    break;

                case ConsoleKey.Enter:
                    Console.WriteLine("\nвведіть значення:");
                    InputInfoAndSaveObj();
                    break;

                case ConsoleKey.Escape:
                    return false;

                case ConsoleKey.Delete:
                    if (isWorckableObj)
                        service.DeleteObj();
                    return false;

                default:
                    service.PropertyNum=inputKey.KeyChar - 48 ;
                    Console.WriteLine("\nвведіть значення:");
                    InputInfoAndSaveObj();
                    break;
            }
            return true;
        }
        static private bool ChooseMethods(ConsoleKeyInfo inputKey, bool isWorckableObj)
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.F1:
                case ConsoleKey.F2:
                case ConsoleKey.F3:
                case ConsoleKey.F4:
                case ConsoleKey.F5:
                case ConsoleKey.F6:
                case ConsoleKey.F7:
                case ConsoleKey.F8:
                case ConsoleKey.F9:
                case ConsoleKey.F10:
                case ConsoleKey.F11:
                case ConsoleKey.F12:
                    WorckWithMethods(inputKey.Key, isWorckableObj);
                    return true;
            }
            return false;
        }

        static private void PrintWorckWithObjSection(bool isWorckableObj)
        {
            Console.Clear();
            Console.WriteLine($"Робота з об'єктом {service.GetObjectHeading()}".PadLeft(21, '*').PadRight(25, '*'));

            PrintInfo();
            PrintObjMethods();
            Console.Write("\n");
            Console.WriteLine("Натисніть відповідну клавішу, щоб змінити значення властивості.\nESC, щоб вийти.");
            if (isWorckableObj)
                Console.WriteLine("\tDEL, щоб видалити об'єкт.");
        }
        static private void PrintInfo()
        {
            List<String> objInfo = service.GetObjectInfo();
            for (int i = 0; i < objInfo.Count; i++)
            {
                if (i == service.PropertyNum)
                    Console.ForegroundColor = consoleColor;
                if (i == 0)
                    Console.WriteLine($"{objInfo[i]}");
                else
                    Console.WriteLine($"{i}) {objInfo[i]}");
                Console.ResetColor();
            }
        }
        static private void PrintObjMethods()
        {
            List<String> objInfo = service.GetMethodsInfo();
            int extension = service.GetObjectInfo().Count;

            Console.Write("\n");

            for (int i = extension; i < objInfo.Count + extension; i++)
                Console.WriteLine($"F{i - extension + 1}) {objInfo[i - extension]}");
        }
        static private void WorckWithMethods(ConsoleKey inputKey, bool isWorckableObj)
        { service.WorckWithMethods(inputKey, isWorckableObj); }



        static public void AddObject()
        {
            int choosenObj = 0;
            bool loop = true;
            ConsoleKeyInfo keyInfo;
            while (loop)
            {
                PrintAddObjSection(choosenObj);
                keyInfo = Console.ReadKey();

                choosenObj = ChooseObjectsInSection(choosenObj, keyInfo);
                loop = ChooseObjInAddObj(choosenObj, keyInfo);
            }
        }
        static private void PrintAddObjSection(int choosenObj)
        {
            Console.Clear();
            Console.WriteLine("****Добавити об'єкт в базу данних****");
            Console.WriteLine("Щоб створити об'єкт виберіть його тип, а потім редагуйте його розділи");

            for (int i = 0; i < EntityService.GetAssemblyTypes().Count; i++)
            {
                if (i == choosenObj)
                    Console.ForegroundColor = consoleColor;

                Console.WriteLine($"{i + 1}) {BLL.EntityService.GetAssemblyTypes()[i]}");
                Console.ResetColor();
            }
        }



        static private bool ChooseObjInAddObj(int choosenObj, ConsoleKeyInfo keyInfo)
        {
            try {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.UpArrow:
                        return true;

                    case (ConsoleKey)13: /*Enter*/
                        service.AppendObjectInDatabase(choosenObj + 1);
                        WorckWithObj(true);
                        return false;

                    case (ConsoleKey)27: /*Esc*/
                        Console.Clear();
                        return false;

                    default:
                        while (!(keyInfo.KeyChar >= 48 && keyInfo.KeyChar <= 57))
                            keyInfo = Console.ReadKey();
                        service.AppendObjectInDatabase(keyInfo.KeyChar - '0');
                        WorckWithObj(true);
                        return false;
                }
            }
            catch (Exception e)
            {
                service.CreateNewFile();
                Console.WriteLine($"{e.Message}\n*** Створення нового файлу ***\n\nНатисність клавішу, щоб продовжити...");
                Console.ReadKey();
                return ChooseObjInAddObj(choosenObj, keyInfo);
            }
        }
        static private int ChooseObjectsInSection(int choosenObj, ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    choosenObj += 1;
                    break;
                case ConsoleKey.UpArrow:
                    choosenObj -= 1;
                    break;
            }
            choosenObj = service.CheckIndexOfChoosenObjs(choosenObj);
            return choosenObj;
        }



        static private void SettingsMenu()
        {
            service.ChangeSettings();
            WorckWithObj(false);
            service.EndOfChangeSettings();
        }


    }
}
