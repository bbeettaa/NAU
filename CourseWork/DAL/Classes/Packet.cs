using ProgramClasses;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DAL_Classes
{
    [Serializable]
    [XmlRoot("___XML ROoT (-_-)___")]
    //[JsonInclude]
    public class Packet
    {
        [JsonInclude]
        [XmlArrayAttribute("Product")]
        public Product[] products;
        [JsonInclude]
        [XmlArrayAttribute("Supplier")]
        public List<Supplier> suppliers;


        public Packet()
        {
            Reset();
        }
        public void AddToPacket(Object obj)
        {
            if(obj is Product)
                AddToPacket(obj as Product);
            if (obj is Supplier)
                AddToPacket(obj as Supplier);

        }
        private void AddToPacket(Product product)
        {
            Array.Resize(ref products, products.Length + 1);
            products[products.Length-1] = product;
        }
        private void AddToPacket(Supplier supplier)
        {
            suppliers.Add(supplier);
        }

        public List<Object> GetProductObj()
        {
            List<Object> list = new List<Object>();
            foreach (var x in products)
                list.Add(x);


            return list;
        }
        public List<Object> GetSupplierObj()
        {
            List<Object> list = new List<Object>();
            foreach (var x in suppliers)
                list.Add(x);


            return list;
        }
        public void Reset()
        {
            products = Array.Empty<Product>();
            suppliers = new List<Supplier>();
        }
    }
}
