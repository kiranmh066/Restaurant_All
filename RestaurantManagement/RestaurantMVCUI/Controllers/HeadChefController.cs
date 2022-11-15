using Microsoft.AspNetCore.Mvc;

namespace RestaurantMVCUI.Controllers
{
    public class HeadChefController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
