using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reposatory;

namespace E_Commerce_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _iorderRepository;

		private readonly DContext db ;

		public OrderController(IOrderRepository iorderRepository, DContext dContext)
		{
			db = dContext;
			_iorderRepository = iorderRepository;

		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderByID(long id)
		{
			var order = await _iorderRepository.GetByIdAsync(id);
			return Ok(order);

		}


		[HttpGet]
		public async Task<IActionResult> GetAllOrders()
		{
			var orders = await _iorderRepository.GetAllOrder();

			return Ok(orders);
		}

		//[HttpPost]
		//public async Task<IActionResult> AddOrder(Order order)
		//{
		//	var data = await _iorderRepository.AddOreder(order);
		//	return Ok(data);
		//}

		[HttpPost]
		public async Task<IActionResult> AddOrder(Order order)
		{
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
