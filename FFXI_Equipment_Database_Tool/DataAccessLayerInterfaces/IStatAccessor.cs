using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IStatAccessor
    {
        // Selects all stats and their aliases
        List<StatVM> SelectAllStatVMs();

        // Select a single StatVM by any of its aliases. Aliases are unique, but one stat can have any number of aliases
        StatVM SelectStatVMByAlias(string nameOfStatAlias);

        // Adds a stat by name
        int AddStat(string statName);

        // Renames a stat
        int RenameStat(string oldStatName, string newStatName);

        // Adds an alias to a stat
        int AddStatAlias(string statName, string alias);

        // Renames a stat's alias
        int RenameStatAlias(string statName, string oldAlias, string newAlias);

        // Deletes a stat's alias
        int DeleteStatAlias(string statName, string alias);
    }
}
