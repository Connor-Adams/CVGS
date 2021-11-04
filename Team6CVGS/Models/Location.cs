using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class Location
    {
        public Location()
        {
            Employees = new HashSet<Employee>();
        }

        public string Gln { get; set; }
        public int Sequence { get; set; }
        public string LocationTypeCode { get; set; }
        public int RegionId { get; set; }
        public string SiteName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string LocalPhone { get; set; }
        public string Fax { get; set; }
        public string TollFreePhone { get; set; }
        public string UserName { get; set; }

        public virtual Country CountryCodeNavigation { get; set; }
        public virtual LocationType LocationTypeCodeNavigation { get; set; }
        public virtual Province ProvinceCodeNavigation { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
