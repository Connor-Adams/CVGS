using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public Guid GameGuid { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewContent { get; set; }
        public int ReviewRaiting { get; set; }
        public bool Approved { get; set; }

        public virtual Game GameGu { get; set; }
        public virtual Person User { get; set; }
    }
}
