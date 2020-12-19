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
    public class TownSQLRepository : ITownSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public TownSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<Town> DeleteAsync(int id)
        {
            Town townInDb = await GetByIdAsync(id);
            _appDbContext.Towns.Remove(townInDb);
            await  _appDbContext.SaveChangesAsync();
            return townInDb;
        }

        public async Task<Town> EditAsync(Town town, int id)
        {
            Town townInDb = await GetByIdAsync(id);
            townInDb.CountryId = town.CountryId;
            townInDb.Name = town.Name;
            townInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return townInDb;
        }

        public async Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId)
        {
            return await _appDbContext.Towns.Where(c => c.CountryId == countryId).ToListAsync();
        }

        public async Task<Town> GetByIdAsync(int id)
        {
            return await _appDbContext.Towns.SingleOrDefaultAsync(c => c.TownId == id);
        }

        public async Task<Town> SaveAsync(Town town)
        {
            town.DateAdded = DateTime.Now;
            _appDbContext.Towns.Add(town);
            await _appDbContext.SaveChangesAsync();
            return town;
        }
    }
}
