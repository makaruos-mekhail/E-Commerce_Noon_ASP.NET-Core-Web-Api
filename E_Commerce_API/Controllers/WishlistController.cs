using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class WishlistController : ControllerBase
	{
		private readonly IWishListRepository _WishListRepository;
		public WishlistController(IWishListRepository wishListRepository)
		{
			_WishListRepository = wishListRepository;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllImages()
		{
			var wishLists = await _WishListRepository.GetAllAsync();

			return Ok(wishLists);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetWishListsByID(long id)
		{
			var wishList = await _WishListRepository.GetByIdAsync(id);
			return Ok(wishList);

		}
		[HttpDelete("id")]
		public async Task<IActionResult> DeleteProductImage(long id)
		{
			var wishList = await _WishListRepository.DeleteAsync(id);
			return Ok(wishList);
		}
	}
}
