using DAL_Classes;
using ProgramClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DALWorckWithDataBases
{
    class BinaryProvider : AbstarctDataProvider
    {
        public BinaryProvider()
        {
            packet = new Packet();
        }
        override public void Serialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream (fileName, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, packet);

            stream.Close();
        }
        override public Packet Deserialize()
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            IFormatter formatter = new BinaryFormatter();

            packet = (Packet)formatter.Deserialize(fs);

            fs.Close();

            return packet;
        }
    }
}
