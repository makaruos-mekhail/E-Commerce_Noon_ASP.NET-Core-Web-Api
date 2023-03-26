using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BrandController : ControllerBase
    {

        private readonly IBrandRepository _iBrandRepository;

        public BrandController(IBrandRepository iBrandRepository)
        {
            _iBrandRepository = iBrandRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Filter(string? filter)
        {
            var brands = await _iBrandRepository.FilterByAsync(filter);
            return Ok(brands);
        }

        [HttpGet("{brandid}")]
        public async Task<IActionResult> GetById(int brandid)
        {
			var brand = await _iBrandRepository.GetByIdAsync(brandid);
			return Ok(brand);
		}

    }
}
