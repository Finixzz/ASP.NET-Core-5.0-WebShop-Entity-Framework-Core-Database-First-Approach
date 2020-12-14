
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
    public class DiscountSQLRepository : IDiscountSQLRepository
    {
        
        private WebShopSampleContext _appDbContext;

        public DiscountSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<Discount> DeleteAsync(int id)
        {
            Discount discountInDb = await GetByIdAsync(id);
            _appDbContext.Discounts.Remove(discountInDb);
            await _appDbContext.SaveChangesAsync();
            return discountInDb;
        }

        public async Task<Discount> EditAsync(Discount discount, int id)
        {
            Discount discountInDb = await GetByIdAsync(id);
            discountInDb.DiscountRate = discount.DiscountRate;
            discountInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return discountInDb;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _appDbContext.Discounts.ToListAsync();
        }

        public async Task<Discount> GetByIdAsync(int id)
        {
            return await  _appDbContext.Discounts.SingleOrDefaultAsync(c => c.DiscountId == id);
        }

        public async Task<Discount> SaveAsync(Discount discount)
        {
            discount.DateAdded = DateTime.Now;
            _appDbContext.Discounts.Add(discount);
            await _appDbContext.SaveChangesAsync();
            return discount;
        }
        
    }
}
