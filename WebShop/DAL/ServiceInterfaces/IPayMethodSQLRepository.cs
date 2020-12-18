using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ServiceInterfaces
{
    public interface IPayMethodSQLRepository
    {
        Task<IEnumerable<PayMethod>> GetAllAsync();

        Task<PayMethod> GetById(int id);
    }
}
