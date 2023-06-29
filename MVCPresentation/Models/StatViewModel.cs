using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataObjects;

namespace MVCPresentation.Models
{
    public class StatViewModel
    {
        public string Name { get; set; }
        public List<string> AliasList { get; set; }
        public string NewAlias { get; set; }

        public StatViewModel()
        {
            this.Name = "Failed to initialize";
            this.AliasList = new List<string>();
        }
        public StatViewModel(StatVM source)
        {
            this.Name = source.Name;
            this.AliasList = source.AliasList;
        }

    }
}