using DAL.ModelHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceFacadeInterfaces
{
    public interface IOrderFacade
    {
        Task<Order> SaveAsync(Order newOrder);
    }
}
