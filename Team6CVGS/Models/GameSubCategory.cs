using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class GameSubCategory
    {
        public GameSubCategory()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public int GameCategoryId { get; set; }
        public string EnglishCategory { get; set; }
        public string FrenchCategory { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
