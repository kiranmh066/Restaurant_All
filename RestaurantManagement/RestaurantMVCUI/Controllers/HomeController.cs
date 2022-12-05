using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantMVCUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            #region Home page Of AR Restaurant

            return View();
            #endregion
        }


        public IActionResult Privacy()
        {
            #region Privacy page of AR Restaurant
            return View();
            #endregion
        }
        public IActionResult Contactus()
        {
            #region contact us  page of AR Restaurant
            return View();
            #endregion
        }


        public IActionResult Aboutus()
        {
            #region About us page of AR Restaurant

            return View();
            #endregion
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
