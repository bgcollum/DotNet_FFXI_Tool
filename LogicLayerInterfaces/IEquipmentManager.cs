using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEquipmentManager
    {
        // Master Lists
        // Retrive the Master Stat List from Data Access
        List<string> RetrieveMasterStatList();
        // Retrieve the Master Equipment List (Simple) from Data Access
        List<Item> RetrieveMasterEquipmentList();

        // Item CRUD functions
        // Create Item
        bool SendNewItemToDB(Item item);
        // Read Item
        ItemVM RetrieveItemVMByItemID(int itemID);
        // Update Item
        bool SendItemUpdateToDB(ItemVM itemVM, int originalItemID, string originalItemName);
        // Delete Item
        bool SendItemDeleteToDBByItemID(int itemID);

        // Stat Functions
        // Add new Stat (single)
        bool SendNewItemStatToDB(ItemVM itemVM, string stat_name, int stat_value);
        int SendAllStatsOnItemToDB(ItemVM itemVM);
        // Read Stat (Not currently needed)
        // Update Stat
        // Delete Stat (single)
        bool SendDeleteStatToDB(ItemVM itemVM, string stat_name);
        // Delete all stats
        bool SendDeleteAllStatsOnItemToDB(ItemVM itemVM);
    }
}
