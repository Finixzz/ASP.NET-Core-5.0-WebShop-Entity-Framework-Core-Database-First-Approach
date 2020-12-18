using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Dtos.PayMethodDTOS
{
    public class PayMethodDTO
    {
        public int PayMethodId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
