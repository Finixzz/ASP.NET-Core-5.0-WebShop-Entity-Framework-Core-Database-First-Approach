using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IShipCostSQLRepository
    {
        Task<IEnumerable<ShipCost>> GetAllAsync();

        Task<ShipCost> GetByIdAsync(int id);

        Task<ShipCost> SaveAsync(ShipCost shipCost);

        Task<ShipCost> EditAsync(ShipCost shipCost, int id);

        Task<ShipCost> DeleteAsync(int id);


    }
}
