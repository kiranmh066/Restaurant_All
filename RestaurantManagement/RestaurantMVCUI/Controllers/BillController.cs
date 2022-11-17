using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
<<<<<<< HEAD
using System.Text;
=======
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419

namespace RestaurantMVCUI.Controllers
{
    public class BillController : Controller
    {
        private IConfiguration _configuration;
        public BillController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
<<<<<<< HEAD
        
=======
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419
        public IActionResult Index()
        {
            return View();
        }
<<<<<<< HEAD
        [HttpPost]
        public async Task<IActionResult> Index(Bill bill)
        {
            Bill bill1 = new Bill();
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
            double total1=0;
            foreach(var item in orderresult)
            {
                total1 += item.OrderTotal;
            }
            
            ViewBag.status = "";

           
            bill1.BillStatus = false;
            bill1.HallTableId= hallTableId1;
            bill1.BillTotal = total1;
            bill1.BillDate=DateTime.Now;
            bill1.UserName = bill.UserName;
            bill1.UserEmail = bill.UserEmail;
    

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bill1), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Bill/AddBill";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Bill Paid Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }

                }
            }
            return View();


        }


        public async Task<IActionResult> GenerateBill()
        {

=======
     /*   [HttpPost]
        public IActionResult Index(int hallTableId)
        {
            TempData["hallTableId"] = hallTableId;
            return RedirectToAction("GenerateBill", "Bill");
        }*/
        public async Task<IActionResult> GenerateBill()
        {
            /*int hallTableId1 = Convert.ToInt32(TempData["hallTableId"]);*/
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419
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
<<<<<<< HEAD
       /* public async Task<IActionResult> GenerateBill()
        {
           
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
        }*/
=======
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419
    }
}
