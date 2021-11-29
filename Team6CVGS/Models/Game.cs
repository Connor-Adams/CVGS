﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class Game
    {
        public Game()
        {
            GameEsrbContentDescriptors = new HashSet<GameEsrbContentDescriptor>();
            Products = new HashSet<Product>();
            Reviews = new HashSet<Review>();
        }

        public Guid Guid { get; set; }
        public string GameStatusCode { get; set; }
        public int GameCategoryId { get; set; }
        public int? GameSubCategoryId { get; set; }
        public string EsrbRatingCode { get; set; }
        public string EnglishName { get; set; }
        public string FrenchName { get; set; }
        public bool FrenchVersion { get; set; }
        public string EnglishPlayerCount { get; set; }
        public string FrenchPlayerCount { get; set; }
        public string GamePerspectiveCode { get; set; }
        public string EnglishTrailer { get; set; }
        public string FrenchTrailer { get; set; }
        public string EnglishDescription { get; set; }
        public string FrenchDescription { get; set; }
        public string EnglishDetail { get; set; }
        public string FrenchDetail { get; set; }
        public string UserName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MSRP { get; set; }

        public virtual EsrbRating EsrbRatingCodeNavigation { get; set; }
        public virtual GameCategory GameCategory { get; set; }
        public virtual GamePerspective GamePerspectiveCodeNavigation { get; set; }
        public virtual GameStatus GameStatusCodeNavigation { get; set; }
        public virtual GameSubCategory GameSubCategory { get; set; }
        public virtual ICollection<GameEsrbContentDescriptor> GameEsrbContentDescriptors { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
