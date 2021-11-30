using System;
using System.Linq;
using InputOutput;
using System.Reflection;

using ProgramClasses;
using System.Text.RegularExpressions;

namespace LB_2_Krupina_225
{
    class ConsoleMenu
    {
        private ConsoleColor consoleColor;
        private InputOutput.InOut inOut;
        private Object[] arrObj;
        private BinaryTree<BasicClass> tree;

        private int indexOfChosenObj = 0;
        private int propertyNum = 1;


        public ConsoleMenu()
        {
            inOut = new InOut();
            consoleColor = ConsoleColor.Green;
            arrObj = new BasicClass[] { };
            tree = new BinaryTree<BasicClass>();

            BinaryTree<Book> tree1 = new BinaryTree<Book>();
        }


        public void MainMenu()
        {
            String[] mainMenuSection = new String[] { "Головне меню", "Знайти об'єкт для роботи", "Добавити об'єкт в бінарне дерево", "Вийти з програми" };

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
                        AddObj();
                        Console.Clear();
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.D3:
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
            String find = "";
            while (true)
            {
                FillBinaryTreeFromDatabase();
                FillArrayOfAppropriateObjectsFromTree(find);
                PrintFindObjs(find);

                ConsoleKeyInfo inputKey;
                inputKey = Console.ReadKey();

                switch (inputKey.KeyChar)
                {
                    case (char)0:
                        SelectObject(inputKey);
                        break;

                    case (char)13: /*Enter*/
                        if (arrObj.Length > 0)
                        {
                            propertyNum = 1;
                            WorckWithObj();
                        }
                        break;

                    case (char)27: /*Esc*/
                        Console.Clear();
                        return;

                    default:
                        find = CheckInfoBeforeInput(inputKey, find);
                        break;
                }
            }
        }
        private void FillBinaryTreeFromDatabase()
        {
            tree.Clear();
            Array.Resize(ref arrObj,0);

            Object obj = new Object();
            obj = Activator.CreateInstance(typeof(BasicClass));

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
                ((BasicClass)obj).AssignValue(arrStr[i]);

                if (arrStr[i].Contains("}"))
                    tree.Insert((BasicClass)obj, ((BasicClass)obj).Name);
            }
        }
        private void FillArrayOfAppropriateObjectsFromTree(String find)
        {
            Array.Resize(ref arrObj, 0);

            foreach (object a in tree)
            {
                if(a is Node<BasicClass>)
                if (((Node<BasicClass>)a).key.Contains(find))
                {
                    Array.Resize(ref arrObj, arrObj.Length + 1);
                    arrObj[arrObj.Length - 1] = ((Node<BasicClass>)a).obj;
                }
            }
        }
        private void PrintFindObjs(String str)
        {


            Console.Clear();
            Console.Write("Знайти об'єкт: ");
            Console.Write(str);
            Console.WriteLine($"\n\nОб'єктів знайдено {arrObj.Length}:\n");

            switch (arrObj.Length)
            {
                case 0:
                    Console.WriteLine("Об'єкт не знайдено...");
                    break;

                default:
                    for (int i = 0; i < arrObj.Length; i++)
                    {
                        if (indexOfChosenObj == i)
                            Console.ForegroundColor = consoleColor;

                        Console.WriteLine($"Об'єкт {arrObj[i].GetType().Name} {((BasicClass)arrObj[i]).Name}");
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
                    if (indexOfChosenObj + 1 < arrObj.Length)
                        indexOfChosenObj += 1;
                    break;
            }
        }
        private String CheckInfoBeforeInput(ConsoleKeyInfo inputKey, String str)
        {
            if (inputKey.Key == ConsoleKey.Backspace)
            {
                if (str.Length > 0)
                    str = str.Remove(str.Length - 1);
            }
            else
            {
                str += inputKey.KeyChar;
                str = Regex.Replace(str, @"[\[\]\^А-Яа-я]", "");
            }
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
                    if (propertyNum + 1 < ((BasicClass)arrObj[indexOfChosenObj]).GetObjInfo().Length + ((BasicClass)arrObj[indexOfChosenObj]).GetMethodsInfo().Length)
                        propertyNum += 1;
                    break;
            }
        }
        private void PrintWorckWithObjSection()
        {
            Console.Clear();
            Console.WriteLine($"Робота з об'єктом {((BasicClass)arrObj[indexOfChosenObj]).HeadingOfObject()}".PadLeft(21, '*').PadRight(25, '*'));

            PrintInfo();
            PrintObjMethods();
            Console.Write("\n");
            Console.WriteLine("Натисніть відповідну клавішу, щоб змінити значення властивості.\n\tDEL, щоб видалити об'єкт.      ESC, щоб вийти.");
        }
        private void PrintInfo()
        {
            String[] ObjInfo;

            ObjInfo = ((BasicClass)arrObj[indexOfChosenObj]).GetObjInfo();

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
            ObjInfo = ((BasicClass)arrObj[indexOfChosenObj]).GetMethodsInfo();
            int extension = ((BasicClass)arrObj[indexOfChosenObj]).GetObjInfo().Length;

            Console.Write("\n");

            for (int i = extension; i < ObjInfo.Length + extension; i++)
                Console.WriteLine($"F{i - extension + 1}) {ObjInfo[i - extension]}");
        }
        private void WorckWithMethods(ConsoleKey inputKey)
        {
            //112 113 114 115 123
            MethodInfo[] objInfo = arrObj[indexOfChosenObj].GetType().GetMethods();
            var infos = arrObj[indexOfChosenObj].GetType().GetInterfaces();
            objInfo = (from x in objInfo where x.ToString().Contains("_Object_") select x).ToArray();
            //objInfo = (from x in objInfo where x.ToString().Contains("_Certain_") select x).ToArray();

            int key = (int)inputKey;

            for (int i = 0; i < objInfo.Length; i++)
                if (i + 112 == key)
                {
                    if (objInfo[i].Name.Contains("_Certain_"))
                    {
                        Console.WriteLine("Введіть значення: ");
                        string num = Console.ReadLine();

                        num = Regex.Replace(num, @"[A-Za-z-+=.]", "");

                        if (num == "") return;

                        ConstructorInfo ctor = arrObj[indexOfChosenObj].GetType().GetConstructor(new Type[] { });
                        Object result = arrObj[indexOfChosenObj];
                        arrObj[indexOfChosenObj].GetType().GetMethod(objInfo[i].Name).Invoke(result, new object[] { double.Parse(num) });
                    }
                    else {
                        ConstructorInfo ctor = arrObj[indexOfChosenObj].GetType().GetConstructor(new Type[] { });
                        Object result = arrObj[indexOfChosenObj];
                        arrObj[indexOfChosenObj].GetType().GetMethod(objInfo[i].Name).Invoke(result, new object[] {});
                    } 
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
                        str = CheckInfoBeforeInput(inputKey, str);
                        if (!((AbstractBasicClass)arrObj[indexOfChosenObj]).ChangeProperties(propertyNum-1, str))
                            str=str.Remove(str.Length-1);

                        // Здесь
                        tree.ChangeNodeObj(((BasicClass)arrObj[indexOfChosenObj]).Name, (BasicClass)arrObj[indexOfChosenObj]);
                        SaveObj();
                        break;
                }
            }
        }


        private void SaveObj()
        {
            String str = "";
            for (int i =arrObj.Length-1;i>=0;i--)
            {
                str += ((BasicClass)arrObj[i]).GetDataForDatabase();
                if (i != 0)
                    str += "\n\n";
            }
            inOut.WriteInDatabase(str);
        }
        private void DeleteObj()// Метод для роботи з масивом
        {
            Object[] tempObject = arrObj;
            Array.Resize(ref arrObj, arrObj.Length - 1);

            for (int i = indexOfChosenObj + 1; i < tempObject.Length; i++)
                arrObj[i - 1] = tempObject[i];
        }

        private void AddObj() 
        {
            Console.Clear();
            Console.WriteLine("****Добавити об'єкт в базу данних****");
            Console.WriteLine("Щоб створити об'єкт виберіть його тип, а потім редагуйте його розділи");

            Type[] typeArr = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "ProgramClasses").ToArray();

            typeArr = (from x in typeArr where !typeof(AbstractBasicClass).Name.Contains(x.Name) select x).ToArray();
            typeArr = (from x in typeArr where !typeof(BasicClass).Name.Contains(x.Name) select x).ToArray();
            typeArr = (from x in typeArr where !x.Name.Contains("<>c") select x).ToArray();
            //typeArr = (from x in typeArr where !typeof(Student).GetType().GetInterfaces().ToString().Contains(x.Name) select x).ToArray();

            for (int i = 0; i < typeArr.Length; i++)
                Console.WriteLine($"{i + 1}) {typeArr[i].Name}");

            Object obj = new Object();
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            while (!(keyInfo.KeyChar >= 48 && keyInfo.KeyChar <= 57))
                keyInfo = Console.ReadKey();

            FillBinaryTreeFromDatabase();
            FillArrayOfAppropriateObjectsFromTree("");

            if (keyInfo.KeyChar - '0' <= typeArr.Length)
                AppendObjInArray(obj, typeArr[keyInfo.KeyChar - '1']);


        }
        private void AppendObjInArray(Object person, Type type)// Метод для роботи з масивом
        {
            person = Activator.CreateInstance(type);

            if (arrObj.Length == 0)
            {
                System.Diagnostics.Debug.WriteLine("1");

                Object[] tempObject = arrObj;
                Array.Resize(ref arrObj, arrObj.Length + 1);
                arrObj[0] = (BasicClass)person;

                indexOfChosenObj = 0;
            }

            for (int i = 0; i < arrObj.Length; i++)
                if (i > 0)
                    if (arrObj[i].GetType().Name != type.Name && arrObj[i - 1].GetType().Name == type.Name)
                    {
                        System.Diagnostics.Debug.WriteLine("1");

                        Object[] tempObject = arrObj;
                        Array.Resize(ref arrObj, arrObj.Length + 1);
                        arrObj[i] = (BasicClass)person;

                        for (int j = i + 1; j < arrObj.Length; j++)
                            arrObj[j] = tempObject[j - 1];

                        indexOfChosenObj = i;
                        break;
                    }
                    else if (i + 1 == arrObj.Length)
                    {
                        System.Diagnostics.Debug.WriteLine("2");
                        Array.Resize(ref arrObj, arrObj.Length + 1);
                        arrObj[arrObj.Length - 1] = (BasicClass)person;

                        indexOfChosenObj = i + 1;
                        break;
                    }

            WorckWithObj();

            foreach (var x in arrObj)
                System.Diagnostics.Debug.WriteLine($"{((BasicClass)x).Name} {((BasicClass)x).GetType()}");
        }
    }


}
