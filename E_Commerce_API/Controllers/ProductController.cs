using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Reposatory;

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
        [HttpGet]
        public async Task<IActionResult> Filter(string? name, string? Category, int? fromPrice, int? toPrice, string? brand, long? colorId)
        {
            var products = await _productRepository.FilterByAsync(name, Category, fromPrice, toPrice, brand, colorId);

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByID(long id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return Ok(product);

        }


    }
}