using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Stat
    {
        // An abstract stat has a name (StatID)
        public string Name { get; set; }
        // An instance of a stat on an item has a value that can be represented by an integer
        public int? Value { get; set; } // List reference items do not have value populated
    }
    public class StatVM : Stat
    {
        // A StatVM has a searchable list of alternate names it can be referenced under
        public List<string> AliasList { get; set; }
    }
}
