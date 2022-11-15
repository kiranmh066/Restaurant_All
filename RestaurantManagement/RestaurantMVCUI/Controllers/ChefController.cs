using Microsoft.AspNetCore.Mvc;

namespace RestaurantMVCUI.Controllers
{
    public class ChefController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
