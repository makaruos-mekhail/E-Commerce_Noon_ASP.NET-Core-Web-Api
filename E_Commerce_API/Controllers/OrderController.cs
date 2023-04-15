using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reposatory.DTOs;
using Repository.DTOs;
using System.Collections.Generic;
using System.Net;
namespace E_Commerce_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _iorderRepository;
        private readonly UserManager<User> _userManager;

        private readonly DContext db ;

		public OrderController(IOrderRepository iorderRepository, UserManager<User> userManager, DContext dContext)
		{
			db = dContext;
			_iorderRepository = iorderRepository;
            _userManager = userManager;

        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderByID(long id)
		{
			var order = await _iorderRepository.GetByIdAsync(id);
			return Ok(order);

		}
        [HttpGet]
		public async Task<IActionResult> GetAllOrders(string userEmail,string status)
		{
            var order = await _iorderRepository.GetAllOrder(userEmail, status);
			return Ok(order);
		}

		////all orders
		[HttpGet]
		public async Task<IActionResult> GetAllUserOrders(string userEmail)
		{
			var orders = await _iorderRepository.GetAllUserOrders(userEmail);
			return Ok(orders);
		}


		[HttpPost]
		public async Task<IActionResult> AddOrder(OrderDTO orderDto)
		{
            var user = await _userManager.FindByEmailAsync(orderDto.username);
            List<OrderItems> orderItems = new List<OrderItems>();
			foreach(var or in orderDto.OrderItemsDTO)
			{
				Product p = db.Product.Single(p => p.Id == or.productid);
				OrderItems orderItems1 = new OrderItems()
				{
					Product = p,
					ProductId = or.productid,
					Quantity = or.Quantity
                };
				orderItems.Add(orderItems1);

            }
			Order order = new Order()
			{
				Address = user.Address,
				PaymentMethod = orderDto.PaymentMethod,
				UserPhone = user.Phone,
				User = user,
				TotalPrice = orderDto.TotalPrice,
				OrderItems = orderItems,
				Status = "Processing",
				IsDeleted = false,
				CreatedAt = DateTime.Now

            };

			 await db.AddAsync(order);
			await db.SaveChangesAsync();
			return Ok(order);
		}

		[HttpDelete("{id}")]
		public async Task<bool> DeleteOrder(long id)
		{
			return await _iorderRepository.DeleteAsync(id);

		}
		

	}
}
