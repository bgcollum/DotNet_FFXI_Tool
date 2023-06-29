using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayerInterfaces;
using LogicLayerInterfaces;
using DataObjects;

namespace LogicLayer
{
    public class EquipmentManager : IEquipmentManager
    {
        // Instantiate object to access Data Access Layer Interface
        private IEquipmentDBAccessor _equipmentAcessor = null;
        // This is used for stat verification before committal to DB
        private List<string> _master_stat_list = null;
        public EquipmentManager()
        {
            _equipmentAcessor = new DataAccessLayer.EquipmentDBAccessor();
            _master_stat_list = _equipmentAcessor.RetrieveMasterStatList();
        }
        public EquipmentManager(IEquipmentDBAccessor statListAcessor)
        {
            _equipmentAcessor = statListAcessor;
        }
        public List<string> RetrieveMasterStatList()
        {
            try
            {
                _master_stat_list = _equipmentAcessor.RetrieveMasterStatList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Logic Layer: Failed to retrieve Master Stat List");
            }
            return _master_stat_list;
        }
        public List<Item> RetrieveMasterEquipmentList()
        {
            List<Item> master_equipment_list = new List<Item>();
            try
            {
                master_equipment_list = _equipmentAcessor.RetrieveMasterEquipmentList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Logic Layer: Failed to retrieve Master Equipment List");
            }
            return master_equipment_list;
        }
        public ItemVM RetrieveItemVMByItemID(int itemID)
        {
            ItemVM itemVM = new ItemVM();
            try
            {
                // Populate stat dictonary for a specific item
                itemVM = _equipmentAcessor.RetrieveDBItemVMByItemID(itemID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Logic Layer: Failed to retrieve stats for: " + itemVM.ItemName);
            }
            return itemVM;
        }

        public bool SendNewItemToDB(Item item)
        {
            return _equipmentAcessor.CreateNewDBItem(item);
        }
        // Update an entire item: Item ID, Name, and all stats
        public bool SendItemUpdateToDB(ItemVM itemVM, int originalItemID, string originalItemName)
        {
            bool finalResult = false;
            bool itemUpdateFailed = false;
            bool statUpdateFailed = false;
            if(itemVM.ItemID != originalItemID || itemVM.ItemName != originalItemName)
            {
                try
                {
                    bool itemUpdated = _equipmentAcessor.UpdateDBItemByItemVM(itemVM, originalItemID);
                }
                catch (Exception ex)
                {
                    itemUpdateFailed = true;
                    throw ex;
                }
            }
            if(!itemUpdateFailed)
            {
                try
                {
                    // Confirm if the item has stats already and delete them if so
                    if (itemVM.ItemStats.Count > 0)
                    {
                        SendDeleteAllStatsOnItemToDB(itemVM);
                    }
                    int stats_added = 0;
                    foreach (KeyValuePair<string, int> stat in itemVM.ItemStats)
                    {
                        if (SendNewItemStatToDB(itemVM, stat.Key, stat.Value))
                        {
                            stats_added++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    statUpdateFailed = true;
                    throw ex;
                }
                
            }
            if(!itemUpdateFailed && !statUpdateFailed)
            {
                finalResult = true;
            }
            return finalResult;
        }

        public bool SendItemDeleteToDBByItemID(int itemID)
        {
            return _equipmentAcessor.DeleteDBItemByItemID(itemID);
        }

        // Stat managers
        public bool SendNewItemStatToDB(ItemVM itemVM, string stat_name, int stat_value)
        {
            bool result = false;
            if(_master_stat_list == null)
            {
                RetrieveMasterEquipmentList();
            }
            if (_master_stat_list.Contains(stat_name))
            {
                result = _equipmentAcessor.AddStatToItemByItemIDNameAndValue(itemVM.ItemID, stat_name, stat_value);
            }
            return result;
        }
        public int SendAllStatsOnItemToDB(ItemVM itemVM)    // This has been superceded by SendItemUpdateToDB
        {
            // Confirm if the item has stats already and delete them if so
            if(itemVM.ItemStats.Count > 0)
            {
                SendDeleteAllStatsOnItemToDB(itemVM);
            }
            int stats_added = 0;
            foreach (KeyValuePair<string, int> stat in itemVM.ItemStats)
            {
                if(SendNewItemStatToDB(itemVM, stat.Key, stat.Value))
                {
                    stats_added++;
                }
            }
            return stats_added;
        }

        public bool SendDeleteStatToDB(ItemVM itemVM, string stat_name)
        {
            try
            {
                return _equipmentAcessor.DeteteStatFromItemByIDAndName(itemVM.ItemID, stat_name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SendDeleteAllStatsOnItemToDB(ItemVM itemVM)
        {
            bool result = false;
            if(itemVM.ItemStats.Count != 0)
            {
                int rows_deleted = _equipmentAcessor.DeleteAllStatsFromItemByItemID(itemVM.ItemID);
                if(rows_deleted > 0)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
