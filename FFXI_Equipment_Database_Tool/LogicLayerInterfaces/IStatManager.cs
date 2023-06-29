using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IStatManager
    {
        // Retrieve all stats and aliases from DB
        List<StatVM> RetrieveStatVMList();

        StatVM GetStatVM(string statNameOrAlias);

        // Add a new statVM, with aliases, to the system
        bool AddNewStat(StatVM statToAdd);

        // Adds an alias to a stat
        bool AddStatAlias(StatVM statVM, string alias);

        bool RemoveAlias(StatVM stat, string aliasToRemove);

        bool RenameStat(StatVM stat, string newStatName);

    }
}
