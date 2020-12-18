using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.ShipCostDTOS
{
    public class ShipCostDTO
    {

        public int ShipCostId { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public decimal ShipCost1 { get; set; }
    }
}
