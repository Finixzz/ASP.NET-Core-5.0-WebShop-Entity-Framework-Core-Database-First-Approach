using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.ItemDiscountDTOS
{
    public class ItemDiscountDTO
    {
        public int ItemDiscountId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int DiscountId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public short IsActive { get; set; }


    }
}
