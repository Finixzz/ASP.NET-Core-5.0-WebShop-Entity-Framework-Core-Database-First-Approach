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
    public class ShipAddressSQLRepository : IShipAddressSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public ShipAddressSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<ShipAddress> DeleteAsync(int id)
        {
            ShipAddress shipAddressInDb = await GetByIdAsync(id);
            _appDbContext.ShipAddresses.Remove(shipAddressInDb);
            await _appDbContext.SaveChangesAsync();
            return shipAddressInDb;
        }

        public async Task<ShipAddress> EditAsync(ShipAddress shipAddress, int id)
        {
            ShipAddress shipAdressInDb = await GetByIdAsync(id);
            shipAdressInDb.TownId = shipAddress.TownId;
            shipAdressInDb.Street = shipAddress.Street;
            shipAdressInDb.StreetNumber = shipAddress.StreetNumber;
            shipAdressInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return shipAdressInDb;
        }

        public async Task<IEnumerable<ShipAddress>> GetAllByUserIdAsync(string userId)
        {
            return await _appDbContext.ShipAddresses.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<ShipAddress> GetByIdAsync(int id)
        {
            return await _appDbContext.ShipAddresses.SingleOrDefaultAsync(c => c.ShipAddressId == id);
        }

        public async Task<ShipAddress> SaveAsync(ShipAddress shipAddress)
        {
            shipAddress.DateAdded = DateTime.Now;
            _appDbContext.ShipAddresses.Add(shipAddress);
            await _appDbContext.SaveChangesAsync();
            return shipAddress;
        }
    }
}
