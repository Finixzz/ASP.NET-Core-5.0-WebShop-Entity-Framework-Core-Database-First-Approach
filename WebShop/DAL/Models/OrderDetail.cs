using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailsId { get; set; }
        public int OrderHeaderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal SoldAtPrice { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Item Item { get; set; }
        public virtual OrderHeader OrderHeader { get; set; }
    }
}
