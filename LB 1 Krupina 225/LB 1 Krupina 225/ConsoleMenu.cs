using System;
using System.Linq;
using InOut;
using System.Reflection;

using ProgramClasses;

namespace Program
{
    class ConsoleMenu
    {
        private ConsoleColor consoleColor;
        private InOut.InputOutput inOut;
        private Person[] persons;

        private int indexOfChosenObj = 0;
        private int propertyNum = 1;

        public ConsoleMenu()
        {
            inOut = new InputOutput();
            consoleColor = ConsoleColor.Green;
            persons = new Person[] { };
        }

        public void MainMenu()
        {
            String[] mainMenuSection = new String[] { "Головне меню", "Знайти об'єкт для роботи", "Обчислити відсоток студентів 1-го курсу, які приїхали з інших міст", "Добавити об'єкт в базу даних", "Вийти з програми" };

            PrintMainMenuSections(mainMenuSection);

            while (true)
            {
                PrintMainMenuSections(mainMenuSection);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("...\n");
                        WorckWithObjMenu();

                        break;

                    case ConsoleKey.D2:
                        CountPercentOfFirstCourseArrivalsStudent();
                        Console.Clear();
                        break;

                    case ConsoleKey.D3:
                        AddObj();
                        Console.Clear();
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.D4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("... Невідома команда\n");
                        break;

                    case ConsoleKey.DownArrow: break;
                    case ConsoleKey.UpArrow: break;
                    case ConsoleKey.LeftArrow: break;
                    case ConsoleKey.RightArrow: break;
                }
            }
        }
        private void PrintMainMenuSections(String[] mainMenuSection)
        {
            Console.Clear();
            Console.WriteLine(mainMenuSection[0].PadLeft(17, '*').PadRight(22, '*'));
            for (int i = 1; i < mainMenuSection.Length; i++)
                Console.WriteLine(i + ") " + mainMenuSection[i]);
        }


        private void WorckWithObjMenu()
        {
            String str = "";
            while (true)
            {          
                FindAppropriateObjsInDatabaseAndFillArrayPerson( str);
                PrintFindObjs(str);

                ConsoleKeyInfo inputKey;
                inputKey = Console.ReadKey();

                switch (inputKey.KeyChar)
                {
                    case (char)0:
                        SelectObject(inputKey);
                        break;

                    case (char)13: /*Enter*/
                        if (persons.Length > 0)
                        {
                            propertyNum = 1;
                            WorckWithObj();
                        }
                        break;

                    case (char)27: /*Esc*/
                        Console.Clear();
                        return;

                    default:
                        str = CheckInfoBeforeInput(inputKey, str);
                        break;
                }
            }
        }
        private void FindAppropriateObjsInDatabaseAndFillArrayPerson(String find)
        {
            Array.Resize(ref persons, 0);

            Object obj = new Object();
            obj = Activator.CreateInstance(typeof(Person));
            String[] arrStr = inOut.ReadArrayFromDatabase();
            Type[] typeArr = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "ProgramClasses").ToArray();

