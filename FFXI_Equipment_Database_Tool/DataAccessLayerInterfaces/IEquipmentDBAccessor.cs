using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IEquipmentDBAccessor
    {
        // Get the Master Stat List
        List<String> RetrieveMasterStatList();

        // Get the Master Equipment List in searchable form (ItemID, Item_Name)
        List<Item> RetrieveMasterEquipmentList();

        // Item CRUD functions
        // Create new Item from ItemVM input
        bool CreateNewDBItem(Item item);

        // Read Item from DB: retrieve ItemVM (Dictionary<int, string>) equipment view
        ItemVM RetrieveDBItemVMByItemID(int itemID);

        // Update Item on Database from ItemVM input
        bool UpdateDBItemByItemVM(ItemVM itemVM, int originalItemID);

        // Delete item on Database by ItemID
        bool DeleteDBItemByItemID(int itemID);

        // Stat CRUD functions
        // Add a single stat to an item
        bool AddStatToItemByItemIDNameAndValue(int itemID, string stat_name, int stat_value);
        // Read
        // Update
        // Delete All
        int DeleteAllStatsFromItemByItemID(int itemID);
        // Delete Single
        bool DeteteStatFromItemByIDAndName(int itemID, string stat_name);
    }
}
