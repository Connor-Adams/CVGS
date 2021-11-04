using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class PopulationClassification
    {
        public PopulationClassification()
        {
            Populations = new HashSet<Population>();
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Population> Populations { get; set; }
    }
}
