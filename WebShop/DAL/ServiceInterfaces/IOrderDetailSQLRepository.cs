using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IOrderDetailSQLRepository
    {
        Task<IEnumerable<OrderDetail>> GetByOrderHeaderIdAsync(int orderHeaderId);

        Task<OrderDetail> GetByIdAsync(int id);

        Task<OrderDetail> SaveAsync(OrderDetail orderDetail);

        Task<OrderDetail> EditAsync(OrderDetail orderDetail, int id);

        Task<OrderDetail> DeleteAsync(int id);

    }
}
