using AutoMapper;
using DAL.Dtos.OrderDetailDTOS;
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
    public class OrderDetailsController : ControllerBase
    {
        private IOrderDetailSQLRepository _orderDetailRepository;
        private IMapper _mapper;

        public OrderDetailsController(IOrderDetailSQLRepository _orderDetailRepository,
                                      IMapper _mapper)
        {
            this._orderDetailRepository = _orderDetailRepository;
            this._mapper = _mapper;
        }


        /*
           <summary>
                  Returns order details based on provided orderHeaderId
                  query parameter
           </summary>
           <remarks>
           Sample request:
               GET /api/orderdetails?orderHeaderId=1
          </remarks>
          <response code="200">Returns order details info if okay</response>
       */
        [HttpGet]
        public async Task<IActionResult> GetOrderDetailsByOrderHeaderIdAsync([FromQuery] int orderHeaderId)
        {
            if (orderHeaderId == 0)
                return BadRequest();

            var orderDetailDTOs = _mapper.Map<IEnumerable<OrderDetail>,IEnumerable<OrderDetailDTO>>(await _orderDetailRepository.GetByOrderHeaderIdAsync(orderHeaderId));
            return Ok(orderDetailDTOs);
        }


        /*
            <summary>
                   Returns single order detail
            </summary>
            <remarks>
            Sample request:
                GET /api/orderdetails/1
           </remarks>
           <response code="200">Returns order detail info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailByIdAsync(int id)
        {
            OrderDetail orderDetailInDb = await _orderDetailRepository.GetByIdAsync(id);

            if (orderDetailInDb == null)
                return NotFound();

            return Ok(_mapper.Map<OrderDetail, OrderDetailDTO>(orderDetailInDb));
        }



        /*
            <summary>
                Creates single order detail from raw JSON
            </summary>
            <remarks>
                Sample request:
            
                POST /api/orderdetails
                {
                    "orderHeaderId":1,
	                "itemId":4,
	                "quantity":2,
	                "soldAtPrice":5
                }
            </remarks>
            <response code="201">Returns order detail info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request
            or if referential integrity is violated eg. if supplied orderHeaderId or itemId 
            doesen't exist in respective tables</response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveOrderDetailAsync(OrderDetailDTO orderDetailDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            OrderDetail newOrderDetail = await _orderDetailRepository.SaveAsync(_mapper.Map<OrderDetailDTO, OrderDetail>(orderDetailDTO));

            orderDetailDTO.OrderDetailsId = newOrderDetail.OrderDetailsId;

            return CreatedAtAction(nameof(orderDetailDTO), new { id = orderDetailDTO.OrderDetailsId }, orderDetailDTO);

        }


        /*
             <summary>
                    Edits existing order detail data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                    POST /api/orderheaders/1
                    {
                        "orderDetailsId":1,
	                    "orderHeaderId":1,
	                    "itemId":4,
	                    "quantity":2,
	                    "soldAtPrice":5
                    }
            </remarks>
            <response code="200">Returns updated order detail info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            orderDetailsId that is provided in json object</response> 
            <response code="404">If order detail doesen't exist in database</response>
            <response code="500">If JSON object is not structured as sample request
            or if referential integrity is violated eg. if supplied orderHeaderId or itemId 
            doesen't exist in respective tables</response> 
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrderDetailAsync(OrderDetailDTO orderDetailDTO,int id)
        {
            if (!ModelState.IsValid || (orderDetailDTO.OrderDetailsId != id))
                return BadRequest();

            OrderDetail orderDetailInDb = await _orderDetailRepository.GetByIdAsync(id);

            if (orderDetailInDb == null)
                return NotFound();

            _mapper.Map(orderDetailDTO, orderDetailInDb);

            orderDetailDTO = _mapper.Map<OrderDetail, OrderDetailDTO>(await _orderDetailRepository.EditAsync(orderDetailInDb, id));

            return Ok(orderDetailDTO);
        }


        /*
            <summary>
                   Deletes existing order detail from database
            </summary>
            <remarks>
            Sample request:
                DELETE /api/orderdetails/1
           </remarks>
           <response code="200">Returns single deleted order detail</response>
           <response code="500">If order detail doesen't exist in database</response>
        */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetailAsync(int id)
        {
            return Ok(_mapper.Map<OrderDetail, OrderDetailDTO>(await _orderDetailRepository.DeleteAsync(id)));
        }
    }
}
