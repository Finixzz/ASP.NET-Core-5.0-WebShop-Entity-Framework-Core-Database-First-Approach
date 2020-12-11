using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.ItemDTOS
{
    public class ItemDTO
    {
        public int ItemId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int UnitsInStock { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
