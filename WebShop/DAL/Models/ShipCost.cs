using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ShipCost
    {
        public int ShipCostId { get; set; }
        public int CountryId { get; set; }
        public decimal ShipCost1 { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Country Country { get; set; }
    }
}
