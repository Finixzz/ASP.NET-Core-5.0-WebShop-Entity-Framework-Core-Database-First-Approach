using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.ModelHelpers
{
    public class Order
    {
        public int OrderHeaderId { get; set; }

        [Required]
        public int PayMethodId { get; set; }

        [Required]
        public int ShipAddressId { get; set; }

        public List<int> OrderDetailList { get; set; }

        [Required]
        public List<int> ItemList { get; set; }

        [Required]
        public List<int> QuantityList { get; set; }

        [Required]
        public List<decimal> SoldAtPriceList { get; set; }

        public Order()
        {
            OrderDetailList = new List<int>();
            ItemList = new List<int>();
            QuantityList = new List<int>();
            SoldAtPriceList = new List<decimal>();
        }
    }
}
