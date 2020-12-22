using DAL.Helpers;
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
    public class OrderDetailSQLRepository : IOrderDetailSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public OrderDetailSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }

        public async Task<OrderDetail> DeleteAsync(int id)
        {
            OrderDetail orderDetailInDb = await GetByIdAsync(id);
            _appDbContext.OrderDetails.Remove(orderDetailInDb);
            await _appDbContext.SaveChangesAsync();
            return orderDetailInDb;
        }

        public async Task<OrderDetail> EditAsync(OrderDetail orderDetail, int id)
        {
            OrderDetail orderDetailInDb = await GetByIdAsync(id);
            orderDetailInDb.ItemId = orderDetail.ItemId;
            orderDetailInDb.Quantity = orderDetail.Quantity;
            orderDetailInDb.SoldAtPrice = orderDetail.SoldAtPrice;
            orderDetailInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return orderDetailInDb;
        }

        public async Task<OrderDetail> GetByIdAsync(int id)
        {
            return await _appDbContext.OrderDetails.SingleOrDefaultAsync(c => c.OrderDetailsId == id);
        }

        public async Task<IEnumerable<OrderDetail>> GetByOrderHeaderIdAsync(int orderHeaderId)
        {
            return await _appDbContext.OrderDetails.Where(c => c.OrderHeaderId == orderHeaderId).ToListAsync();
        }

        public async Task<OrderDetail> SaveAsync(OrderDetail orderDetail)
        {
            orderDetail.DateAdded = DateTime.Now;
            _appDbContext.OrderDetails.Add(orderDetail);
            await _appDbContext.SaveChangesAsync();
            return orderDetail;
        }
    }
}
