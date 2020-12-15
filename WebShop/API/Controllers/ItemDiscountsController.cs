using AutoMapper;
using DAL.Dtos.ItemDiscountDTOS;
using DAL.Helpers;
using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDiscountsController : ControllerBase
    {
        private IItemDiscountsSQLRepository _itemDiscountsRepository;
        private IMapper _mapper;

        public ItemDiscountsController(IItemDiscountsSQLRepository _itemDiscountsRepository,
                                       IMapper _mapper)
        {
            this._itemDiscountsRepository = _itemDiscountsRepository;
            this._mapper = _mapper;
        }


        /*
            <summary>
                   Returns discounted items based on on provided query parametes,
                   metadata is provided in headers section
            </summary>
            <remarks>
            Sample request:
                GET /api/itemdiscounts?pageNumber=1&pageSize=25
           </remarks>
           <response code="200">Returns discounted items info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetItemDiscountsByOwnerQueryParametes([FromQuery] OwnerParameters ownersParameters)
        {
            var discountedItems = await _itemDiscountsRepository.GetDiscountedItemsAsync(ownersParameters);

            var metadata = new
            {
                discountedItems.TotalCount,
                discountedItems.PageSize,
                discountedItems.CurrentPage,
                discountedItems.HasNext,
                discountedItems.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var discountedItemDTOs = _mapper.Map<List<ItemDiscount>, List<ItemDiscountDTO>>(discountedItems);
            return Ok(discountedItemDTOs);
        }



        /*
           <summary>
                  Returns single item discount info
           </summary>
           <remarks>
           Sample request:
               GET /api/itemdiscounts/1
          </remarks>
          <response code="200">Returns item discount info if found</response>
          <response code="404">If something goes wrong</response> 
       */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountedItemByIdAsync(int id)
        {
            ItemDiscount discountedItem = await _itemDiscountsRepository.GetByIdAsync(id);

            if (discountedItem == null)
                return NotFound();

            return Ok(_mapper.Map<ItemDiscount, ItemDiscountDTO>(discountedItem));
        }





        /*
             <summary>
                    Saves item discount from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 POST /api/itemdiscounts
                 {
                     "itemId":3,
                     "discountId": 1,
                     "startDate": null,
                     "endDate": null,
                     "isActive": 1
                 }
            </remarks>
            <response code="201">Returns item discount info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request or 
                  if referential integrity is violated eg. if you provide itemId or discountId
                  value that is not present in respective tables
            </response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveItemDiscountAsync(ItemDiscountDTO itemDiscountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ItemDiscount newItemDiscount = await _itemDiscountsRepository.SaveAsync(_mapper.Map<ItemDiscountDTO, ItemDiscount>(itemDiscountDTO));

            itemDiscountDTO.ItemDiscountId = newItemDiscount.ItemDiscountId;

            return CreatedAtAction(nameof(itemDiscountDTO), new { id = itemDiscountDTO.ItemDiscountId }, itemDiscountDTO);
        }




        /*
            <summary>
                   Edits existing item discount data from raw JSON
            </summary>
            <remarks>
                Sample request:

                PUT /api/itemdiscounts/1
                {
                   "itemDiscountId":2,
                   "itemId":3,
                   "discountId": 1,
                   "startDate": null,
                   "endDate": null,
                   "isActive": 0
                 }
           </remarks>
           <response code="200">Returns updated item discount info if okay</response>
           <response code="400">If model state is not valid or supplied URI id doesen't match 
           itemDiscountId that is provided in json object</response> 
           <response code="404">If item discount doesen't exist in database</response>
           <response code="500">If JSON object is not structured as sample request or 
                  if referential integrity is violated eg. you provide itemId or discountId
                  value that is not present in respective tables
            </response> 

        */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditItemDiscountAsync(ItemDiscountDTO itemDiscountDTO,int id)
        {
            if (!ModelState.IsValid || (itemDiscountDTO.ItemDiscountId != id))
                return BadRequest();

            ItemDiscount itemDiscountInDb = await _itemDiscountsRepository.GetByIdAsync(id);
            if (itemDiscountInDb == null)
                return NotFound();

            _mapper.Map(itemDiscountDTO, itemDiscountInDb);

            itemDiscountDTO = _mapper.Map<ItemDiscount, ItemDiscountDTO>(await _itemDiscountsRepository.EditAsync(itemDiscountInDb, id));

            return Ok(itemDiscountDTO);           
        }



        /*
             <summary>
                    Deletes existing item discount from database
             </summary>
             <remarks>
             Sample request:
            
                 DELETE /api/itemdiscounts/1
                 
            </remarks>
            <response code="200">Returns deleted item discount</response>
            <response code="500">If item discount doesen't exist in database</response>
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDiscountAsync(int id)
        {
            return Ok(_mapper.Map<ItemDiscount,ItemDiscountDTO>(await _itemDiscountsRepository.DeleteAsync(id)));
        }
    }
}
