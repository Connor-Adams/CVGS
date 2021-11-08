using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class GamePerspective
    {
        public string Code { get; set; }
        public string EnglishPerspectiveName { get; set; }
        public string FrenchPerspectiveName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
