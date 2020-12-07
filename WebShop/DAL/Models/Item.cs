using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemDiscounts = new HashSet<ItemDiscount>();
            ItemPictures = new HashSet<ItemPicture>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ItemId { get; set; }
        public int? BrandId { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ItemBrand Brand { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<ItemDiscount> ItemDiscounts { get; set; }
        public virtual ICollection<ItemPicture> ItemPictures { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
