using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.CountryDTOS
{
    public class CountryDTO
    {
        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
