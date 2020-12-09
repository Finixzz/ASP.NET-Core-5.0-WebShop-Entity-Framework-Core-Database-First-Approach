using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class CategorySQLRepository : ICategorySQLRepository
    {
        private readonly WebShopSampleContext _appDbContext;

        public CategorySQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            Category catInDb = await _appDbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
            _appDbContext.Categories.Remove(catInDb);
            await _appDbContext.SaveChangesAsync();
            return catInDb;

        }

        public async Task<Category> EditAsync(Category category, int id)
        {
            Category catInDb = await GetByIdAsync(id);
            catInDb.Name = category.Name;
            catInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return catInDb;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _appDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _appDbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);

        }

        public async Task<Category> SaveAsync(Category category)
        {
            category.DateAdded = DateTime.Now;
            _appDbContext.Categories.Add(category);
            await _appDbContext.SaveChangesAsync();
            return category;
        }
    }
}
