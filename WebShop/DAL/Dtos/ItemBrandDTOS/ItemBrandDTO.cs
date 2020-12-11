using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.ItemBrandDTOS
{
    public class ItemBrandDTO
    {
        public int ItemBrandId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
