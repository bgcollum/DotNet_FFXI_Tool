using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LogicLayer;
using DataObjects;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class ItemController : Controller
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();

        List<Item> _masterEquipmentList = null;
        public List<StatVM> _masterStatVMList = null;
        public HashSet<string> _statAliasSet = null;

        public ItemController()
        {
            try
            {
                _masterStatVMList = _masterManager.StatManager.RetrieveStatVMList();
                _masterEquipmentList = _masterManager.EquipmentManager.RetrieveMasterEquipmentList();
                _statAliasSet = new HashSet<string>();
                foreach (StatVM statVM in _masterStatVMList)
                {
                    foreach (string alias in statVM.AliasList)
                    {
                        _statAliasSet.Add(alias);
                    }
                }
            }
            catch
            {

            }
        }

        // GET: Item
        public ActionResult Index()
        {
            return View(_masterEquipmentList);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            var itemToView = _masterManager.EquipmentManager.RetrieveItemVMByItemID(id);

            return View(itemToView);
        }

        // GET: Item/Create
        [Authorize(Roles = "Administrator, Contributor")]
        public ActionResult Create()
        {
            //ViewBag.StatList = _masterStatVMList;
            //ViewBag.Aliases = _statAliasSet;
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [Authorize(Roles = "Administrator, Contributor")]
        public ActionResult Create(ItemMVCModel itemMVC)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // Add the item to the DB, cover the stats on the edit page
                    Item item = new Item { ItemID = itemMVC.ItemID, ItemName = itemMVC.ItemName };
                    // RUn update
                    if (_masterManager.EquipmentManager.SendNewItemToDB(item))
                    {
                        // Step to make sure there is at least one stat for the item
                        ItemVM itemVM = new ItemVM() { ItemID = item.ItemID, ItemName = item.ItemName, ItemStats = new Dictionary<string, int>() };
                        _masterManager.EquipmentManager.SendNewItemStatToDB(itemVM, "PlaceholderStat", 0);
                    }
                    
                    return RedirectToAction("EditStats", "Item", new { id = itemMVC.ItemID });
                }
                catch
                {
                    return View("Error");
                }
            }
            return View("Error");
        }

        // GET: Item/Edit/5
        [Authorize(Roles = "Administrator, Contributor")]
        public ActionResult EditItem(int id)
        {
            try
            {
                ItemVM itemVM = _masterManager.EquipmentManager.RetrieveItemVMByItemID(id);
                ItemMVCModel itemMVC = new ItemMVCModel { ItemID = itemVM.ItemID, ItemName = itemVM.ItemName, ItemStats = itemVM.ItemStats };
                itemMVC.OldItemID = itemMVC.ItemID;
                itemMVC.OldItemName = itemMVC.ItemName;
                return View(itemMVC);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Item/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Contributor")]
        public ActionResult EditItem(int id, ItemMVCModel itemMVC)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ItemVM item = new ItemVM();
                    // Check if fields were left blank
                    if (itemMVC.ItemID == null || itemMVC.ItemID <= 0)
                    {
                        item.ItemID = itemMVC.OldItemID;
                    }
                    else
                    {
                        item.ItemID = itemMVC.ItemID;
                    }
                    if (itemMVC.ItemName == null || itemMVC.ItemName.Equals(""))
                    {
                        item.ItemName = itemMVC.OldItemName;
                    }
                    else
                    {
                        item.ItemName = itemMVC.ItemName;
                    }
                    _masterManager.EquipmentManager.SendItemUpdateToDB(item, itemMVC.OldItemID, itemMVC.OldItemName);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View("Error");
        }

        // GET: Item/Edit/5
        [Authorize(Roles = "Administrator, Contributor")]
        public ActionResult EditStats(int id)
        {
            try
            {
                ItemVM itemVM = _masterManager.EquipmentManager.RetrieveItemVMByItemID(id);
                ItemMVCModel itemMVC = new ItemMVCModel { ItemID = itemVM.ItemID, ItemName = itemVM.ItemName, ItemStats = itemVM.ItemStats };
                itemMVC.OldItemID = itemMVC.ItemID;
                itemMVC.OldItemName = itemMVC.ItemName;
                ViewBag.StatList = _masterStatVMList;
                ViewBag.Aliases = _statAliasSet;
                return View(itemMVC);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Item/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Contributor")]
        public ActionResult EditStats(int id, ItemMVCModel itemMVC)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ItemVM itemVM = _masterManager.EquipmentManager.RetrieveItemVMByItemID(id);
                    StatVM stat = _masterManager.StatManager.GetStatVM(itemMVC.NewStatName);
                    _masterManager.EquipmentManager.SendNewItemStatToDB(itemVM, stat.Name, itemMVC.NewStatValue);


                    return RedirectToAction("EditStats", "Item", new { id = itemMVC.ItemID });
                }
                catch
                {
                    return View();
                }
            }
            return View("Error");
        }

        [Authorize(Roles = "Administrator, Contributor")]
        public ActionResult RemoveStatFromItem(int itemID, string statToRemove)
        {
            try
            {
                ItemVM itemVM = _masterManager.EquipmentManager.RetrieveItemVMByItemID(itemID);

                // Prevent removal of system alias
                if (itemVM.ItemStats.Count < 2)
                {
                    ViewBag.Error = "An item must have at least one stat.";
                    return RedirectToAction("EditStats", "Item", new { id = itemID });
                }
                _masterManager.EquipmentManager.SendDeleteStatToDB(itemVM, statToRemove);
                return RedirectToAction("EditStats", "Item", new { id = itemID });
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
