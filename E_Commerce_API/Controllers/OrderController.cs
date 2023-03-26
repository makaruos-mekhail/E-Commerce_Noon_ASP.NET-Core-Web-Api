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


		public OrderController(IOrderRepository iorderRepository)
		{
			_iorderRepository = iorderRepository;

		}
		// [HttpPost]
		//public async Task<IActionResult> createorder()
		//{
		//    var data = con.Order.Add(order);
		//        await con.SaveChangesAsync();
		//       return Ok(data);
		//}

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

		[HttpDelete("{id}")]
		public async Task<bool> DeleteOrder(long id)
		{
			return await _iorderRepository.DeleteAsync(id);

		}


	}
}
