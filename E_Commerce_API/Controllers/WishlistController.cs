using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.DTOs;

namespace E_Commerce_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
    [Authorize]
	public class WishlistController : ControllerBase
	{
		private readonly IWishListRepository _WishListRepository;
		public WishlistController(IWishListRepository wishListRepository)
		{
			_WishListRepository = wishListRepository;
		}
        [HttpGet]
        public async Task<IActionResult> GetAll(string userEmail)
        {
            var wishLists = await _WishListRepository.GetAllAsync(userEmail);

            return Ok(wishLists);
        }


        [HttpPost]
        public async Task<IActionResult> AddProductToWishList([FromBody]WishlistDto wishlistDto)
        {
            var result = await _WishListRepository.AddProductToWishlist(wishlistDto.useremail, wishlistDto.productid);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProductFromWishList( string useremail ,long  productid)//[FromBody] WishlistDto wishlistDto)
        {
            var result = await _WishListRepository.DeleteProductFromWishlist(useremail, productid); //wishlistDto.useremail, wishlistDto.productid);
            return Ok(result);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllImages()
        //{
        //	var wishLists = await _WishListRepository.GetAllAsync();

        //	return Ok(wishLists);
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetWishListsByID(long id)
        //{
        //	var wishList = await _WishListRepository.GetByIdAsync(id);
        //	return Ok(wishList);

        //}
        //[HttpDelete("id")]
        //public async Task<IActionResult> DeleteProductImage(long id)
        //{
        //	var wishList = await _WishListRepository.DeleteAsync(id);
        //	return Ok(wishList);
        //}
    }
}
