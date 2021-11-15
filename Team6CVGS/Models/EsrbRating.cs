using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class EsrbRating
    {
        public EsrbRating()
        {
            Games = new HashSet<Game>();
        }

        public string Code { get; set; }
        public string EnglishRating { get; set; }
        public string FrenchRating { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
