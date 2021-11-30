using ProgramClasses;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DAL_Classes
{
    [Serializable]
    [XmlRoot("___XML ROoT (-_-)___")]
    public class Packet
    {
        [JsonInclude]
        [XmlArrayAttribute("ArrBooks1")]
        public Book[] ArrBooks;

        [JsonInclude]
        [XmlArrayAttribute("ListBooks1")]
        public List<Book> ListBooks;

        public Packet()
        {
            Reset();
        }
        public void AddToPacket(Object obj)
        {
            if (obj is Book)
                AddToPacket(obj as Book);
        }

        public void AddToPacket(Book book)
        {
            if (ArrBooks.Length < ListBooks.Count)
            {
                Array.Resize(ref ArrBooks, ArrBooks.Length + 1);
                ArrBooks[^1] = book;
            }
            else
            {
                ListBooks.Add(book);
            }
        }

        public List<Object> GetList()
        {
            List<Object> list = new();
            foreach (var x in ArrBooks)
                list.Add(x);
            foreach (var x in ListBooks)
                list.Add(x);

            return list;
        }
        public void Reset()
        {
            ArrBooks = Array.Empty<Book>();
            ListBooks = new List<Book>();
        }

    }
}
