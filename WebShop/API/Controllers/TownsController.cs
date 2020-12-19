using AutoMapper;
using DAL.Dtos.TownDTOS;
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
    public class TownsController : ControllerBase
    {
        private ITownSQLRepository _townRepository;
        private IMapper _mapper;

        public TownsController(ITownSQLRepository _townRepository, IMapper _mapper)
        {
            this._townRepository = _townRepository;
            this._mapper = _mapper;
        }


        /*
            <summary>
                   Returns all towns based on provided counrtyId
                   from query parameters
            </summary>
            <remarks>
            Sample request:
                GET /api/towns?countryId=1
           </remarks>
           <response code="200">Returns towns info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetAllTownsByCountryId([FromQuery]int countryId)
        {
            var townDTOs = _mapper.Map<IEnumerable<Town>, IEnumerable<TownDTO>>
                                    (await _townRepository.GetAllByCountryIdAsync(countryId));
            return Ok(townDTOs);
        }



        /*
            <summary>
                   Returns single town
            </summary>
            <remarks>
            Sample request:
                GET /api/towns/1
           </remarks>
           <response code="200">Returns town info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTownByIdAsync(int id)
        {
            Town townInDb = await _townRepository.GetByIdAsync(id);

            if (townInDb == null)
                return NotFound();

            return Ok(_mapper.Map<Town, TownDTO>(townInDb));
        }



        /*
             <summary>
                    Creates town from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 POST /api/towns
                 {
                    "countryId": 1,
                    "name": "Town sample"
                 }
            </remarks>
            <response code="201">Returns town info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request or
                     if referential integrity is violated eg. if you provide counrtyId that
                     doesent exist in respective rable
            </response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveTownAsync(TownDTO townDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Town newTown = await _townRepository.SaveAsync(_mapper.Map<TownDTO, Town>(townDTO));

            townDTO.TownId = newTown.TownId;

            return CreatedAtAction(nameof(townDTO), new { id = townDTO.TownId }, townDTO);

        }




        /*
             <summary>
                    Edits existing town data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 PUT /api/categories/1
                 {
                    "townId":1,
                    "countryId": 1,
                    "name": "Town sample edit"
                  }
            </remarks>
            <response code="200">Returns updated town info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            townId that is provided in json object</response> 
            <response code="404">If town doesen't exist in database</response>
            <response code="500">If JSON object is not structured as sample request or
                     if referential integrity is violated eg. if you provide counrtyId that
                     doesent exist in respective rable
            </response> 
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTownAsync(TownDTO townDTO,int id)
        {
            if (!ModelState.IsValid || (townDTO.TownId != id))
                return BadRequest();

            Town townInDb = await _townRepository.GetByIdAsync(id);

            if (townInDb == null)
                return NotFound();

            _mapper.Map(townDTO, townInDb);

            townDTO = _mapper.Map<Town, TownDTO>(await _townRepository.EditAsync(townInDb, id));

            return Ok(townDTO);
        }


        /*
            <summary>
                   Deletes existing town from database
            </summary>
            <remarks>
            Sample request:

                DELETE /api/towns/1

           </remarks>
           <response code="200">Returns deleted town</response>
           <response code="500">If town doesen't exist in database or town we want to delete is referenced 
               by ShipAddress table (ON DELETE NO ACTION)      
           </response>
        */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTownAsync(int id)
        {
            return Ok(_mapper.Map<Town, TownDTO>(await _townRepository.DeleteAsync(id)));
        }
    }
}
