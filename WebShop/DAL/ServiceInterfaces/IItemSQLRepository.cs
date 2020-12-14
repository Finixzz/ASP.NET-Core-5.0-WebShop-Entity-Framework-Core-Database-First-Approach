using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IItemSQLRepository
    {
        
        Task<PagedList<Item>> GetItemsAsync(OwnerParameters ownerParameters);
        Task<Item> GetByIdAsync(int id);
        Task<Item> SaveAsync(Item item);

        Task<Item> EditAsync(Item item, int id);

        Task<Item> DeleteAsync(int id);
        
    }
}
