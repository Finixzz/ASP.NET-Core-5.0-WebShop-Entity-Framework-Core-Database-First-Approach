using AutoMapper;
using DAL.Dtos.SubCategoryDTOS;
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
    public class SubCategoriesController : ControllerBase
    {
        private ISubCategorySQLRepository _subCategoryRepository;
        private IMapper _mapper;
        public SubCategoriesController(ISubCategorySQLRepository _subCategoryRepository,
                                       IMapper _mapper)
        {
            this._subCategoryRepository = _subCategoryRepository;
            this._mapper = _mapper;
        }


        /*
            <summary>
                   Returns all subcategories
            </summary>
            <remarks>
            Sample request:

                GET /api/subcategories
           </remarks>
           <response code="200">Returns subcategory info if okay</response>
        */


        [HttpGet]
        public async Task<IActionResult> GetAllSubCategoriesAsync()
        {
            var subCategoryDTOS = _mapper
                .Map<IEnumerable<SubCategory>, IEnumerable<SubCategoryDTO>>
                (await _subCategoryRepository.GetAllAsync());
            return Ok(subCategoryDTOS);
        }



        /*
            <summary>
                   Returns single subcategory
            </summary>
            <remarks>
            Sample request:

                GET /api/subcategories/1

           </remarks>
           <response code="200">Returns subcategory info if found</response>
           <response code="404">If object not found</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryByIdAsync(int id)
        {
            SubCategory scInDb = await _subCategoryRepository.GetByIdAsync(id);

            if (scInDb == null)
                return NotFound();

            return Ok(_mapper.Map<SubCategory, SubCategoryDTO>(scInDb));
        }




        /*
            <summary>
                Creates category from raw JSON
            </summary>
            <remarks>
                Sample request:

                POST /api/subcategory
                {
                    "categoryId":1,
                    "name":"First subcategory"
                }
            </remarks>
            <response code="201">Returns subcategory info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If referential integrity is violated
            eg. If you provide categoryId that is not present in category table</response> 
        */

        [HttpPost]
        public async Task<IActionResult> SaveSubCategoryAsync(SubCategoryDTO subCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SubCategory newSubCategory =
                await _subCategoryRepository
                .SaveAsync(_mapper.Map<SubCategoryDTO, SubCategory>(subCategoryDTO));

            subCategoryDTO.SubCategoryId = newSubCategory.SubCategoryId;

            return CreatedAtAction(nameof(subCategoryDTO), new { id = subCategoryDTO.SubCategoryId }, subCategoryDTO);
        }




        /*
            <summary>
                    Edits existing subcategory data from raw JSON
            </summary>
            <remarks>
                Sample request:

                PUT /api/subcategory/1
                {
                  "categoryId":1,
                  "name":"first category name edit"
                }
            </remarks>
            <response code="200">Returns updated category info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="404">If subcategory doesen't exist in database</response>
            <response code="500">If referential integrity is violated
            eg. If you provide categoryId that is not present in category table</response> 

        */
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSubCategoryAsync(SubCategoryDTO subCategoryDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SubCategory subCategoryInDb = await _subCategoryRepository.GetByIdAsync(id);

            if (subCategoryInDb == null)
                return NotFound();

            subCategoryDTO.SubCategoryId = id;
            _mapper.Map(subCategoryDTO, subCategoryInDb);

            subCategoryDTO = _mapper.Map<SubCategory, SubCategoryDTO>(await _subCategoryRepository.EditAsync(subCategoryInDb, id));

            return Ok(subCategoryDTO);
        }

        /*
           <summary>
                  Deletes existing subcategory in database
           </summary>
           <remarks>
           Sample request:

               DELETE /api/subcategory/1

          </remarks>
          <response code="200">Returns deleted subcategory</response>
          <response code="400">If subcategory doesen't exist in database</response>
       */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoryAsync(int id)
        {
            SubCategory subCategoryInDb = await _subCategoryRepository.GetByIdAsync(id);

            if (subCategoryInDb == null)
                return NotFound();

            return Ok(_mapper.Map<SubCategory, SubCategoryDTO>(await _subCategoryRepository.DeleteAsync(id)));
        }
    }
}
