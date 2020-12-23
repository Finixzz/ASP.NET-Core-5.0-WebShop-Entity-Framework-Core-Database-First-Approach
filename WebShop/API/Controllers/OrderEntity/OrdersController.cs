using DAL.ModelHelpers;
using DAL.ServiceFacadeInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private IOrderFacade _orderFacade;

        public OrdersController(IOrderFacade _orderFacade)
        {
            this._orderFacade = _orderFacade;
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrderAsync(Order newOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _orderFacade.SaveAsync(newOrder));
        }
    }
}
