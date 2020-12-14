using AutoMapper;
using DAL.Dtos.ItemDTOS;
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
    public class ItemsController : ControllerBase
    {
        private IItemSQLRepository _itemRepository;
        private IMapper _mapper;
        public ItemsController(IItemSQLRepository _itemRepository, IMapper _mapper)
        {
            this._itemRepository = _itemRepository;
            this._mapper = _mapper;
        }


        /*
            <summary>
                   Returns items based on provided query parametes,
                   metadata is provided in headers section
            </summary>
            <remarks>
            Sample request:
                GET /api/items/pageNumber=1&pageSize=25
           </remarks>
           <response code="200">Returns items info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetItemsByOwnerParametersAsync([FromQuery] OwnerParameters ownersParameters)
        {
            var items = await _itemRepository.GetItemsAsync(ownersParameters);

            var metadata = new
            {
                items.TotalCount,
                items.PageSize,
                items.CurrentPage,
                items.HasNext,
                items.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var itemDTOs = _mapper.Map<List<Item>, List<ItemDTO>>(items);
            return Ok(itemDTOs);
        }



        /*
            <summary>
                   Returns single item
            </summary>
            <remarks>
            Sample request:
                GET /api/items/1
           </remarks>
           <response code="200">Returns item info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemByIdAsync(int id)
        {
            Item itemInDb = await _itemRepository.GetByIdAsync(id);

            if (itemInDb == null)
                return NotFound();

            return Ok(_mapper.Map<Item, ItemDTO>(itemInDb));
        }



        /*
            <summary>
                Creates item from raw JSON
            </summary>
            <remarks>
                Sample request:
            
                POST /api/items
                {
                    "brandId":1,
                    "subCategoryId":1,
                    "name": "First item",
                    "description": "First item description",
                    "unitsInStock":50,
                    "unitPrice": 5
                }
            </remarks>
            <response code="201">Returns item info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request
            or if referential integrity is violated eg. if supplied brandId or subCategoryId 
            doesen't exist in respective tables</response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveItemAsync(ItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Item newItem = await _itemRepository.SaveAsync(_mapper.Map<ItemDTO, Item>(itemDTO));

            itemDTO.ItemId = newItem.ItemId;

            return CreatedAtAction(nameof(itemDTO), new { id = itemDTO.ItemId }, itemDTO);

        }


        /*
             <summary>
                    Edits existing item data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                    POST /api/items/1
                    {
                        "itemId":1,
                        "brandId":1,
                        "subCategoryId":1,
                        "name": "First item name edited",
                        "description": "First item description edited",
                        "unitsInStock":50,
                        "unitPrice": 5
                    }
            </remarks>
            <response code="200">Returns updated item info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            itemId that is provided in json object</response> 
            <response code="404">If item doesen't exist in database</response>
            <response code="500">If JSON object is not structured as sample request
            or if referential integrity is violated eg. if supplied brandId or subCategoryId 
            doesen't exist in respective tables</response> 
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditItemAsync(ItemDTO itemDTO, int id)
        {
            if (!ModelState.IsValid || (itemDTO.ItemId != id))
                return BadRequest();

            Item itemInDb = await _itemRepository.GetByIdAsync(id);

            if (itemInDb == null)
                return NotFound();

            _mapper.Map(itemDTO, itemInDb);
            itemDTO = _mapper.Map<Item, ItemDTO>(await _itemRepository.EditAsync(itemInDb, id));

            return Ok(itemDTO);
        }



        /*
           <summary>
                  Deletes existing item from database
           </summary>
           <remarks>
           Sample request:
               DELETE /api/items/1
          </remarks>
          <response code="200">Returns deleted item</response>
          <response code="500">If item doesen't exist in database</response>
       */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(int id)
        {
            return Ok(_mapper.Map<Item, ItemDTO>(await _itemRepository.DeleteAsync(id)));
        }
    }

}
