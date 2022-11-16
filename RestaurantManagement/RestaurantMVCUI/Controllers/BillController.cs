using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace RestaurantMVCUI.Controllers
{
    public class BillController : Controller
    {
        private IConfiguration _configuration;
        public BillController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
     /*   [HttpPost]
        public IActionResult Index(int hallTableId)
        {
            TempData["hallTableId"] = hallTableId;
            return RedirectToAction("GenerateBill", "Bill");
        }*/
        public async Task<IActionResult> GenerateBill()
        {
            /*int hallTableId1 = Convert.ToInt32(TempData["hallTableId"]);*/
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();
            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByTableId?hallTableId=" + hallTableId1;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        orderresult = JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
                    }
                }
            }
            return View(orderresult);
        }
    }
}
