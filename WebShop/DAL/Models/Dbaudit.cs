using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Dbaudit
    {
        public int EventId { get; set; }
        public string UserId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
    }
}
