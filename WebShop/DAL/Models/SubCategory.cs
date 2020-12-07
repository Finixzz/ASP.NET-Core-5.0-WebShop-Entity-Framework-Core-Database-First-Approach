using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Items = new HashSet<Item>();
        }

        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
