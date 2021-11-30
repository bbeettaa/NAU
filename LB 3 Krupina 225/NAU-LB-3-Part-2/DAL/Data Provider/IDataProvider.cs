using DAL_Classes;
using ProgramClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace DALWorckWithDataBases
{
    public interface IDataProvider
    {
        public void Serialize();
        public List<Object> Deserialize();
        public void SaveListToPacket(List<Object> objList);
        public void SetFileName(String fileName);
        public bool CheckFile();
        public void CreateFile();
    }
}
