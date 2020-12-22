using DAL.Helpers;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    enum PayMethod
    {
        CreditCard=1,
        OnArrival
    }
    public class OrderHeaderSQLRepository : IOrderHeaderSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public OrderHeaderSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<OrderHeader> DeleteAsync(int id)
        {
            OrderHeader orderHeaderInDb = await GetByIdAsync(id);
            _appDbContext.OrderHeaders.Remove(orderHeaderInDb);
            await _appDbContext.SaveChangesAsync();
            return orderHeaderInDb;
        }


        public async Task<OrderHeader> EditAsync(OrderHeader orderHeader, int id)
        {
            OrderHeader orderHeaderInDb = await GetByIdAsync(id);
            orderHeaderInDb.PayMethodId = orderHeader.PayMethodId;
            orderHeaderInDb.ShipAddressId = orderHeader.ShipAddressId;
            orderHeaderInDb.ShippedDate = orderHeader.ShippedDate;
            orderHeaderInDb.IsShipped = orderHeader.IsShipped;
            orderHeaderInDb.IsPayed = orderHeader.IsPayed;
            orderHeaderInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return orderHeaderInDb;
        }

        public async Task<PagedList<OrderHeader>> GetAllAsync(OwnerParameters ownerParameters)
        {
            return PagedList<OrderHeader>.ToPagedList(await _appDbContext.OrderHeaders.ToListAsync(), ownerParameters.PageNumber, ownerParameters.PageSize);
        }

        public async Task<OrderHeader> GetByIdAsync(int id)
        {
            return await _appDbContext.OrderHeaders.SingleOrDefaultAsync(c => c.OrderHeaderId == id);
        }

        public async Task<OrderHeader> SaveAsync(OrderHeader orderHeader)
        {
            DateTime currentDateTime = DateTime.Now;
            orderHeader.DateAdded = currentDateTime;
            orderHeader.OrderDate = currentDateTime;
            orderHeader.IsShipped = 0;
            
            if (orderHeader.PayMethodId == (int)PayMethod.CreditCard)
                orderHeader.IsPayed = 1;
            else
                orderHeader.IsPayed = 0;

            _appDbContext.OrderHeaders.Add(orderHeader);
            await _appDbContext.SaveChangesAsync();
            return orderHeader;
        }
    }
}
