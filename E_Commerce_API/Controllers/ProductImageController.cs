using Application.Contracts;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Reposatory;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageController(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var productimages = await _productImageRepository.GetAllAsync();

            return Ok(productimages);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageByID(long id)
        {
            var productimage = await _productImageRepository.GetByIdAsync(id);
            return Ok(productimage);

        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductImage(long id)
        {
            var productimage = await _productImageRepository.DeleteAsync(id);
            return Ok(productimage);
        }
        
     
    }
}
