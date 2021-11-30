using DAL_Classes;
using ProgramClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALWorckWithDataBases

{
    public class CustomProvider : AbstarctDataProvider
    {
        public CustomProvider() { }




        override public void Serialize()
        {
            FileStream s = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter
                (s);

            String data = Serialize(packet);

            sw.WriteLine(data);
            sw.Close();
            s.Close();
        }
        override public List<Object> Deserialize()
        {
            packet.Reset();
            CheckFile();
            foreach (var p in Deserialize(packet))
                packet.AddToPacket(p);

            return packet.GetList();
        }
        private String Serialize(Object obj)
        {
            string data = "";
            Object o = obj;

            data += $"{o.GetType().Name} {{";
            List<PropertyInfo> props = o.GetType().GetProperties().ToList();

            foreach (var prop in props)
                data += String.Format("\n{0}: {1}", prop.Name, prop.GetValue(o));

            foreach (var tt in o.GetType().GetFields())
                data += SerializeField(tt, o);

            data += "}\n";

            return data;
        }
        private String SerializeField(FieldInfo tt, Object o)
        {
            String data = "";

            if (tt.FieldType.IsArray || tt.FieldType.IsGenericType)
            {
                System.Collections.IList sampleObject_test1 = (System.Collections.IList)tt.GetValue(o);
                data += $"\n{tt.Name}: [";
                foreach (var tt1 in sampleObject_test1)
                {
                    data += SerializeObj((Object)tt1);
                }
                data += "]\n";
            }
            return data;
        }
        private String SerializeObj(Object obj)
        {
            string data = "";
            Object o = obj;

            List<PropertyInfo> props = o.GetType().GetProperties().ToList();
            data += $"\n\t{o.GetType().Name} {{";
            foreach (var prop in props)
                data += String.Format("\n\t{0}: {1}", prop.Name, prop.GetValue(o));

            foreach (var tt in o.GetType().GetFields())
                data += SerializeField(tt, o);
            data += "\t}\n";
            return data;
        }
        private static bool DecerializeStringAndSetValue(String str, ref Object obj)
        {
            List<PropertyInfo> props = obj.GetType().GetProperties().ToList();
            foreach (var prop in props)
                if (str.Contains(prop.Name))
                {
                    str = str.Replace($"{prop.Name} {{", "");
                    str = str.Replace($"{prop.Name}: [", "");
                    str = str.Replace($"\t{prop.Name} {{", "");

                    str = str.Replace($"{prop.Name}: ", "");
                    str = str.Replace($"\t}}", "");
                    str = str.Replace($"\t", "");

                    prop.SetValue(obj, str);
                    return true;
                }
            return false;
        }
        private Object[] Deserialize(Object WorckableObj)
        {
            FileStream s = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(s);

            String str = "";

            bool isEnd = false;
            bool isBegining = false;

            Object ElementOfArray = new Object();
            Object[] returnArr = Array.Empty<object>();

            while (str != null)
            {
                foreach (var field in WorckableObj.GetType().GetFields())
                {

                    if (str.Contains("\t}"))
                        isEnd = true;


                    str = str.Replace(": [", "");
                    str = str.Replace("\t\t", "");
                    str = str.Replace("\t", "");
                    str = str.Replace("}", "");
                    str = str.Replace("}\t", "");
                    str = str.Replace(" {", "");
                    str = str.Replace("{", "");


                    if (str == field.Name)
                        isBegining = true;

                    if (isBegining)
                    {
                        if (field.FieldType.IsGenericType && field.Name.Contains(str))
                            ElementOfArray = DeserializeGenericTypeAndCreateINstanceToType(field);

                        else if (field.FieldType.IsArray && field.Name.Contains(str))
                            ElementOfArray = DeserializeArrayTypeAndCreateINstanceToType(field);

                        DecerializeStringAndSetValue(str, ref ElementOfArray);

                    }

                    if (isEnd && isBegining)
                    {
                        Array.Resize(ref returnArr, returnArr.Length + 1);
                        returnArr[returnArr.Length-1] = ElementOfArray;
                        ElementOfArray = new object();
                        isEnd = false;
                    }

                    if (str == "]")
                        isBegining = false;
                }
                str = sr.ReadLine();
            }
            sr.Close();
            s.Close();

            return returnArr;
        }
        static private Object DeserializeGenericTypeAndCreateINstanceToType(FieldInfo field)
        {
            String assemblyName;
            Type t = field.FieldType;
            assemblyName = t.FullName;

            //"System.Collections.Generic.List`1[[ProgramClasses.TaxiDriver, DAL_Classes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]"

            int pos = assemblyName.IndexOf("[[");
            assemblyName = assemblyName.Remove(0, pos + 2);

            pos = assemblyName.IndexOf("]]");
            assemblyName = assemblyName.Remove(pos,2);
            
            Type tt = Type.GetType(assemblyName);
            return Activator.CreateInstance(tt);
        }
        static private Object DeserializeArrayTypeAndCreateINstanceToType(FieldInfo field)
        {
            String typeName;
            Type t = field.FieldType;
            typeName = t.FullName;
            typeName = typeName.Replace("[]", "");
           
            Type tt = Type.GetType(typeName+", "+ t.Assembly.FullName);
            return Activator.CreateInstance(tt);
        }




        ////////////////////////////////////////////////////////////////////////////

       

    }
}
