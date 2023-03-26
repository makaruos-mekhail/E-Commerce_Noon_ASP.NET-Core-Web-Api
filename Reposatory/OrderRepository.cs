using Application.Contracts;
using Context;
using Domain.Entities;
using E_Commerce_API.Reposatories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposatory
{
    public class OrderRepository : Repository<Order, long>, IOrderRepository
    {
        public OrderRepository(DContext context) : base(context)
        {

        }

        public Task<IEnumerable<Order>> GetAllOrder()
        {
            IEnumerable<Order> orders = _context.Order.ToList();
            return Task.FromResult(orders);
        }
    }
}
