using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Country
    {
        public Country()
        {
            ShipCosts = new HashSet<ShipCost>();
            Towns = new HashSet<Town>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<ShipCost> ShipCosts { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
    }
}
