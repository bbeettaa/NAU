using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace InOut
{
    public class InputOutput
    {
        private const String fileName = "C:\\Users\\bedu_s_bashkoy\\source\\repos\\LB 1 Krupina 225\\TextDataBase.txt";
        public InputOutput()
        {
            CheckFile();
        }
        private void CheckFile()
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                throw new ArgumentException("Помилка при читанні файлу - Файлу не існує...\nСтворено новий файл...");
            }
        }
        public String[] ReadArrayFromDatabase()
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(file, Encoding.GetEncoding(1252));
            String str;
            String[] arrStr = new String[] {};

            while(!reader.EndOfStream)
            {
                str = reader.ReadLine();
                Array.Resize(ref arrStr, arrStr.Length+1);
                arrStr[arrStr.Length - 1] = str;
            }

            file.Close();
            reader.Close();

            return arrStr;
        }
        public void WriteInDatabase(String str)
        {
            FileStream file = new FileStream(fileName, FileMode.Create);
            StreamWriter writer = new StreamWriter(file, Encoding.Unicode);

            writer.Write(str);

            writer.Close();
            file.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
