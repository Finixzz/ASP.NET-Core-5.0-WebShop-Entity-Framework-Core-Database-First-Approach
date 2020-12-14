using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.DiscountDTOS
{
    public class DiscountDTO
    {
        public int DiscountId { get; set; }

        [Required]
        [Range(5,50)]
        public int DiscountRate { get; set; }
    }
}
