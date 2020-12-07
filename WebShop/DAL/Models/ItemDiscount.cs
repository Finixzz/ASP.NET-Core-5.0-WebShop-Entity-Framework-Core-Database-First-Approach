using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ItemDiscount
    {
        public int ItemDiscountId { get; set; }
        public int ItemId { get; set; }
        public int DiscountId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public short IsActive { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual Item Item { get; set; }
    }
}
