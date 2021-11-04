using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class Region
    {
        public Region()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
