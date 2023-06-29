using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataObjects;

namespace MVCPresentation.Models
{
    public class ItemMVCModel
    {
        public int ItemID { get; set; }
        public int OldItemID { get; set; }
        public string ItemName { get; set; }
        public string OldItemName { get; set; }
        public Dictionary<string, int> ItemStats { get; set; }

        public string NewStatName { get; set; }
        public int NewStatValue { get; set; }
    }
}