using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class Country
    {
        public Country()
        {
            Locations = new HashSet<Location>();
            People = new HashSet<Person>();
            Populations = new HashSet<Population>();
            Suppliers = new HashSet<Supplier>();
        }

        public string Code { get; set; }
        public string Alpha2Code { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Population> Populations { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
