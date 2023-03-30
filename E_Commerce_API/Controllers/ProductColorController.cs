using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace E_Commerce_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductColorController : ControllerBase
    {
        private readonly IProductColorRepository _iproductColorRepository;

        public ProductColorController(IProductColorRepository iproductColorRepository)
        {
            _iproductColorRepository = iproductColorRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Filter(string? name)
        {
            var colors = await _iproductColorRepository.FilterByAsync(name);

            return Ok(colors);
        }

    }
}
