using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class Province
    {
        public Province()
        {
            Locations = new HashSet<Location>();
            People = new HashSet<Person>();
            Populations = new HashSet<Population>();
            Suppliers = new HashSet<Supplier>();
        }

        public string Code { get; set; }
        public string CountryCode { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }
        public double? FederalTax { get; set; }
        public string FederalTaxAcronym { get; set; }
        public double? ProvincialTax { get; set; }
        public string ProvincialTaxAcronym { get; set; }
        public bool? PstOnGst { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Population> Populations { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
