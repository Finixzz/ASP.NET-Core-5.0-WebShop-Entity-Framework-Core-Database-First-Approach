using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Services;
using DAL.ServiceInterfaces;
using System.Threading.Tasks;
using DAL.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategorySQLRepository _categoryRepository;

        public CategoriesController(ICategorySQLRepository _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            return Ok(await _categoryRepository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Category newCategory = await _categoryRepository.SaveAsync(category);

            return CreatedAtAction(nameof(newCategory), new { id = newCategory.CategoryId }, newCategory);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            Category categoryInDb = await _categoryRepository.GetByIdAsync(id);

            if (categoryInDb == null)
                return NotFound();

            return Ok(categoryInDb);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(Category category, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Category catInDb = await _categoryRepository.GetByIdAsync(id);

            if (catInDb == null)
                return NotFound();

            await _categoryRepository.EditAsync(category, id);

            return Ok(category);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            Category catInDb = await _categoryRepository.GetByIdAsync(id);
            if (catInDb == null)
                return NotFound();

            catInDb = await _categoryRepository.DeleteAsync(id);
            return Ok(catInDb);
        }
    }
}
