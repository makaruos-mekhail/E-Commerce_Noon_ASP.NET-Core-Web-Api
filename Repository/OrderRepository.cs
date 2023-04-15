using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reposatory.DTOs;

namespace Repository
{
	public class OrderRepository : Repository<Order, long>, IOrderRepository
    {
		
        public OrderRepository(DContext context) : base(context)
        { }

        public Task<Order> GetAllOrder(string userEmail,string status)
		{
            var user = _context.Users.Single(u=>u.UserName== userEmail);
            var order = _context.Order.Include(o=>o.OrderItems).ThenInclude(o=>o.Product).Where(o=>o.UserId==user.Id)
				.Where(or=>or.Status== status).OrderByDescending(o=>o.CreatedAt).First();
        

            return Task.FromResult(order);
        }
        public Task<IEnumerable<Order>> GetAllUserOrders(string userEmail)
        {
            var user = _context.Users.Single(u => u.UserName == userEmail);
            IEnumerable<Order> orders = _context.Order.Include(o => o.OrderItems).ThenInclude(o => o.Product).AsNoTracking().Where(o => o.UserId == user.Id)
               .OrderByDescending(o => o.CreatedAt);


            return Task.FromResult( orders);
        }

        public async Task<bool> CancelOrder(long orderid)
        {
            var _order = _context.Order.Single(o => o.Id == orderid);
            _order.Status = "Cancled";
            _order.IsDeleted = true;
            _context.Update(_order);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

    }
}
