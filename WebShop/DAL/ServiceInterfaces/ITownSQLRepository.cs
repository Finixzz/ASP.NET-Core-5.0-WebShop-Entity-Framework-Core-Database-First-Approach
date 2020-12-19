using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface ITownSQLRepository
    {
        Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId);

        Task<Town> GetByIdAsync(int id);

        Task<Town> SaveAsync(Town town);

        Task<Town> EditAsync(Town town, int id);

        Task<Town> DeleteAsync(int id);


    }
}
