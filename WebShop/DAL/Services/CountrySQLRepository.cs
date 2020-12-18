using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class CountrySQLRepository : ICountrySQLRepository
    {

        private WebShopSampleContext _appDbContext;

        public CountrySQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<Country> DeleteAsync(int id)
        {
            Country countryInDb = await GetByIdAsync(id);
            _appDbContext.Countries.Remove(countryInDb);
            await _appDbContext.SaveChangesAsync();
            return countryInDb;
        }

        public async Task<Country> EditAsync(Country country, int id)
        {
            Country countryInDb = await GetByIdAsync(id);
            countryInDb.Name = country.Name;
            countryInDb.ShipCosts = country.ShipCosts;
            countryInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return countryInDb;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _appDbContext.Countries.ToListAsync();
        }

        public  async Task<Country> GetByIdAsync(int id)
        {
            return await _appDbContext.Countries.SingleOrDefaultAsync(c => c.CountryId == id);
        }

        public async Task<Country> SaveAsync(Country country)
        {
            country.DateAdded = DateTime.Now;
            _appDbContext.Countries.Add(country);
            await _appDbContext.SaveChangesAsync();
            return country;
        }
    }
}
