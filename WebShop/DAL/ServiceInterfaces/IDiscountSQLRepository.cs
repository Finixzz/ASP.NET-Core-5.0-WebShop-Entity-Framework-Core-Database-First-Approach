
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IDiscountSQLRepository
    {
        
        Task<IEnumerable<Discount>> GetAllAsync();

        Task<Discount> GetByIdAsync(int id);

        Task<Discount> SaveAsync(Discount discount);

        Task<Discount> EditAsync(Discount discount, int id);

        Task<Discount> DeleteAsync(int id);
        
    }
}
