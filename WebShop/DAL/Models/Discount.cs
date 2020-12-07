using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Discount
    {
        public Discount()
        {
            ItemDiscounts = new HashSet<ItemDiscount>();
        }

        public int DiscountId { get; set; }
        public int DiscountRate { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<ItemDiscount> ItemDiscounts { get; set; }
    }
}
