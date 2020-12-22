using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface IOrderHeaderSQLRepository
    {
        Task<PagedList<OrderHeader>> GetAllAsync(OwnerParameters ownerParameters);

        Task<OrderHeader> GetByIdAsync(int id);

        Task<OrderHeader> SaveAsync(OrderHeader orderHeader);

        Task<OrderHeader> EditAsync(OrderHeader orderHeader,int id);

        Task<OrderHeader> DeleteAsync(int id);
    }
}
