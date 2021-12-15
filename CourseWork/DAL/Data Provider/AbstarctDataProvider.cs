using DAL_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace DALWorckWithDataBases
{
    public abstract class AbstarctDataProvider
    {
        protected String fileName = "";
        protected Packet packet = new Packet();
        
        virtual public void Serialize() { }
        virtual public Packet Deserialize() { return null; }
        public void SaveListToPacket(List<Object> objList)
        {
            foreach (var obj in objList)
                packet.AddToPacket(obj);
        }
        public void SetFileName(String fileName)
        {
            this.fileName = fileName;
            CheckFile();
        }
        public bool CheckFile()
        {
            int pos = fileName.LastIndexOf("\\");
            String pathDir = fileName.Remove(pos, fileName.Length - pos) + "\\";
            if (!Directory.Exists(pathDir))
            {
                return false;
            }
            if (!File.Exists(fileName))
            {
                return false;
            }

            return true;
        }
        public void CreateFile()
        {
            FileStream file = new FileStream(fileName, FileMode.Create);
            StreamWriter writer = new StreamWriter(file, Encoding.Unicode);
            writer.Close();
            file.Close();

            packet = new Packet();
            Serialize();

            String firstLine = File.ReadAllLines(fileName)[0];
            if (!firstLine.Contains("<?xml version=\"1.0\""))
            {
                packet = new Packet();
                Serialize();
            }
        }
        public void CreateDir()
        {
            int pos = fileName.LastIndexOf("\\");
            String pathDir = fileName.Remove(pos, fileName.Length - pos) + "\\";
            Directory.CreateDirectory(pathDir);
        }
    }
}
