using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    public class OrderItemController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
