using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface ICategorySQLRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int id);
        Task<Category> SaveAsync(Category category);
        Task<Category> EditAsync(Category category, int id);
        Task<Category> DeleteAsync(int id);
    }
}