            for (int i = 0; i < arrStr.Length; i++)
            {
                foreach (Type x in typeArr)
                    if (arrStr[i].Contains(x.Name))
                    {
                        obj = new Object();
                        obj = Activator.CreateInstance(x);
                    }

                ((Person)obj).AssignValue(arrStr[i]);

                if (arrStr[i].Contains("}"))
                {
                    Array.Resize(ref persons, persons.Length + 1);
                    persons[persons.Length - 1] = (Person)obj;
                }
            }
            persons = (from x in persons where x.IsFindInfo(find) == true select x).ToArray();
        }
        private void PrintFindObjs(String str)
        {
                Console.Clear();
                Console.Write("Знайти об'єкт: ");
                Console.Write(str);
                Console.WriteLine($"\n\nОб'єктів знайдено {persons.Length}:\n");

            switch (persons.Length)
            {
                case 0:
                    Console.WriteLine("Об'єкт не знайдено...");
                    break;

                default:
                    for (int i = 0; i < persons.Length; i++)
                    {
                        if (indexOfChosenObj == i)
                            Console.ForegroundColor = consoleColor;

                        Console.WriteLine($"Об'єкт {persons[i].GetType().Name} {persons[i].FirstName} {persons[i].LastName}");
                        Console.ResetColor();
                    }
                    break;
            }
        }
        private void SelectObject(ConsoleKeyInfo inputKey)
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (!(indexOfChosenObj - 1 < 0))
                        indexOfChosenObj -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    if (indexOfChosenObj + 1 < persons.Length)
                        indexOfChosenObj += 1;
                    break;
            }

        }
        private String CheckInfoBeforeInput (ConsoleKeyInfo inputKey, String str)
        {
            if(inputKey.KeyChar != '[' && inputKey.KeyChar != ']' && inputKey.KeyChar != '^')
            if (inputKey.KeyChar >= 48 && inputKey.KeyChar <= 57 ||
                inputKey.KeyChar >= 65 && inputKey.KeyChar <= 123 ||
                inputKey.KeyChar == 0 || inputKey.KeyChar == 13 || inputKey.KeyChar == 27 || 
                inputKey.KeyChar == '\b' || inputKey.KeyChar == '-')
                switch (inputKey.Key)
                {
                    case ConsoleKey.Backspace:
                        if (str.Length > 0)
                            str = str.Remove(str.Length - 1);
                        break;

                    case ConsoleKey.Escape:
                        break;

                    case ConsoleKey.Enter:
                        break;

                    default:
       
                        str += inputKey.KeyChar;
                        break;
                }
            // || 
            return str;
        }


        private void WorckWithObj()
        {
            while (true)
            {
                ConsoleKeyInfo inputKey;

                PrintWorckWithObjSection();

                inputKey = Console.ReadKey();

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
                        WorckWithMethods(inputKey.Key);
                        SaveObj();
                        break;

                    case ConsoleKey.DownArrow:
                        case ConsoleKey.UpArrow:
                            SelectProperty(inputKey);
                        break;

                    case ConsoleKey.Enter:
                        Console.WriteLine("\nвведіть значення:");
                        InputInfoAndSaveObj(propertyNum);
                        break;

                    case ConsoleKey.Escape:
                        return;

                        case ConsoleKey.Delete:
                            DeleteObj();
                            SaveObj();
                            return;         

                    default:
                        propertyNum = inputKey.KeyChar - 48;
                        Console.WriteLine("\nвведіть значення:");
                        InputInfoAndSaveObj(propertyNum);
                        break;
                }
            }
        }
        private void SelectProperty(ConsoleKeyInfo inputKey)
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (!(propertyNum - 1 < 1))
                        propertyNum -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    if (propertyNum + 1 < persons[indexOfChosenObj].GetObjInfo().Length/* + persons[indexOfChosenObj].GetMethodsInfo().Length*/)
                        propertyNum += 1;
                    break;
            }
        }
        private void PrintWorckWithObjSection()
        {
            Console.Clear();
            Console.WriteLine($"Робота з об'єктом {persons[indexOfChosenObj].FirstName} {persons[indexOfChosenObj].LastName}".PadLeft(21, '*').PadRight(25, '*'));

            PrintInfo();
            PrintObjMethods();
            Console.Write("\n");
            Console.WriteLine("Натисніть відповідну клавішу, щоб змінити значення властивості.\n\tDEL, щоб видалити об'єкт.      ESC, щоб вийти.");
        }
        private void PrintInfo()
        {
            String[] ObjInfo;

            ObjInfo = persons[indexOfChosenObj].GetObjInfo();

            for (int i = 0; i < ObjInfo.Length; i++)
            {
                if (i == propertyNum)
                    Console.ForegroundColor = consoleColor;
                if (i == 0)
                    Console.WriteLine($"{ObjInfo[i]}");
                else
                    Console.WriteLine($"{i}) {ObjInfo[i]}");
                Console.ResetColor();
            }
        }
        private void PrintObjMethods()
        {
            String[] ObjInfo;
            ObjInfo = persons[indexOfChosenObj].GetMethodsInfo();
            int extension = persons[indexOfChosenObj].GetObjInfo().Length;

            Console.Write("\n");

            for (int i = extension; i < ObjInfo.Length + extension; i++)
                Console.WriteLine($"F{i - extension + 1}) {ObjInfo[i - extension]}");
        }
        private void WorckWithMethods(ConsoleKey inputKey)
        {
            //112 113 114 115 123
            MethodInfo[] objInfo = persons[indexOfChosenObj].GetType().GetMethods();
            var infos = persons[indexOfChosenObj].GetType().GetInterfaces();
            objInfo = (from x in objInfo where x.ToString().Contains("_Object_") select x).ToArray();

            int key = (int)inputKey;

            for (int i = 0; i < objInfo.Length; i++)
                if(i + 112 == key)
                {
                    ConstructorInfo ctor = persons[indexOfChosenObj].GetType().GetConstructor(new Type[] { });
                    Object result = persons[indexOfChosenObj];
                    persons[indexOfChosenObj].GetType().GetMethod(objInfo[i].Name).Invoke(result, new object[] { });
                }
        }
        private void InputInfoAndSaveObj(int propertyNum)
        {
            String str = "";
            ConsoleKeyInfo inputKey;
            bool isLoop = true;

            while (isLoop == true)
            {
                consoleColor = ConsoleColor.DarkRed;
                PrintWorckWithObjSection();
                inputKey = Console.ReadKey();
                switch (inputKey.Key)
                {
                    case ConsoleKey.Enter:
                        isLoop = false;
                        consoleColor = ConsoleColor.Green;
                        break;

                    default:
                        str = CheckInfoBeforeInput (inputKey, str);
                        persons[indexOfChosenObj].ChangeProperties(propertyNum , str);

                        SaveObj();
                        break;
                }
            }
        }



        public void CountPercentOfFirstCourseArrivalsStudent()
        {
            Console.Clear();
            Console.WriteLine("Обчислити відсоток студентів 1-го курсу, які приїхали з інших міст\n");
            FindAppropriateObjsInDatabaseAndFillArrayPerson("");

            for (int i = 0; i < persons.Length; i++)
                if (!(persons[i] is Student))
                {
                    indexOfChosenObj = i;
                    i--;
                    DeleteObj();
                }

            double total = 0;
            double matchStudent = 0;

            for (int i = 0; i < persons.Length; i++)
                if (((Student)persons[i]).Course == "1")
                {
                    total++;
                    if (((Student)persons[i]).ArivalCity != "Kyiv")
                        matchStudent++;
                }
            Console.WriteLine($"Всього студентів на першому курсі: {total} \nСтудентів на першому курсі, що приїхали: {matchStudent}");

            Console.WriteLine("Процент студентів - {0}%",  100 /(total / matchStudent));
            Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
            Console.ReadKey();

        }


        private void SaveObj()
        {
            String str = "";
            foreach (Person p in persons)
            {
                str += p.GetDataForDatabase();
                if (p != persons.Last())
                    str += "\n\n";
            }
                inOut.WriteInDatabase(str);
        }
        private void DeleteObj()
        {
            Person[] tempPersons = persons;
            Array.Resize(ref persons,persons.Length-1);

            for (int i = indexOfChosenObj+1; i < tempPersons.Length; i++)
                persons[i-1]=tempPersons[i];
        }

        private void AddObj()
        {
            Console.Clear();
            Console.WriteLine("****Добавити об'єкт в базу данних****");
            Console.WriteLine("Щоб створити об'єкт виберіть його тип, а потім редагуйте його розділи");

            Type[] typeArr = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "ProgramClasses").ToArray();
    
                typeArr = (from x in typeArr where !typeof(AbstractPerson).Name.Contains(x.Name) select x).ToArray();
                typeArr = (from x in typeArr where !typeof(Person).Name.Contains(x.Name) select x).ToArray();
                typeArr = (from x in typeArr where !x.Name.Contains("<>c" ) select x).ToArray();
                typeArr = (from x in typeArr where !typeof(Student).GetType().GetInterfaces().ToString().Contains(x.Name) select x).ToArray();

            for (int i = 0; i < typeArr.Length; i++)
                System.Diagnostics.Debug.WriteLine(typeArr[i]);


                for (int i = 0; i < typeArr.Length; i++)
                Console.WriteLine($"{i+1}) {typeArr[i].Name}");

            Object obj = new Object();
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            while (! (keyInfo.KeyChar >= 48 && keyInfo.KeyChar <= 57))
                 keyInfo = Console.ReadKey();

            FindAppropriateObjsInDatabaseAndFillArrayPerson("");

            if (keyInfo.KeyChar - '0' <= typeArr.Length)
                AppendObjInArray(obj, typeArr[keyInfo.KeyChar - '1']);


        }
        private void AppendObjInArray(Object person, Type type)
        {
            person = Activator.CreateInstance(type);
            
            for (int i = 0; i < persons.Length; i++)
                if (i>0 )
                    if (persons[i].GetType().Name != type.Name && persons[i - 1].GetType().Name == type.Name)
                    {
                        System.Diagnostics.Debug.WriteLine("1");

                        Person[] tempPersons = persons;
                        Array.Resize(ref persons, persons.Length + 1);
                        persons[i] = (Person)person;

                        for (int j = i+1; j < persons.Length; j++)
                            persons[j] = tempPersons[j-1];

                        indexOfChosenObj = i;
                        break;
                    }
                    else if(i+1 == persons.Length)
                    {
                        System.Diagnostics.Debug.WriteLine("2");
                        Array.Resize(ref persons, persons.Length + 1);
                        persons[persons.Length - 1] = (Person)person;

                        indexOfChosenObj = i+1;
                        break;
                    }

            WorckWithObj();

            foreach (var x in persons)
                Console.WriteLine($"{x.FirstName} {x.LastName} {x.GetType()}");
        }

    } 
}