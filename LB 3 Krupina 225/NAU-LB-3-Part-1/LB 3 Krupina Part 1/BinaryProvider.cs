using DAL_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DAL_Worck_With_DataBases
{
    class BinaryProvider : IDataProvider
    {
        String fileName = "";
        Packet packet;

        public BinaryProvider()
        {
            packet = new ();
        }
        public void Serialize()
        {
            BinaryFormatter formatter = new();
            Stream stream = new FileStream (fileName, FileMode.Create, FileAccess.Write);
#pragma warning disable SYSLIB0011 // Тип или член устарел
            formatter.Serialize(stream, packet);
#pragma warning restore SYSLIB0011 // Тип или член устарел

            stream.Close();
        }
        public List<Object> Deserialize()
        {
            FileStream fs = new (fileName, FileMode.Open, FileAccess.Read);
            IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Тип или член устарел
            packet = (Packet)formatter.Deserialize(fs);
#pragma warning restore SYSLIB0011 // Тип или член устарел
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
                FileStream file = new(fileName, FileMode.Create);
                StreamWriter writer = new(file, Encoding.Unicode);
                writer.Close();
                file.Close();

                packet = new Packet();
                Serialize();
            }

        }
    }
}
