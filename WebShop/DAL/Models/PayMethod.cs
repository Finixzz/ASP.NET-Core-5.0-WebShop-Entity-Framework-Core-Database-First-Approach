using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PayMethod
    {
        public PayMethod()
        {
            OrderHeaders = new HashSet<OrderHeader>();
        }

        public int PayMethodId { get; set; }
        public string Name { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
