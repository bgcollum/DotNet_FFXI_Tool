using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Item   // Simplified item, represents item in Master_Equipment_List
    {
        // Numbered item ID. Corresponds to in-game item ID.
        public int ItemID { get; set; }
        // 
        public string ItemName { get; set; }
    }
    public class ItemVM : Item  // Detailed stats of item instances represented in Equip_Stats
    {
        // String represents Stat_Name, from Master Stat List
        // Int represents Stat_Value of stat instance in Equip_Stats
        private Dictionary<string, int> _itemStats;
        public Dictionary<string, int> ItemStats
        {
            // Need this to make sure it doesnt try to add things to an un-initialized dictionary
            get { if (_itemStats == null) { _itemStats = new Dictionary<string, int>(); } return _itemStats; }
            set { _itemStats = value; }
        }
    }
}
