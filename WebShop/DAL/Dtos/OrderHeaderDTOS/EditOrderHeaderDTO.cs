using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.OrderHeaderDTOS
{
    public class EditOrderHeaderDTO
    {
        [Required]
        public int OrderHeaderId { get; set; }

        [Required]
        public int PayMethodId { get; set; }

        [Required]
        public int ShipAddressId { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public short IsShipped { get; set; }

        [Required]
        public short IsPayed { get; set; }
    }
}
