using System;
using System.Collections.Generic;


namespace DAL_Worck_With_DataBases
{
    public interface IDataProvider
    {
        public void Serialize();
        public List<Object> Deserialize();
        public void SaveListToPacket(List<Object> objList);
        public void SetSettings(String fileName);
        public void CheckFile();
    }
}
