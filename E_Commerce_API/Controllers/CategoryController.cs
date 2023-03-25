using Context;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    public class CategoryController : ControllerBase
    {
        private DContext _Context;
        public CategoryController(DContext context)
        {
            _Context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
