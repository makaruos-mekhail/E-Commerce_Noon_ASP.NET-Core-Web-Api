using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    public class OrderController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
