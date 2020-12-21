using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IShipAddressSQLRepository
    {
        Task<IEnumerable<ShipAddress>> GetAllByUserIdAsync(string userId);

        Task<ShipAddress> GetByIdAsync(int id);

        Task<ShipAddress> SaveAsync(ShipAddress shipAddress);

        Task<ShipAddress> EditAsync(ShipAddress shipAddress, int id);

        Task<ShipAddress> DeleteAsync(int id);
    }
}
