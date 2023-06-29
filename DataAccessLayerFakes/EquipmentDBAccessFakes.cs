using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class EquipmentDBAccessFakes : IEquipmentDBAccessor
    {
        List<string> testStatList = new List<string>();
        List<Item> testItemList = new List<Item>();

        public EquipmentDBAccessFakes()
        {
            // RetrieveMasterStatList() returns in alphabetical order
            testStatList.Add("AGI");
            testStatList.Add("CHR");
            testStatList.Add("DEX");
            testStatList.Add("INT");
            testStatList.Add("MND");
            testStatList.Add("STR");
            testStatList.Add("VIT");

            // Simple test items
            testItemList.Add(new Item { ItemID = 1, ItemName = "TestItem1" });
            testItemList.Add(new Item { ItemID = 2, ItemName = "TestItem2" });
            testItemList.Add(new Item { ItemID = 3, ItemName = "TestItem3" });
        }

        public List<string> RetrieveMasterStatList()
        {
            return testStatList;
        }

        public List<Item> RetrieveMasterEquipmentList()
        {
            return testItemList;
        }

        public bool CreateNewDBItem(Item item)
        {
            throw new NotImplementedException();
        }

        public ItemVM RetrieveDBItemVMByItemID(int itemID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDBItemByItemVM(ItemVM itemVM, int originalItemID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDBItemByItemID(int ItemID)
        {
            throw new NotImplementedException();
        }

        public bool AddStatToItemByItemIDNameAndValue(int itemID, string stat_name, int stat_value)
        {
            throw new NotImplementedException();
        }

        public int DeleteAllStatsFromItemByItemID(int itemID)
        {
            throw new NotImplementedException();
        }

        public bool DeteteStatFromItemByIDAndName(int itemID, string stat_name)
        {
            throw new NotImplementedException();
        }
    }
}
