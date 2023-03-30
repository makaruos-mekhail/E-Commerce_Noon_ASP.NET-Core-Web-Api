using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository ReviewRepository)
        {
            _reviewRepository = ReviewRepository;
        }

        [HttpGet("{prodid}")]
        public async Task<IActionResult> Filter(long prodid)
        {
            var ReviewProduct = await _reviewRepository.GitByProductIdAscyn(prodid);
            return Ok(ReviewProduct);
        }

    }
}
