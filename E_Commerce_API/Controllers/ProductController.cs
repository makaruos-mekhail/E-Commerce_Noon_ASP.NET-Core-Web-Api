using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //[HttpPost]
        [HttpGet]
        public async Task<IActionResult> Filter(string? name = null, string? nameAr = null,
            string? Category = null, string? CategoryAr = null,
            int? fromPrice = null, int? toPrice = null,
            string? brand = null, string? brandAr = null,
            string? colorName = null)
        {
            var products = await _productRepository.FilterByAsync(name, nameAr,
                Category, CategoryAr, fromPrice, toPrice, brand, brandAr, colorName);


            return Ok(products);
        }
        
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductByID(long id)
        //{
        //    var product = await _productRepository.GetByIdAsync(id);
        //    return Ok(product);

        //}
        [HttpGet]
        [Route("getproductsbyides")]

        public async Task<IActionResult> getproductsbyides([FromQuery] long[] ids)
        {
            return Ok(await _productRepository.GetProductsbyIdes(ids));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getproductdetails(long id)
        {
            return Ok(await _productRepository.GetProductDetailsById(id));
        }

        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct([FromRoute] long id)
		{
			return Ok(await _productRepository.DeleteAsync(id));
		}


	}
}