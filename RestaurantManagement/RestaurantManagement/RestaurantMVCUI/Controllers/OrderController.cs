using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantBLL.Services;
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
            #region index of order
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
            #endregion
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

            List<SelectListItem> tableId = new List<SelectListItem>();

            tableId.Add(new SelectListItem { Value = "Select", Text = "select" });
            foreach (var item in halltable)
            {   //if(item.HallTableStatus==true)
                tableId.Add(new SelectListItem { Value = (item.HallTableId).ToString(), Text = "Table Size : "+(item.HallTableSize)+" Table No : "+ item.HallTableId.ToString() });
            }
          
            ViewBag.TableId = tableId; 
            order.OrderDate = DateTime.Now;

            return View(order);

        } 




            


        

        [HttpPost]
        public async Task<IActionResult> AddOrder1(Order order)
        {
            ViewBag.status = "";
     
            HallTable hallTable = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTableById?hallTableId=" + order.HallTableId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        hallTable = JsonConvert.DeserializeObject<HallTable>(result);
                    }



                }
            }
            hallTable.HallTableStatus = false;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(hallTable), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/UpdateHallTable";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content)) ;
           
            }

            
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
            TempData["OrderedTime"]=DateTime.Now;
            TempData.Keep();
           
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
                orders.Clear();
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

        public async Task<IActionResult> CancelOrder()
        { 

            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);

           DateTime orderedtime = Convert.ToDateTime(TempData["OrderedTime"]);
           TempData.Keep();
           DateTime maxTime = orderedtime.AddMinutes(0.5); 
           DateTime cancelTime = DateTime.Now;


            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByHallById?HallId=" + hallTableId1;//api controller name and httppost name given inside httppost in moviecontroller of api

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
                        ViewBag.message = "Your Order is Already Prepared Can't cancel now";
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

        public async Task<IActionResult> ViewOrdered()
        {
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();

            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByHallById?HallId=" + hallTableId1;//api controller name and httppost name given inside httppost in moviecontroller of api

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
        //public IActionResult GetDetails(Order orderObj,Payment paymentobj,FoodS foodsObj)
        //{

        //}

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
            
            DateTime orderedtime = Convert.ToDateTime(TempData["OrderedTime"]);
            TempData.Keep();
            DateTime maxTime = orderedtime.AddMinutes(0.5);
            DateTime cancelTime = DateTime.Now;



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
                    if (response.StatusCode == System.Net.HttpStatusCode.OK && cancelTime <= maxTime )
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Order Details Updated Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Your Order is already prepared can't update now";
                    }

                }
            }
            return View();


        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrder1(int OrderId)
        {
            DateTime orderedtime = Convert.ToDateTime(TempData["OrderedTime"]);
            TempData.Keep();
            DateTime maxTime = orderedtime.AddMinutes(0.5);
            DateTime cancelTime = DateTime.Now;
            ViewBag.status = "";
            Order order = null;
            
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrderById?orderId=" + OrderId;//OrderId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Ordercontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK && cancelTime<=maxTime)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        order = JsonConvert.DeserializeObject<Order>(result);
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = " Ordered prepared can't cancel now";
                    }



                }
            }
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
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBaseUrl"] + "Order/DeleteOrder?orderId=" + OrderId;  //api controller name and its function

                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = food.FoodName+" Order Deleted Successfully!!";
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
      


        public IActionResult ClearCart()
        {
            orders.Clear();
            if (orders.Count == 0)
            {
                ViewBag.status = "Ok";
                ViewBag.message = "Cart Emptied Successfully!!";
            }
            else
            {
                ViewBag.status = "Error";
                ViewBag.message = "Cart not Emptied";
            }

            return View();
        }
    }
    }
