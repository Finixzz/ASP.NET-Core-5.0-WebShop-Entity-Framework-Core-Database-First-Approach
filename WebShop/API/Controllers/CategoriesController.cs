using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Services;
using DAL.ServiceInterfaces;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

using DAL.Dtos.CategoryDTOS;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategorySQLRepository _categoryRepository;
        private IMapper _mapper;


        public CategoriesController(ICategorySQLRepository _categoryRepository,
                                    IMapper _mapper)
        {
            this._categoryRepository = _categoryRepository;
            this._mapper = _mapper;
        }

        /*
            <summary>
                   Returns all categories
            </summary>
            <remarks>
            Sample request:

                GET /api/categories
           </remarks>
           <response code="200">Returns category info if okay</response>
        */
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categoryDTOs = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(await _categoryRepository.GetAllAsync());
            return Ok(categoryDTOs);
        }


        /*
            <summary>
                   Returns single product
            </summary>
            <remarks>
            Sample request:

                GET /api/categories/1

           </remarks>
           <response code="200">Returns category info if found</response>
           <response code="404">If something goes wrong</response> 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            Category categoryInDb = await _categoryRepository.GetByIdAsync(id);

            if (categoryInDb == null)
                return NotFound();

            return Ok(_mapper.Map<Category, CategoryDTO>(categoryInDb));
        }



        /*
             <summary>
                    Creates category from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 POST /api/category
                 {
                    "name": "First category",
                 }
            </remarks>
            <response code="201">Returns category info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="500">If JSON object is not structured as sample request</response> 
         */
        [HttpPost]
        public async Task<IActionResult> SaveCategoryAsync(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Category newCategory = await _categoryRepository.SaveAsync(_mapper.Map<CategoryDTO, Category>(categoryDTO));

            categoryDTO.CategoryId = newCategory.CategoryId;

            return CreatedAtAction(nameof(categoryDTO), new { id = categoryDTO.CategoryId }, categoryDTO);
        }


        /*
             <summary>
                    Edits existing category data from raw JSON
             </summary>
             <remarks>
                 Sample request:
            
                 PUT /api/category/1
                 {
                    "name": "First category name edit",
                  }
            </remarks>
            <response code="200">Returns updated category info if okay</response>
            <response code="400">If model state is not valid</response> 
            <response code="404">If category doesen't exist in database</response>
            
         */

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategoryAsync(CategoryDTO categoryDTO, int id)
        {
           
            if (!ModelState.IsValid)
                return BadRequest();

            Category categoryInDb = await _categoryRepository.GetByIdAsync(id);

            if (categoryInDb == null)
                return NotFound();

            categoryDTO.CategoryId=id;
            _mapper.Map(categoryDTO, categoryInDb);

            categoryDTO=_mapper.Map<Category,CategoryDTO>(await _categoryRepository.EditAsync(categoryInDb, id));

            return Ok(categoryDTO);
        }


        /*
             <summary>
                    Deletes existing category in database
             </summary>
             <remarks>
             Sample request:
            
                 DELETE /api/category/1
                 
            </remarks>
            <response code="200">Returns deleted category</response>
            <response code="400">If category doesen't exist in database</response>
            <response code="500">If category we want to delete is referenced 
                by another subcategory (ON DELETE NO ACTION)      
            </response>
         */

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            Category categoryInDb = await _categoryRepository.GetByIdAsync(id);
            if (categoryInDb == null)
                return NotFound();

            await _categoryRepository.DeleteAsync(id);

            return Ok(_mapper.Map<Category,CategoryDTO>(categoryInDb));
        }
    }
}
