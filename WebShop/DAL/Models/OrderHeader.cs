using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class OrderHeader
    {
        public OrderHeader()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderHeaderId { get; set; }
        public int PayMethodId { get; set; }
        public int ShipAddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public short IsShipped { get; set; }
        public short IsPayed { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual PayMethod PayMethod { get; set; }
        public virtual ShipAddress ShipAddress { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
