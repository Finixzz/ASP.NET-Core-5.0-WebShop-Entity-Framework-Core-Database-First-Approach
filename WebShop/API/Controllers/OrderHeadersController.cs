using AutoMapper;
using DAL.Dtos.OrderHeaderDTOS;
using DAL.Helpers;
using DAL.Models;
using DAL.Services;
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
    public class OrderHeadersController : ControllerBase
    {

        private IOrderHeaderSQLRepository _orderHeaderRepository;
        private IMapper _mapper;


        public OrderHeadersController(IOrderHeaderSQLRepository _orderHeaderRepository,
                                     IMapper _mapper)
        {
            this._orderHeaderRepository = _orderHeaderRepository;
            this._mapper = _mapper;
        }


        /*
            <summary>
                   Returns order headers based on provided query parametes,
                   metadata is provided in headers section
            </summary>
            <remarks>
            Sample request:
                GET /api/orderheaders?pageNumber=1&pageSize=25
           </remarks>
           <response code="200">Returns order headers info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetOrderHeadersByOwnerParametersAsync([FromQuery] OwnerParameters ownersParameters)
        {
            var orderHeaders = await _orderHeaderRepository.GetAllAsync(ownersParameters);

            var metadata = new
            {
                orderHeaders.TotalCount,
                orderHeaders.PageSize,
                orderHeaders.CurrentPage,
                orderHeaders.HasNext,
                orderHeaders.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var orderHeaderDTOs = _mapper.Map<IEnumerable<OrderHeader>, IEnumerable<OrderHeaderDTO>>(orderHeaders);
            return Ok(orderHeaderDTOs);
        }



        /*
            <summary>
                   Returns single order header
            </summary>
            <remarks>
            Sample request:
                GET /api/orderheaders/1
           </remarks>
           <response code="200">Returns order headers info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderHeaderByIdAsync(int id)
        {
            OrderHeader orderHeaderInDb = await _orderHeaderRepository.GetByIdAsync(id);

            if (orderHeaderInDb == null)
                return NotFound();

            return Ok(_mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeaderInDb));
        }


        /*
            <summary>
                Creates order header from raw JSON
            </summary>
            <remarks>
                Sample request:
            
                POST /api/orderheaders
                {
                    "payMethodId": 1,
	                "shipAddressId":1
                }
            </remarks>
            <response code="201">Returns order header info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request
            or if referential integrity is violated eg. if supplied payMethodId or shipAddressId 
            doesen't exist in respective tables</response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveOrderHeaderAsync(AddOrderHeaderDTO addOrderHeaderDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            OrderHeader newOrderHeader = await _orderHeaderRepository.SaveAsync(_mapper.Map<AddOrderHeaderDTO, OrderHeader>(addOrderHeaderDTO));

            OrderHeaderDTO orderHeaderDTO = _mapper.Map<OrderHeader, OrderHeaderDTO>(newOrderHeader);

            return CreatedAtAction(nameof(orderHeaderDTO), new { id = orderHeaderDTO.OrderHeaderId }, orderHeaderDTO);

        }


        /*
             <summary>
                    Edits existing order header data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                    POST /api/orderheaders/1
                    {
                        "orderHeaderId": 1,
                        "payMethodId": 1,
                        "shipAddressId": 2,
                        "orderDate": "2020-12-22T10:35:02.217",
                        "shippedDate": null,
                        "isShipped": 0,
                        "isPayed": 1
                    }
            </remarks>
            <response code="200">Returns updated order header info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            orderHeaderId that is provided in json object</response> 
            <response code="404">If item doesen't exist in database</response>
            <response code="500">If JSON object is not structured as sample request
            or if referential integrity is violated eg. if supplied payMethodId or shipAddressId 
            doesen't exist in respective tables</response> 
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrderHeaderAsync(EditOrderHeaderDTO editOrderHeaderDTO, int id)
        {
            if (!ModelState.IsValid || (editOrderHeaderDTO.OrderHeaderId != id))
                return BadRequest();

            OrderHeader orderHeaderInDb = await _orderHeaderRepository.GetByIdAsync(id);

            if (orderHeaderInDb == null)
                return NotFound();

            _mapper.Map(editOrderHeaderDTO, orderHeaderInDb);

            OrderHeaderDTO orderHeaderDTO = _mapper.Map<OrderHeader,OrderHeaderDTO>(await _orderHeaderRepository.EditAsync(orderHeaderInDb, id));

            return Ok(orderHeaderDTO);
        }


        /*
        <summary>
               Deletes existing order header from database
        </summary>
        <remarks>
        Sample request:
            DELETE /api/items/1
       </remarks>
       <response code="200">Returns deleted order header</response>
       <response code="500">If item doesen't exist in database or if referential integrity
                        is violated eg. if orderHeaderId is referenced in orderDetails table
                        (ON DELETE NO ACTION)
        </response>
    */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderHeaderAsync(int id)
        {
            return Ok(_mapper.Map<OrderHeader, OrderHeaderDTO>(await _orderHeaderRepository.DeleteAsync(id)));
        }
    }
}
