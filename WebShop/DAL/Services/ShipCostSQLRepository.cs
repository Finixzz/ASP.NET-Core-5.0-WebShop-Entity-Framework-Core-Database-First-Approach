using DAL.Models;
using DAL.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ShipCostSQLRepository : IShipCostSQLRepository
    {
        private WebShopSampleContext _appDbContext;

        public ShipCostSQLRepository(WebShopSampleContext _appDbContext)
        {
            this._appDbContext = _appDbContext;
        }
        public async Task<ShipCost> DeleteAsync(int id)
        {
            ShipCost shipCostInDb = await GetByIdAsync(id);
            _appDbContext.ShipCosts.Remove(shipCostInDb);
            await _appDbContext.SaveChangesAsync();
            return shipCostInDb;
        }

        public async Task<ShipCost> EditAsync(ShipCost shipCost, int id)
        {
            ShipCost shipCostInDb = await GetByIdAsync(id);
            shipCostInDb.CountryId = shipCost.CountryId;
            shipCostInDb.ShipCost1 = shipCost.ShipCost1;
            shipCostInDb.DateModified = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return shipCostInDb;
        }

        public async Task<IEnumerable<ShipCost>> GetAllAsync()
        {
            return await _appDbContext.ShipCosts.ToListAsync();
        }

        public async Task<ShipCost> GetByIdAsync(int id)
        {
            return await _appDbContext.ShipCosts.SingleOrDefaultAsync(c => c.ShipCostId == id);
        }

        public async Task<ShipCost> SaveAsync(ShipCost shipCost)
        {
            shipCost.DateAdded = DateTime.Now;
            _appDbContext.ShipCosts.Add(shipCost);
            await _appDbContext.SaveChangesAsync();
            return shipCost;
        }
    }
}
