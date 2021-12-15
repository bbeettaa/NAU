using DALWorckWithDataBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class Sorting
    {
        public enum SortingOreder { ASC = 1, DESC = -1 }
        public SortingOreder sortingOreder { get; set; } = SortingOreder.ASC;
        EntityContext context;
        private delegate string getSortValue(object obj);
        getSortValue sortValue;

        public Sorting(EntityContext context)
        { this.context = context; }




        public List<Object> SortName(List<Object> objList)
        {
            sortValue = context.GetObjName;
            return SortStr(objList);
        }

        public List<Object> SortBrand(List<Object> objList)
        {
            sortValue = context.GetObjBrand;
            return SortStr(objList);
        }

        public List<Object> SortPrice(List<Object> objList)
        {
            sortValue = context.GetObjPrice;
            // SortStr(objList);
            return SortDouble(objList);
        }

        public List<Object> SortLastNameSupp(List<Object> objList)
        {
            sortValue = context.GetObjLastName;
            return SortStr(objList);
        }




        private List<Object> SortStr(List<Object> objList)
        {
            int index = 1;
            while (index < objList.Count)
            {
                if (index > 0)
                {
                    string first = sortValue(objList[index - 1]),
                    second = sortValue(objList[index]);

                    if (first.CompareTo(second) == (int)sortingOreder)
                    {
                        object temp = objList[index - 1];
                        objList[index - 1] = objList[index];
                        objList[index] = temp;
                        index--;
                        continue;
                    }
                }
                index++;
            }
            return objList;
        }

        private List<Object> SortDouble(List<Object> objList)
        {
            int index = 1;
            while (index < objList.Count)
            {
                if (index > 0)
                {
                    double first = double.Parse(sortValue(objList[index - 1]).Replace('.', ',')),
                    second = double.Parse(sortValue(objList[index]).Replace('.', ','));

                    if (first.CompareTo(second) == (int)sortingOreder)
                    {
                        object temp = objList[index - 1];
                        objList[index - 1] = objList[index];
                        objList[index] = temp;
                        index--;
                        continue;
                    }
                }
                index++;
            }
            return objList;
        }
    }
}
