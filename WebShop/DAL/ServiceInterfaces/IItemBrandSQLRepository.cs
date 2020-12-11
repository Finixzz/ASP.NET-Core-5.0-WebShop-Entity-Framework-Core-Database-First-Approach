using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IItemBrandSQLRepository
    {
        Task<IEnumerable<ItemBrand>> GetAllAsync();
        Task<ItemBrand> GetByIdAsync(int id);
        Task<ItemBrand> SaveAsync(ItemBrand itemBrand);

        Task<ItemBrand> EditAsync(ItemBrand itemBrand, int id);

        Task<ItemBrand> DeleteAsync(int id);
    }
}
