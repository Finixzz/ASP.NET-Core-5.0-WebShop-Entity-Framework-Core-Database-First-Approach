using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.OrderDetailDTOS
{
    public class OrderDetailDTO
    {
        public int OrderDetailsId { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        [Range(1,10)]
        public int Quantity { get; set; }

        [Required]
        public decimal SoldAtPrice { get; set; }
    }
}
