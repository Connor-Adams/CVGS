using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Team6CVGS.Common;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class EventLog
    {
        public Guid Guid { get; set; }
        [Required]
        [DateMin]
        public DateTime Date { get; set; }
        public string Category { get; set; }
        [Required]
        [StringLength(6)]
        public string Event { get; set; }
        [Required]
        [StringLength(4000)]
        public string Detail { get; set; }
    }
}
