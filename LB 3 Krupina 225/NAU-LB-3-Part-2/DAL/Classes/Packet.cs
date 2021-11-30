using ProgramClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL_Classes
{
    [Serializable]
    [XmlRoot("___XML ROoT (-_-)___")]
    //[JsonInclude]
    public class Packet
    {
        [JsonInclude]
        [XmlArrayAttribute("Students")]
        public Student[] students;

        [JsonInclude]
        [XmlArrayAttribute("Taxi Drivers")]
        public List<TaxiDriver> taxiDrivers;

        [JsonInclude]
        [XmlArrayAttribute("Acrobats")]
        public List<Acrobat> acrobats;

        public Packet()
        {
            Reset();
        }
        public void AddToPacket(Object obj)
        {
            if(obj is Student)
                AddToPacket(obj as Student);
            if (obj is TaxiDriver)
                AddToPacket(obj as TaxiDriver);
            if (obj is Acrobat)
                AddToPacket(obj as Acrobat);
        }
        public void AddToPacket(Student student)
        {
            Array.Resize(ref students, students.Length + 1);
            students[^1] = student;
        }
        public void AddToPacket(TaxiDriver taxiDriver)
        { taxiDrivers.Add(taxiDriver);  }
        public void AddToPacket(Acrobat acrobat)
        { acrobats.Add(acrobat); }

        public List<Object> GetList()
        {
            List<Object> list = new();
            foreach (var x in students)
                list.Add(x);
            foreach (var x in taxiDrivers)
                list.Add(x);
            foreach (var x in acrobats)
                list.Add(x);

            return list;
        }
        public void Reset()
        {
            students = Array.Empty<Student>();
            taxiDrivers = new List<TaxiDriver> { };
            acrobats = new List<Acrobat>();
        }

    }
}
