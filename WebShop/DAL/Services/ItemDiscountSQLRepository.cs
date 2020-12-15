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
    public class ItemDiscountSQLRepository : IItemDiscountsSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public ItemDiscountSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<ItemDiscount> DeleteAsync(int id)
        {
            ItemDiscount itemDiscountInDb = await GetByIdAsync(id);
            _appDbContext.ItemDiscounts.Remove(itemDiscountInDb);
            await _appDbContext.SaveChangesAsync();
            return itemDiscountInDb;
        }

        public async Task<ItemDiscount> EditAsync(ItemDiscount itemDiscount, int id)
        {
            ItemDiscount itemDiscountInDb = await GetByIdAsync(id);
            itemDiscountInDb.ItemId = itemDiscount.ItemId;
            itemDiscountInDb.DiscountId = itemDiscount.DiscountId;
            itemDiscountInDb.StartDate = itemDiscount.StartDate;
            itemDiscountInDb.EndDate = itemDiscount.EndDate;
            itemDiscountInDb.IsActive = itemDiscount.IsActive;
            itemDiscountInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return itemDiscountInDb;
        }

        public async Task<ItemDiscount> GetByIdAsync(int id)
        {
            return await _appDbContext.ItemDiscounts.SingleOrDefaultAsync(c => c.ItemDiscountId == id);
        }

        public async Task<PagedList<ItemDiscount>> GetDiscountedItemsAsync(OwnerParameters ownerParameters)
        {
            return PagedList<ItemDiscount>.ToPagedList(await _appDbContext.ItemDiscounts.ToListAsync(), ownerParameters.PageNumber, ownerParameters.PageSize);

        }


        public async Task<ItemDiscount> SaveAsync(ItemDiscount itemDiscount)
        {
            itemDiscount.DateAdded = DateTime.Now;
            _appDbContext.ItemDiscounts.Add(itemDiscount);
            await _appDbContext.SaveChangesAsync();
            return itemDiscount;
        }
    }
}
