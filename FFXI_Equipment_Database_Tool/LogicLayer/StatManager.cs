using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    class StatManager : IStatManager
    {
        private IStatAccessor _statAccessor = null;
        List<StatVM> _masterStatList = null;


        public StatManager()
        {
            _statAccessor = new StatAccessor();
            _masterStatList = RetrieveStatVMList();
        }
        public StatManager(IStatAccessor statAccessor)
        {
            _statAccessor = statAccessor;
            _masterStatList = RetrieveStatVMList();
        }

        public bool AddNewStat(StatVM statToAdd)
        {
            int rowsAdded = 0;
            if(statToAdd == null || statToAdd.Equals(""))
            {
                return false;
            }
            if (statToAdd.Name.Contains('|'))
            {
                throw new ArgumentException("Stat names may not contain the | character.");
            }
            else
            {
                try
                {
                    rowsAdded = _statAccessor.AddStat(statToAdd.Name);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to add " + statToAdd.Name, ex);
                }
            }
            // Add a default alias that matches the new Stat's name
            bool addedAlias = false;
            if (rowsAdded > 0)
            {
                try
                {
                    addedAlias = AddStatAlias(statToAdd, statToAdd.Name);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to add default alias to " + statToAdd.Name, ex);
                }
            }
            return addedAlias;
        }

        public bool AddStatAlias(StatVM statVM, string alias)
        {
            int aliasesAdded = 0;
            if (alias == null && !alias.Equals(""))
            {
                return false;
            }
            else if (alias.Contains('|'))
            {
                throw new ArgumentException("Stat aliases may not contain the | character.");
            }
            else
            {
                try
                {
                    aliasesAdded = _statAccessor.AddStatAlias(statVM.Name, alias);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to add alias to " + statVM.Name, ex);
                }
            }
            return (aliasesAdded > 0);
        }

        public StatVM GetStatVM(string statNameOrAlias)
        {
            if (statNameOrAlias == null || statNameOrAlias.Equals(""))
            {
                throw new Exception("You must enter a value for which to search");
            }
            StatVM result = new StatVM { Name = "404 Stat not found", AliasList = new List<string> { "404 Stat not found" }, Value = 404 };
            try
            {
                result = _statAccessor.SelectStatVMByAlias(statNameOrAlias);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve Stat for " + statNameOrAlias, ex);
            }
            return result;
        }

        public bool RemoveAlias(StatVM stat, string aliasToRemove)
        {
            int rowsAffected = 0;
            if (aliasToRemove != null && !aliasToRemove.Equals("") && stat.AliasList.Contains(aliasToRemove))
            {
                try
                {
                    rowsAffected = _statAccessor.DeleteStatAlias(stat.Name, aliasToRemove);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to remove " + aliasToRemove + " from " + stat.Name, ex);
                }
            }
            return (rowsAffected > 0);
        }

        public bool RenameStat(StatVM stat, string newStatName)
        {
            bool result = false;
            if (stat.Name != null && !stat.Name.Equals("") && !stat.Name.Equals(newStatName))
            {
                try
                {
                    // Make sure that a renamed stat has a new System Alias
                    if (!stat.AliasList.Contains(newStatName))
                    {
                        AddStatAlias(stat, newStatName);
                    }
                    int linecount = _statAccessor.RenameStat(stat.Name, newStatName);
                    if (linecount > 0)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to rename Stat", ex);
                }
            }
            return result;
        }

        public List<StatVM> RetrieveStatVMList()
        {
            List<StatVM> statList;
            try
            {
                statList = _statAccessor.SelectAllStatVMs();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve list of StatVMs from the Database", ex);
            }
            return statList;
        }
    }
}
