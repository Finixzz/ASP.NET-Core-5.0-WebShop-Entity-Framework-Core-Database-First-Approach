using AutoMapper;
using DAL.Dtos.DiscountDTOS;
using DAL.Models;
using DAL.ServiceInterfaces;
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

        private IDiscountSQLRepository _discountRepository;
        private IMapper _mapper;

        public DiscountsController(IDiscountSQLRepository _discountRepository,
                                   IMapper _mapper)
        {
            this._discountRepository = _discountRepository;
            this._mapper = _mapper;
        }


        /*
           <summary>
                  Returns all discounts
           </summary>
           <remarks>
           Sample request:
               GET /api/discounts
          </remarks>
          <response code="200">Returns discount info if okay</response>
       */
        [HttpGet]
        public async Task<IActionResult> GetAllDiscountsAsync()
        {
            var discountDTOS = _mapper.Map<IEnumerable<Discount>, IEnumerable<DiscountDTO>>
                                    (await _discountRepository.GetAllAsync());

            return Ok(discountDTOS);
        }



        /*
            <summary>
                   Returns single discount
            </summary>
            <remarks>
            Sample request:
                GET /api/discounts/1
           </remarks>
           <response code="200">Returns discount info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountByIdAsync(int id)
        {
            Discount discountInDb = await _discountRepository.GetByIdAsync(id);

            if (discountInDb == null)
                return NotFound();

            return Ok(_mapper.Map<Discount,DiscountDTO>(discountInDb));
        }


        /*
             <summary>
                    Creates discount from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 POST /api/discounts
                 {
                    "discountRate": 10,
                 }
            </remarks>
            <response code="201">Returns discount info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request</response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveDiscountAsync(DiscountDTO discountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Discount newDiscount = await _discountRepository.SaveAsync(_mapper.Map<DiscountDTO, Discount>(discountDTO));

            discountDTO.DiscountId = newDiscount.DiscountId;

            return CreatedAtAction(nameof(discountDTO), new { id = discountDTO.DiscountId }, discountDTO);

        }



        /*
            <summary>
                   Edits existing discount data from raw JSON
            </summary>
            <remarks>
                Sample request:

                PUT /api/discounts/1
                {
                   "discountId":1,
                   "discountRate": 12,
                 }
           </remarks>
           <response code="200">Returns updated discount info if okay</response>
           <response code="400">If model state is not valid or supplied URI id doesen't match 
           discountId that is provided in json object</response> 
           <response code="404">If discount doesen't exist in database</response>

        */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDiscountAsync(DiscountDTO discountDTO, int id)
        {
            if (!ModelState.IsValid || (discountDTO.DiscountId != id))
                return BadRequest();

            Discount discountInDb = await _discountRepository.GetByIdAsync(id);

            if (discountInDb == null)
                return NotFound();

            _mapper.Map(discountDTO, discountInDb);

            discountDTO =_mapper.Map<Discount,DiscountDTO>( await _discountRepository.EditAsync(discountInDb, id));

            return Ok(discountDTO);
        }



        /*
             <summary>
                    Deletes existing discount from database
             </summary>
             <remarks>
             Sample request:
            
                 DELETE /api/discounts/1
                 
            </remarks>
            <response code="200">Returns deleted discount</response>
            <response code="500">If discount doesen't exist in database or discount we want to
                delete is referenced by ItemDiscounts table (ON DELETE NO ACTION)      
            </response>
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountAsync(int id)
        {
            return Ok(_mapper.Map<Discount,DiscountDTO>(await _discountRepository.DeleteAsync(id)));
        }

    }
}
