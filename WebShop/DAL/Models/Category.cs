using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
