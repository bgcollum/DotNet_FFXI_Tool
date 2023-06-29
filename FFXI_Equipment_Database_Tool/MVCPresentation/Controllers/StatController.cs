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
    [Authorize]
    public class StatController : Controller
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        public List<StatVM> _masterStatVMList = null;
        public HashSet<string> _statAliasSet = null;

        public StatController()
        {
            RefreshMasterStatList();
        }

        // GET: Stat
        public ActionResult Index()
        {
            return View(_masterStatVMList);
        }

        // GET: Stat/Details/5
        public ActionResult Details(string id)
        {
            StatVM stat = new StatVM { Name = "Stat retrieval failed", AliasList = new List<string> { "Failed to retrieve aliases" } };
            try
            {
                stat = _masterManager.StatManager.GetStatVM(id);
            }
            catch (Exception)
            {
                throw;
            }
            ViewBag.AliasSet = _statAliasSet;
            return View(stat);
        }

        // GET: Stat/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.Title = "New Stat";
            return View();
        }

        // POST: Stat/Create
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(StatVM stat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _masterManager.StatManager.AddNewStat(stat);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Stat/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id)
        {
            try
            {
                StatVM statVM = _masterManager.StatManager.GetStatVM(id);
                StatViewModel statViewModel = new StatViewModel(statVM);
                return View(statViewModel);
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Stat/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id, StatViewModel statViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatVM stat = _masterManager.StatManager.GetStatVM(id);
                    if (statViewModel.NewAlias != null && !statViewModel.NewAlias.Equals(""))
                    {
                        // Add the new alias to the stat
                        _masterManager.StatManager.AddStatAlias(stat, statViewModel.NewAlias);
                    }
                    if (statViewModel.Name != null && !statViewModel.Name.Equals("") && !statViewModel.Name.Equals(stat.Name))
                    {
                        // Rename the stat
                        _masterManager.StatManager.RenameStat(stat, statViewModel.Name);
                    }
                    return RedirectToAction("Edit", "Stat", new { id = id });
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        //public ActionResult AddAliasToStat(string id, string aliasToAdd)
        //{
        //    try
        //    {
        //        var statVM = _masterManager.StatManager.GetStatVM(id);
        //        _masterManager.StatManager.AddStatAlias(statVM, aliasToAdd);

        //    }
        //    catch (Exception)
        //    {
        //        return View("Error");
        //    }

        //    return RedirectToAction("Details", "Stat", id);
        //}

        [Authorize(Roles = "Administrator")]
        public ActionResult RemoveAliasFromStat(string statName, string aliasToRemove)
        {
            try
            {
                // Prevent removal of system alias
                if (statName.Equals(aliasToRemove))
                {
                    ViewBag.Error = "You may not remove the system alias for this stat.";
                    return RedirectToAction("Edit", "Stat", new { id = statName });
                }
                var statVM = _masterManager.StatManager.GetStatVM(statName);
                _masterManager.StatManager.RemoveAlias(statVM, aliasToRemove);
                return RedirectToAction("Edit", "Stat", new { id = statName });
            }
            catch
            {
                return View("Error");
            }
        }
        public void RefreshMasterStatList()
        {
            try
            {
                _masterStatVMList = _masterManager.StatManager.RetrieveStatVMList();
                _statAliasSet = new HashSet<string>();
                foreach (StatVM statVM in _masterStatVMList)
                {
                    foreach (string alias in statVM.AliasList)
                    {
                        _statAliasSet.Add(alias);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
