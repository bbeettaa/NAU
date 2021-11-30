using DAL_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DAL_Worck_With_DataBases
{
    class XML_Provider : IDataProvider
    {
        String fileName = "";
        Packet packet;

        public XML_Provider()
        {
            packet = new ();
        }
        public void Serialize()
        {
            TextWriter twr = new StreamWriter(fileName);
            XmlSerializer writer = new (typeof(Packet));

            writer.Serialize(twr, packet);
            twr.Close();
        }
        public List<Object> Deserialize()
        {
            XmlSerializer serializer = new (typeof(Packet));
            FileStream fs = new (fileName, FileMode.Open);
            Packet packet = (Packet)serializer.Deserialize(fs);

            fs.Close();

            return packet.GetList();
        }
        public void SaveListToPacket(List<Object> objList)
        {
            packet.Reset();
            foreach (var obj in objList)
                packet.AddToPacket(obj);
        }

        public void SetSettings(String fileName)
        {
            this.fileName = fileName;
            CheckFile();
        }

        public void CheckFile()
        {
            if (!File.Exists(fileName))
            {
                FileStream file = new (fileName, FileMode.Create);
                StreamWriter writer = new (file, Encoding.Unicode);
                writer.Close();
                file.Close();

                packet = new Packet();
                Serialize();
            }
            String firstLine = File.ReadAllLines(fileName)[0];
            if (!firstLine.Contains("<?xml version=\"1.0\""))
            {
                packet = new Packet();
                Serialize();
            }
        }
    }
}
