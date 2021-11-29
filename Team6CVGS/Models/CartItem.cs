using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team6CVGS.Models
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public System.DateTime DateCreated { get; set; }

        public Guid GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
