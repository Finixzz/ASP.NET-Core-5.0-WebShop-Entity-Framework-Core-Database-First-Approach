using DAL.Dtos.DiscountDTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {



        [HttpGet]
        public async Task<IActionResult> GetAllDiscountsAsync()
        {


            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountByIdAsync(int id)
        {


            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveDiscountAsync(DiscountDTO discountDTO)
        {


            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditDiscountAsync(DiscountDTO discountDTO, int id)
        {
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountAsync(int id)
        {
            return Ok();
        }

    }
}
