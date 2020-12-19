using AutoMapper;
using DAL.Dtos.CategoryDTOS;
using DAL.Dtos.CountryDTOS;
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
    public class CountriesController : ControllerBase
    {
        private ICountrySQLRepository _countryRepository;
        private IMapper _mapper;

        public CountriesController(ICountrySQLRepository _countryRepository,
                                   IMapper _mapper)
        {
            this._countryRepository = _countryRepository;
            this._mapper = _mapper;
        }



        /*
            <summary>
                   Returns all countries
            </summary>
            <remarks>
            Sample request:
                GET /api/countries
           </remarks>
           <response code="200">Returns country info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetAllCountriesAsync()
        {
            var countryDTOs = _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>
                (await _countryRepository.GetAllAsync());
            return Ok(countryDTOs);
        }



        /*
            <summary>
                   Returns single country
            </summary>
            <remarks>
            Sample request:
                GET /api/countries/1
           </remarks>
           <response code="200">Returns country info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryByIdAsync(int id)
        {
            Country countryInDb = await _countryRepository.GetByIdAsync(id);

            if (countryInDb == null)
                return NotFound();

            return Ok(_mapper.Map<Country, CountryDTO>(countryInDb));
        }


        /*
            <summary>
                   Creates country from raw JSON
            </summary>
            <remarks>
                Sample request:

                POST /api/countries
                {
                   "name": "Hungary",
                }
           </remarks>
           <response code="201">Returns counrty info if okay</response>
           <response code="400">If model state is not valid</response> 
           <response code="500">If JSON object is not structured as sample request</response> 
        */
        [HttpPost]
        public async Task<IActionResult> SaveCountryAsync(CountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Country newCountry = await _countryRepository.SaveAsync(_mapper.Map<CountryDTO,Country>(countryDTO));

            countryDTO.CountryId = newCountry.CountryId;

            return CreatedAtAction(nameof(countryDTO), new { id = countryDTO.CountryId }, countryDTO);

        }




        /*
             <summary>
                    Edits existing country data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 PUT /api/countries/1
                 {
                    "countryId":1,
                    "name": "Hungary edit",
                  }
            </remarks>
            <response code="200">Returns updated country info if okay</response>
            <response code="400">If model state is not valid or supplied URI id doesen't match 
            countryId that is provided in json object</response> 
            <response code="404">If country doesen't exist in database</response>
            
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCountryAsync(CountryDTO countryDTO,int id)
        {
            if (!ModelState.IsValid || (countryDTO.CountryId != id))
                return BadRequest();

            Country countryInDb = await _countryRepository.GetByIdAsync(id);

            if (countryInDb == null)
                return NotFound();

            _mapper.Map(countryDTO, countryInDb);

            countryDTO = _mapper.Map<Country, CountryDTO>(await _countryRepository.EditAsync(countryInDb, id));

            return Ok(countryDTO);
        }


        /*
             <summary>
                    Deletes existing country from database
             </summary>
             <remarks>
             Sample request:
            
                 DELETE /api/countries/1
                 
            </remarks>
            <response code="200">Returns deleted country</response>
            <response code="500">If country doesen't exist in database or country we want to delete is referenced 
                by ShipCost or Town table (ON DELETE NO ACTION)      
            </response>
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryAsync(int id)
        {
            return Ok(_mapper.Map<Country, CountryDTO>(await _countryRepository.DeleteAsync(id)));
        }
    }
}
