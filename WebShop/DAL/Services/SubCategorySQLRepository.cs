using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class SubCategorySQLRepository : ISubCategorySQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public SubCategorySQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<SubCategory> DeleteAsync(int id)
        {
            SubCategory scInDb = await GetByIdAsync(id);
            _appDbContext.SubCategories.Remove(scInDb);
            await _appDbContext.SaveChangesAsync();
            return scInDb;
        }

        public async Task<SubCategory> EditAsync(SubCategory subCategory, int id)
        {
            SubCategory scInDb = await GetByIdAsync(id);
            scInDb.Name = subCategory.Name;
            scInDb.CategoryId = subCategory.CategoryId;
            await _appDbContext.SaveChangesAsync();
            return scInDb;

        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _appDbContext.SubCategories.ToListAsync();
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            return await _appDbContext.SubCategories.SingleOrDefaultAsync(c => c.SubCategoryId == id);
        }

        public async Task<SubCategory> SaveAsync(SubCategory subCategory)
        {
            subCategory.DateAdded = DateTime.Now;
            _appDbContext.SubCategories.Add(subCategory);
            await _appDbContext.SaveChangesAsync();
            return subCategory;
        }
    }
}
