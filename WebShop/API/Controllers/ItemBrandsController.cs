using AutoMapper;
using DAL.Dtos.ItemBrandDTOS;
using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemBrandsController : ControllerBase
    {
        private IItemBrandSQLRepository _itemBrandRepository;
        private IMapper _mapper;

        public ItemBrandsController(IItemBrandSQLRepository _itemBrandRepository,
                                    IMapper _mapper)
        {
            this._itemBrandRepository = _itemBrandRepository;
            this._mapper = _mapper;
        }


        /*
            <summary>
                   Returns all itembrands
            </summary>
            <remarks>
            Sample request:

                GET /api/itembrands
           </remarks>
           <response code="200">Returns itembrands info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetAllItemBrandsAsync()
        {
            var itemBrandDTOS = _mapper
                .Map<IEnumerable<ItemBrand>, IEnumerable<ItemBrandDTO>>
                (await _itemBrandRepository.GetAllAsync());
            return Ok(itemBrandDTOS);
        }


        /*
            <summary>
                   Returns single itembrand
            </summary>
            <remarks>
            Sample request:

                GET /api/itembrands/1

           </remarks>
           <response code="200">Returns itembrand info if found</response>
           <response code="404">If something goes wrong</response> 
        */

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemBrandByIdAsync(int id)
        {
            ItemBrand itemBrandInDb = await _itemBrandRepository.GetByIdAsync(id);

            if (itemBrandInDb == null)
                return NotFound();

            return Ok(_mapper.Map<ItemBrand, ItemBrandDTO>(itemBrandInDb));
        }



        /*
             <summary>
                    Creates itembrand from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 POST /api/itembrand
                 {
                    "name": "First itembrand",
                 }
            </remarks>
            <response code="201">Returns itembrand info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request</response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveItemBrandAsync(ItemBrandDTO itemBrandDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ItemBrand newItemBrand = await _itemBrandRepository.SaveAsync(_mapper.Map<ItemBrandDTO, ItemBrand>(itemBrandDTO));

            itemBrandDTO.ItemBrandId = newItemBrand.ItemBrandId;

            return CreatedAtAction(nameof(itemBrandDTO), new { id = itemBrandDTO.ItemBrandId }, itemBrandDTO);
        }



        /*
             <summary>
                    Edits existing itembrand data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 PUT /api/itembrands/1
                 {
                    "itemBrandId":1,
                    "name": "First itembrand name edit",
                  }
            </remarks>
            <response code="200">Returns updated itembrand info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            itemBrandId that is provided in json object</response> 
            <response code="404">If itembrand doesen't exist in database</response>
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditItemBrandAsync(ItemBrandDTO itemBrandDTO,int id)
        {
            if (!ModelState.IsValid || (itemBrandDTO.ItemBrandId != id))
                return BadRequest();

            ItemBrand itemBrandInDb = await _itemBrandRepository.GetByIdAsync(id);

            if (itemBrandInDb == null)
                return NotFound();

            _mapper.Map(itemBrandDTO, itemBrandInDb);
            itemBrandDTO = _mapper.Map<ItemBrand, ItemBrandDTO>(await _itemBrandRepository.EditAsync(itemBrandInDb, id));

            return Ok(itemBrandDTO);
        }



        /*
           <summary>
                  Deletes existing itembrand from database
           </summary>
           <remarks>
           Sample request:

               DELETE /api/itembrands/1

          </remarks>
          <response code="200">Returns deleted itembrand</response>
          <response code="500"> If itembrand doesen't exist in database or itembrand 
                       we want to delete is referenced by Item (ON DELETE NO ACTION)      
          </response>
       */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemBrandAsync(int id)
        {
            return Ok(_mapper.Map<ItemBrand, ItemBrandDTO>(await _itemBrandRepository.DeleteAsync(id)));
        }
    }
}
