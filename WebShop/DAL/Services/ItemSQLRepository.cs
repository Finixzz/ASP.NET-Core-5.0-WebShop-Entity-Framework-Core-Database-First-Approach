using DAL.Helpers;
using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ItemSQLRepository : IItemSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public ItemSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<Item> DeleteAsync(int id)
        {
            Item itemInDb = await GetByIdAsync(id);
            _appDbContext.Items.Remove(itemInDb);
            await _appDbContext.SaveChangesAsync();
            return itemInDb;
        }

        public async Task<Item> EditAsync(Item item, int id)
        {
            Item itemInDb = await GetByIdAsync(id);
            itemInDb.BrandId = item.BrandId;
            itemInDb.SubCategoryId = item.SubCategoryId;
            itemInDb.Name = item.Name;
            itemInDb.Description = item.Description;
            itemInDb.UnitsInStock = item.UnitsInStock;
            itemInDb.UnitPrice = item.UnitPrice;
            itemInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return itemInDb;

        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _appDbContext.Items.SingleOrDefaultAsync(c => c.ItemId == id);
        }

        public async Task<PagedList<Item>> GetItemsAsync(OwnerParameters ownerParameters)
            
        {
            return PagedList<Item>.ToPagedList( await _appDbContext.Items.ToListAsync(), ownerParameters.PageNumber, ownerParameters.PageSize);
        }

        public  async Task<Item> SaveAsync(Item item)
        {
            item.DateAdded = DateTime.Now;
            _appDbContext.Items.Add(item);
            await _appDbContext.SaveChangesAsync();
            return item;
        }
    }
}
