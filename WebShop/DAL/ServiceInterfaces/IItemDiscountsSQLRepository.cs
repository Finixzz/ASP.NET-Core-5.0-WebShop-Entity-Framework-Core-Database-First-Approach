using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IItemDiscountsSQLRepository
    {
        Task<PagedList<ItemDiscount>> GetDiscountedItemsAsync(OwnerParameters ownerParameters);

        Task<ItemDiscount> GetByIdAsync(int id);

        Task<ItemDiscount> SaveAsync(ItemDiscount itemDiscount);

        Task<ItemDiscount> EditAsync(ItemDiscount itemDiscount, int id);

        Task<ItemDiscount> DeleteAsync(int id);
    }
}
