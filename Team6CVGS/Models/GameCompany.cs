using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class GameCompany
    {
        public GameCompany()
        {
            ProductDevelopers = new HashSet<Product>();
            ProductPublishers = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }

        public virtual ICollection<Product> ProductDevelopers { get; set; }
        public virtual ICollection<Product> ProductPublishers { get; set; }
    }
}
