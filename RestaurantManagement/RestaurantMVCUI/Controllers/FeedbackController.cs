using Microsoft.AspNetCore.Mvc;

namespace RestaurantMVCUI.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
