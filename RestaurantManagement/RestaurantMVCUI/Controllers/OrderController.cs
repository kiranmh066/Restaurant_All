using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{

    public class OrderController : Controller
    {
        public static List<Order> orders = new List<Order>();

        
        private IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Food> foodresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoods";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        foodresult = JsonConvert.DeserializeObject<IEnumerable<Food>>(result);
                    }



                }
            }
            return View(foodresult);

        }
        [HttpGet]
        public async Task<IActionResult> AddOrder1(int FoodId)
        {
            Order order = new Order();
            order.FoodId = FoodId;

            Food food = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + FoodId;//movieId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        food = JsonConvert.DeserializeObject<Food>(result);
                    }



                }
            }
            TempData["foodcost1"] = Convert.ToInt32(food.FoodCost);
            TempData.Keep();
            //
            IEnumerable<HallTable> halltable = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTables";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        halltable = JsonConvert.DeserializeObject<IEnumerable<HallTable>>(result);
                    }



                }
            }

            List<SelectListItem> tableId = new List<SelectListItem>()
            {
                new SelectListItem{Value="Select",Text="select"},
                new SelectListItem{Value="1",Text="1"},
                new SelectListItem{Value="2",Text="2"},
                new SelectListItem{Value="3",Text="3"},

            };


            ViewBag.tablelist = tableId;


            return View(order);


        }

        [HttpPost]
        public async Task<IActionResult> AddOrder1(Order order)
        {
            ViewBag.status = "";
            /*            if (Request.Form.Files.Count > 0)
                        {
                            MemoryStream ms = new MemoryStream();
                            Request.Form.Files[0].CopyTo(ms);
                            Orderv.ImgPoster = ms.ToArray();
                        }*/
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                var foodcost = Convert.ToInt32(TempData["foodcost1"]);
                TempData.Keep();
                order.OrderTotal = order.Quantity * foodcost;
                order.OrderStatus = false;

                TempData["halltableuserid"] = order.HallTableId;
                TempData.Keep();


                orders.Add(order);
                TempData["status"] = true;
                while (Convert.ToBoolean(TempData["status"]))
                {
                    TempData.Keep();
                    return RedirectToAction("Index", "Order");
                }
                foreach (var item in orders)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(orders), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Order/AddOrder";//api controller name and its function

                    using (var response = await client.PostAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "Order " + item.OrderId + " Added Successfull!!";
                        }

                        else
                        {
                            ViewBag.status = "Error";
                            ViewBag.message = "Wrong Entries";
                        }

                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            TempData["status"] = false;
            TempData.Keep();
            using (HttpClient client = new HttpClient())
            {
                int count = 0;
                foreach (var item in orders)
                {   
                    StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    string endPoint = _configuration["WebApiBaseUrl"] + "Order/AddOrder";//api controller name and its function
                    count++;
                    using (var response = await client.PostAsync(endPoint, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = count + "Order  Added Successfull!!";
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
           
        }


        [HttpGet]

        public async Task<IActionResult> GetAllFoods(Food food)
        {
            IEnumerable<Food> foodresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoods";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        foodresult = JsonConvert.DeserializeObject<IEnumerable<Food>>(result);
                    }



                }
            }
            return View(foodresult);
        }

        [HttpGet]

        public IActionResult GetOrders1()
        {

            return View(orders);
        }

        public async Task<IActionResult> CancelOrder(int HallId)
        {

            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByHallById?HallId=" + HallId;//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        orderresult = JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
                    }

                    if(orderresult==null)
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "You have Not Ordered Anything";
                    }


                }
            }


            foreach (var item in orderresult)
            {
                ViewBag.status = "";
                //using grabage collection only for inbuilt classes
                using (HttpClient client = new HttpClient())
                {

                    string endPoint = _configuration["WebApiBaseUrl"] + "Order/DeleteOrder?orderId=" + item.OrderId;  //api controller name and its function

                    using (var response = await client.DeleteAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            ViewBag.status = "Ok";
                            ViewBag.message = "Order  Canceled Successfull!!";
                        }

                        
                           

                    }
                }
             

            }
            return View();

        }

        public async Task<IActionResult> ViewOrdered(int HallId)
        {
            TempData.Keep();
            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByHallById?HallId=" + HallId;//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        orderresult = JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
                    }
                    if (orderresult == null)
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "You have Not Ordered Anything";
                    }


                }
            }
            return View(orderresult);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateOrder1(int OrderId)
        {

            Order order= null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrderById?orderId=" + OrderId;//OrderId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Ordercontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        order = JsonConvert.DeserializeObject<Order>(result);
                    }



                }
            }
            return View(order);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder1(Order order)
        {
            ViewBag.status = "";


            Food food = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + order.FoodId;//OrderId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Ordercontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        food = JsonConvert.DeserializeObject<Food>(result);
                    }



                }
            }

            order.OrderTotal = order.Quantity * Convert.ToInt32(food.FoodCost);
            order.OrderStatus = false;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/UpdateOrder";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Orders Details Updated Successfull!!";
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
    }
    }
