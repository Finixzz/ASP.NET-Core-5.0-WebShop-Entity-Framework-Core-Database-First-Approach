using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Town
    {
        public Town()
        {
            ShipAddresses = new HashSet<ShipAddress>();
        }

        public int TownId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
    }
}
