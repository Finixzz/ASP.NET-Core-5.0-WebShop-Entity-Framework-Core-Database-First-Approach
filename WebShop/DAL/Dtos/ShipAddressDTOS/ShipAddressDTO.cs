using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.ShipAddressDTOS
{
    public class ShipAddressDTO
    {
        public int ShipAddressId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TownId { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; }

        [Required]
        public int StreetNumber { get; set; }
    }
}
