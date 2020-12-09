using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface ISubCategorySQLRepository
    {
        Task<IEnumerable<SubCategory>> GetAllAsync();
        Task<SubCategory> GetByIdAsync(int id);

        Task<SubCategory> SaveAsync(SubCategory subCategory);

        Task<SubCategory> EditAsync(SubCategory subCategory, int id);

        Task<SubCategory> DeleteAsync(int id);
    }
}
