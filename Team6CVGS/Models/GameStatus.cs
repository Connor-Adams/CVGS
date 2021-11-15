using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class GameStatus
    {
        public GameStatus()
        {
            Games = new HashSet<Game>();
        }

        public string Code { get; set; }
        public string EnglishCategory { get; set; }
        public string FrenchCategory { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
