using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IOrderRepository : IRepository<Order, long>
    {
        Task<IEnumerable<Order>> GetAllOrder();

    }
}
