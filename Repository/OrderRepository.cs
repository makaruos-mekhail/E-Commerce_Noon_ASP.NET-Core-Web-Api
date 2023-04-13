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

        

        //public Task<Order> AddOreder(OrderDTO order)
        //{
        //	User user = _context.Users.Where(a => a.Id == order.UserId).FirstOrDefault();

        //	ICollection<OrderItems> OrderItems = new List<OrderItems>();
        //	Product p = _context.Product.Where(a => a.Id == order.orderItem.ProductId).FirstOrDefault();

        //	foreach(var o in order.OrderItemsDTO)
        //	{

        //	}
        //	OrderItems orderitem1 = new OrderItems();
        //	orderitem1.Product = p;
        //	orderitem1.ProductId=order.orderItem.ProductId;
        //	orderitem1.Quantity=order.orderItem.Quantity;

        //	OrderItems.Add(orderitem1);



        //          Order order1 = new Order();
        //	order1.Address = order.Address;
        //	order1.PaymentMethod = order.PaymentMethod;
        //	//order1.PaymentStatus = order.PaymentStatus;
        //	order1.TotalPrice = order.TotalPrice;
        //	order1.OrderItems = OrderItems;
        //	order1.User = user;
        //	_context.Order.Add(order1);
        //	_context.SaveChanges();
        //	return Task.FromResult(order1);

        //}



        //public Task<Order> AddOreder(Order order)
        //{
        //          _context.Order.Add(order);
        //	_context.SaveChanges();
        //	return Task.FromResult(order);
        //}
    }
}
