using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.OrderHeaderDTOS
{
    public class AddOrderHeaderDTO
    {
        public int OrderHeaderId { get; set; }

        [Required]
        public int PayMethodId { get; set; }

        [Required]
        public int ShipAddressId { get; set; }

    }
}
