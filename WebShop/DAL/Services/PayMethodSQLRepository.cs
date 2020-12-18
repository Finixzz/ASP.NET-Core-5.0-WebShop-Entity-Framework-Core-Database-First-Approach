using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class PayMethodSQLRepository : IPayMethodSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public PayMethodSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<IEnumerable<PayMethod>> GetAllAsync()
        {
            return await _appDbContext.PayMethods.ToListAsync();
        }

        public async Task<PayMethod> GetById(int id)
        {
            return await _appDbContext.PayMethods.SingleOrDefaultAsync(c => c.PayMethodId == id);
        }
    }
}
