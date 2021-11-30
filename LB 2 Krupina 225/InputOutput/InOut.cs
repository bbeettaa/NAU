using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InputOutput
{
    public class InOut
    {
        private static String fileName;

        public InOut()
        {
            String appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            String relativePath = @"TextDataBase.txt";
            fileName = appDir + relativePath;

            fileName = fileName.Replace("LB 2 Krupina 225\\bin\\Debug\\net5.0", "");

            CheckFile();
        }
        public void CheckFile()
        {
            if (!File.Exists(fileName))
                File.Create(fileName);
        }
        public String[] ReadArrayFromDatabase()
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            String str;
            String[] arrStr = new String[] { };

            while (!reader.EndOfStream)
            {
                str = reader.ReadLine();
                Array.Resize(ref arrStr, arrStr.Length + 1);
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
