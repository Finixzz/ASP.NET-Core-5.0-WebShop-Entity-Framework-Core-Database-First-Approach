using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ItemBrand
    {
        public ItemBrand()
        {
            Items = new HashSet<Item>();
        }

        public int ItemBrandId { get; set; }
        public string Name { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
