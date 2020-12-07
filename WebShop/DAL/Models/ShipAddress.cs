using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ShipAddress
    {
        public ShipAddress()
        {
            OrderHeaders = new HashSet<OrderHeader>();
        }

        public int ShipAddressId { get; set; }
        public string UserId { get; set; }
        public int TownId { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Town Town { get; set; }
        public virtual AspNetUser User { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
