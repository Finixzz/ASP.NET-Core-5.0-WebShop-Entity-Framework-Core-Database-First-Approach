using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ItemBrandSQLRepository : IItemBrandSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public ItemBrandSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<ItemBrand> DeleteAsync(int id)
        {
            ItemBrand itemBrandInDb = await GetByIdAsync(id);
            _appDbContext.ItemBrands.Remove(itemBrandInDb);
            await _appDbContext.SaveChangesAsync();
            return itemBrandInDb;

        }

        public async Task<ItemBrand> EditAsync(ItemBrand itemBrand, int id)
        {
            ItemBrand itemBrandInDb = await GetByIdAsync(id);
            itemBrandInDb.Name = itemBrand.Name;
            itemBrandInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return itemBrandInDb;
        }

        public async Task<IEnumerable<ItemBrand>> GetAllAsync()
        {
            return await _appDbContext.ItemBrands.ToListAsync();
        }

        public async Task<ItemBrand> GetByIdAsync(int id)
        {
            return await _appDbContext.ItemBrands.SingleOrDefaultAsync(c => c.ItemBrandId == id);
        }

        public async Task<ItemBrand> SaveAsync(ItemBrand itemBrand)
        {
            itemBrand.DateAdded = DateTime.Now;
            _appDbContext.ItemBrands.Add(itemBrand);
            await _appDbContext.SaveChangesAsync();
            return itemBrand;
        }
    }
}
