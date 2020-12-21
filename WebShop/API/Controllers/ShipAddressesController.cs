using AutoMapper;
using DAL.Dtos.ShipAddressDTOS;
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
    public class ShipAddressesController : ControllerBase
    {
        private IShipAddressSQLRepository _shipAdressRepository;
        private IMapper _mapper;

        public ShipAddressesController(IShipAddressSQLRepository _shipAdressRepository,
                                      IMapper _mapper)
        {
            this._shipAdressRepository = _shipAdressRepository;
            this._mapper = _mapper;
        }



        /*
           <summary>
                  Returns all user ship addresses based on
                  provided userId query parameter
           </summary>
           <remarks>
           Sample request:
               GET /api/shipaddresses?userId=786f4db4-f655-46b5-8e1e-19bdb8f10069
          </remarks>
          <response code="200">Returns user ship addresses info if okay</response>
          <response code="500">If userId query parameter is not provided</response>
       */
        [HttpGet]
        public async Task<IActionResult> GetShipAddressesByUserIdAsync([FromQuery] string userId)
        {
            if (userId == null)
                return BadRequest();

            var userShipAdressDTOs = _mapper.Map<IEnumerable<ShipAddress>, IEnumerable<ShipAddressDTO>>
                                            (await _shipAdressRepository.GetAllByUserIdAsync(userId));
            return Ok(userShipAdressDTOs);
        }



        /*
            <summary>
                   Returns single ship address
            </summary>
            <remarks>
            Sample request:
                GET /api/shipadresses/1
           </remarks>
           <response code="200">Returns ship address info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipAddressByIdAsync(int id)
        {
            ShipAddress shipAdressInDb = await _shipAdressRepository.GetByIdAsync(id);

            if (shipAdressInDb == null)
                return NotFound();

            return Ok(_mapper.Map<ShipAddress, ShipAddressDTO>(shipAdressInDb));
        }



        /*
             <summary>
                    Creates ship address from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 POST /api/shipaddresses
                 {
                    "userId":"786f4db4-f655-46b5-8e1e-19bdb8f10069",
                    "townId":1,
                    "street":"Street sample name",
                    "streetNumber":22
                 }
            </remarks>
            <response code="201">Returns ship address info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request or if
                        referential integrity is violated eg. if townId or userId value is not present
                        in respective tables
            </response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveShipAddressAsync(ShipAddressDTO shipAddressDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ShipAddress newShipAddress = await _shipAdressRepository
                                         .SaveAsync(_mapper.Map<ShipAddressDTO, ShipAddress>(shipAddressDTO));

            shipAddressDTO.ShipAddressId = newShipAddress.ShipAddressId;

            return CreatedAtAction(nameof(shipAddressDTO), new { id = shipAddressDTO.ShipAddressId }, shipAddressDTO);
        }



        /*
             <summary>
                    Edits existing ship address data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 PUT /api/shipaddresses/1
                 {
                    "shipAddressId":2,
                    "userId":"786f4db4-f655-46b5-8e1e-19bdb8f10069",
                    "townId":3,
                    "street":"Sample street name edit",
                    "streetNumber":22
                  }
            </remarks>
            <response code="200">Returns updated ship address info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            shipAddressId that is provided in json object</response> 
            <response code="404">If ship address doesen't exist in database</response>
            <response code="500">If JSON object is not structured as sample request or if
                        referential integrity is violated eg. if townId or userId value is not present
                        in respective tables
            </response> 
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditShipAddressAsync(ShipAddressDTO shipAddressDTO,int id)
        {
            if (!ModelState.IsValid || (shipAddressDTO.ShipAddressId != id))
                return BadRequest();

            ShipAddress shipAddressInDb = await _shipAdressRepository.GetByIdAsync(id);

            if (shipAddressInDb == null)
                return NotFound();

            _mapper.Map(shipAddressDTO, shipAddressInDb);

            shipAddressDTO =_mapper.Map<ShipAddress,ShipAddressDTO>( await _shipAdressRepository.EditAsync(shipAddressInDb, id));

            return Ok(shipAddressDTO);
        }


        /*
             <summary>
                    Deletes existing ship address from database
             </summary>
             <remarks>
             Sample request:
            
                 DELETE /api/shipaddress/1
                 
            </remarks>
            <response code="200">Returns deleted ship address</response>
            <response code="500">If ship address doesen't exist in database</response>
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipAddress(int id)
        {
            return Ok(_mapper.Map<ShipAddress, ShipAddressDTO>(await _shipAdressRepository.DeleteAsync(id)));
        }
    } 
}
