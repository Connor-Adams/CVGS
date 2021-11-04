using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class LocationType
    {
        public LocationType()
        {
            Locations = new HashSet<Location>();
        }

        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
