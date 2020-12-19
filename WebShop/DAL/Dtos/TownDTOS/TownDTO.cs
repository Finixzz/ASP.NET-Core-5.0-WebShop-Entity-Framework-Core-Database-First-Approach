using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.TownDTOS
{
    public class TownDTO
    {
        public int TownId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
