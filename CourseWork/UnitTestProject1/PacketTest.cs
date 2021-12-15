using DAL_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramClasses;
using System;
using System.Collections.Generic;

namespace DALL.Classes
{

    [TestClass]
    public class PacketTest
    {
        [TestMethod]
        public void GetSupplierObj()
        {
            Packet packet = new Packet();
            packet.GetSupplierObj();
        }

        [TestMethod]
        public void AddToPacketProduct()
        {
            Packet packet = new Packet();

            packet.AddToPacket(new Product());

            Assert.AreEqual(1,packet.GetProductObj().Count);
            Assert.AreEqual(0, packet.GetSupplierObj().Count);
        }
        [TestMethod]
        public void AddToPacketSupp()
        {
            Packet packet = new Packet();

            packet.AddToPacket(new Supplier());

            Assert.AreEqual(1, packet.GetSupplierObj().Count);
            Assert.AreEqual(0, packet.GetProductObj().Count);
        }
        [TestMethod]
        public void AddToPacketObj()
        {
            Packet packet = new Packet();

            packet.AddToPacket(new object());

            Assert.AreEqual(0, packet.GetSupplierObj().Count);
            Assert.AreEqual(0, packet.GetProductObj().Count);
        }

        [TestMethod]
        public void Reset()
        {
            Packet packet = new Packet();
            packet.AddToPacket(new Supplier());
            packet.AddToPacket(new Product());

            packet.Reset();

            Assert.AreEqual(0, packet.GetSupplierObj().Count);
            Assert.AreEqual(0, packet.GetProductObj().Count);
        }

    }
}
