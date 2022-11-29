using Microsoft.AspNetCore.Mvc;

namespace RestaurantMVCUI.Controllers
{
    public class HelpBotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
