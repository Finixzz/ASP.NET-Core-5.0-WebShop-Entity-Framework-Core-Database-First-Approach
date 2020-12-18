using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface ICountrySQLRepository
    {
        Task<IEnumerable<Country>> GetAllAsync();

        Task<Country> GetByIdAsync(int id);

        Task<Country> SaveAsync(Country country);

        Task<Country> EditAsync(Country country, int id);

        Task<Country> DeleteAsync(int id);
    }
}
