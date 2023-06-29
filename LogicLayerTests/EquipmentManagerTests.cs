using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class EquipmentManagerTests
    {
        // Instantiate a StatManager object to pass stuff to
        private EquipmentManager _equipmentManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _equipmentManager = new EquipmentManager();   // This points to Data Acess Layer without arguments
            // _equipmentManager = new EquipmentManager(new EquipmentDBAccessFakes());
        }

        [TestMethod]
        public void Test_RetrieveMasterStatList()
        {
            // Fakes pass/fail successfully

            // Test with SQL server also pass/fail successfully

            // Arrange (This mirrors DB instances)
            List<string> sampleList = new List<string>();
            sampleList.Add("AGI");
            sampleList.Add("CHR");
            sampleList.Add("DEX");
            sampleList.Add("INT");
            sampleList.Add("MND");
            sampleList.Add("STR");
            sampleList.Add("VIT");

            // Act
            List<string> retrieved_list = _equipmentManager.RetrieveMasterStatList();
            // Assert
            Assert.AreEqual(sampleList.Count, retrieved_list.Count);
            Assert.AreEqual(sampleList[0], retrieved_list[0]);
            Assert.AreEqual(sampleList[1], retrieved_list[1]);
            Assert.AreEqual(sampleList[2], retrieved_list[2]);
            Assert.AreEqual(sampleList[3], retrieved_list[3]);
            Assert.AreEqual(sampleList[4], retrieved_list[4]);
            Assert.AreEqual(sampleList[5], retrieved_list[5]);
            Assert.AreEqual(sampleList[6], retrieved_list[6]);

        }
        [TestMethod]
        public void Test_RetrieveMasterEquipmentList()
        {
            /*// Arrange (From Fakes)
            List<Item> sampleItemList = new List<Item>();
            sampleItemList.Add(new Item { ItemID = 1, ItemName = "TestItem1" });
            sampleItemList.Add(new Item { ItemID = 2, ItemName = "TestItem2" });
            sampleItemList.Add(new Item { ItemID = 3, ItemName = "TestItem3" });
            // Act
            List<Item> retrievedList = _equipmentManager.RetrieveMasterEquipmentList();
            // Assert
            Assert.AreEqual(sampleItemList.Count, retrievedList.Count);
            Assert.AreEqual(sampleItemList[0].ItemID, retrievedList[0].ItemID);
            Assert.AreEqual(sampleItemList[0].ItemName, retrievedList[0].ItemName);
            Assert.AreEqual(sampleItemList[1].ItemID, retrievedList[1].ItemID);
            Assert.AreEqual(sampleItemList[1].ItemName, retrievedList[1].ItemName);
            Assert.AreEqual(sampleItemList[2].ItemID, retrievedList[2].ItemID);
            Assert.AreEqual(sampleItemList[2].ItemName, retrievedList[2].ItemName);
            // All fakes pass test*/

            // Arrange (From DB)
            List<Item> sampleItemList = new List<Item>();
            sampleItemList.Add(new Item { ItemID = 1, ItemName = "Helmet of thinking" });
            sampleItemList.Add(new Item { ItemID = 2, ItemName = "Cuirass of not getting stabbed" });
            sampleItemList.Add(new Item { ItemID = 3, ItemName = "Gauntlets of squishing" });
            sampleItemList.Add(new Item { ItemID = 4, ItemName = "Pants of keeping your bits in" });
            sampleItemList.Add(new Item { ItemID = 5, ItemName = "Boots of running urgently" });
            sampleItemList.Add(new Item { ItemID = 6, ItemName = "Codpiece of variable attractiveness" });
            // Act
            List<Item> retrievedList = _equipmentManager.RetrieveMasterEquipmentList();
            // Assert
            Assert.AreEqual(sampleItemList.Count, retrievedList.Count);
            Assert.AreEqual(sampleItemList[0].ItemID, retrievedList[0].ItemID);
            Assert.AreEqual(sampleItemList[0].ItemName, retrievedList[0].ItemName);
            Assert.AreEqual(sampleItemList[1].ItemID, retrievedList[1].ItemID);
            Assert.AreEqual(sampleItemList[1].ItemName, retrievedList[1].ItemName);
            Assert.AreEqual(sampleItemList[2].ItemID, retrievedList[2].ItemID);
            Assert.AreEqual(sampleItemList[2].ItemName, retrievedList[2].ItemName);
            // All fakes pass test

        }
    }
}
