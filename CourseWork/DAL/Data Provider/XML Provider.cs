using DAL_Classes;
using ProgramClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

// внутринее исключение xml - файл не являеться xml - пересоздать его с потерей данних

namespace DALWorckWithDataBases
{
    class XML_Provider : AbstarctDataProvider
    {

        public XML_Provider()
        {
            packet = new Packet();
        }
        override public void Serialize()
        {
            TextWriter twr = new StreamWriter(fileName);
            XmlSerializer writer = new XmlSerializer(typeof(Packet));

            writer.Serialize(twr, packet);
            twr.Close();
        }
        override public Packet Deserialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Packet));
            FileStream fs = new FileStream(fileName, FileMode.Open);
            Packet packet = (Packet)serializer.Deserialize(fs);

            fs.Close();

            return packet;
        }
       
    }
}



