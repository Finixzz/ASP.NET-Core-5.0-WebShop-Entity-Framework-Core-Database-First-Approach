using AutoMapper;
using DAL.Dtos.ShipCostDTOS;
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
    public class ShipCosts : ControllerBase
    {
        private IShipCostSQLRepository _shipCostRepository;
        private IMapper _mapper;

        public ShipCosts(IShipCostSQLRepository _shipCostRepository,
                        IMapper _mapper)
        {
            this._shipCostRepository = _shipCostRepository;
            this._mapper = _mapper;
        }


        /*
          <summary>
                 Returns all ship costs
          </summary>
          <remarks>
          Sample request:
              GET /api/shipcosts
         </remarks>
         <response code="200">Returns shipcost info if okay</response>
      */
        [HttpGet]
        public async Task<IActionResult> GetAllShipCostsAsync()
        {
            var shipCostDTOs = _mapper.Map<IEnumerable<ShipCost>, IEnumerable<ShipCostDTO>>
                                (await _shipCostRepository.GetAllAsync());
            return Ok(shipCostDTOs);
        }



        /*
           <summary>
                  Returns single shipcost info
           </summary>
           <remarks>
           Sample request:
               GET /api/shipcosts/1
          </remarks>
          <response code="200">Returns shipcost info if found</response>
          <response code="404">If something goes wrong</response> 
       */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipCostByIdAsync(int id)
        {
            ShipCost shipCostInDb = await _shipCostRepository.GetByIdAsync(id);

            if (shipCostInDb == null)
                return BadRequest();

            return Ok(_mapper.Map<ShipCost,ShipCostDTO>(shipCostInDb));
        }


        /*
             <summary>
                    Creates shipcost from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 POST /api/shipcosts
                 {
                    "countryId": 1,
                    "shipCost1": 12.55,
                 }
            </remarks>
            <response code="201">Returns shipcost info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request</response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveShipCostAsync(ShipCostDTO shipCostDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ShipCost newShipCost = await _shipCostRepository.SaveAsync(_mapper.Map<ShipCostDTO, ShipCost>(shipCostDTO));

            shipCostDTO.ShipCostId = newShipCost.ShipCostId;
            return CreatedAtAction(nameof(shipCostDTO), new { id = shipCostDTO.ShipCostId }, shipCostDTO);

        }


        /*
             <summary>
                    Edits existing shipcost data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 PUT /api/shipcosts/1
                 {
                    "shipCostId":4,
                    "countryId": 1,
                    "shipCost1": 10.37
                  }
            </remarks>
            <response code="200">Returns updated shipcost info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            shipCostId that is provided in json object</response> 
            <response code="404">If shipcost doesen't exist in database</response>
            <response code="500">If referential integrity is violated
            eg.if countryId doesen't exist in database</response>
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditShipCostAsync(ShipCostDTO shipCostDTO,int id)
        {
            if (!ModelState.IsValid || (shipCostDTO.ShipCostId != id))
                return BadRequest();

            ShipCost shipCostInDb = await _shipCostRepository.GetByIdAsync(id);

            if (shipCostInDb == null)
                return NotFound();

            _mapper.Map(shipCostDTO, shipCostInDb);

            shipCostDTO = _mapper.Map<ShipCost, ShipCostDTO>(await _shipCostRepository.EditAsync(shipCostInDb, id));

            return Ok(shipCostDTO);
        }


        /*
             <summary>
                    Deletes existing shipcost from database
             </summary>
             <remarks>
             Sample request:
            
                 DELETE /api/shipcosts/1
                 
            </remarks>
            <response code="200">Returns deleted shipcost</response>
            <response code="500">If shipcost doesen't exist in database(ON DELETE NO ACTION)</response>
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipCostAsync(int id)
        {
            return Ok(_mapper.Map<ShipCost,ShipCostDTO>(await _shipCostRepository.DeleteAsync(id)));
        }
    }
}
