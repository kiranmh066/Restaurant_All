using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantBLL.Services;
using RestaurantDAL;
using RestaurantEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{

    public class OrderController : Controller
    {
        public static List<Order> orders = new List<Order>();

        private IConfiguration _configuration;
        RestaurantDbContext db = new RestaurantDbContext();
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index(string option, string search)
        {
            #region Search Button
            if (option == "Subjects")
            {
                //GetAllPatients action method will return a view with a patient records based on what a user specify the value in textbox  
                return View(db.tbl_Food.Where(x => x.FoodType.StartsWith( search )|| search == null).ToList());
            }
            else if (option == "Gender")
            {
                return View(db.tbl_Food.Where(x => x.FoodCuisine.StartsWith(search )|| search == null).ToList());
            }
            else
            {
                return View(db.tbl_Food.Where(x => x.FoodName.StartsWith(search) || search == null).ToList());
            }
            #endregion
        }

        [HttpGet]
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
            #region Adding order to the Cart
            Order order = new Order();
            order.FoodId = FoodId;

            Food food = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + FoodId;
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
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTables";
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
<<<<<<< HEAD
            { 
           
                   
                tableId.Add(new SelectListItem { Value = (item.HallTableId).ToString(), Text = "Table Size : "+(item.HallTableSize)+" Table No : "+ item.HallTableId.ToString() });

            }          

=======
            {   //if(item.HallTableStatus==true)

                tableId.Add(new SelectListItem { Value = (item.HallTableId).ToString(), Text = "Table Size : " + (item.HallTableSize) + " Table No : " + item.HallTableId.ToString() });
            }
      

                   

              
>>>>>>> 898803bffde025b1ca5a3a61eba2adcee4133adc
            ViewBag.TableId = tableId; 

            order.OrderDate = DateTime.Now;
           
            return View(order);
            #endregion
        }


        [HttpPost]
        public async Task<IActionResult> AddOrder1(Order order)
        {
            #region Adding order to the Cart
            ViewBag.status = "";
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();

            HallTable hallTable = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTableById?hallTableId=" + hallTableId1;
                
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
                order.HallTableId = hallTableId1;
                int count = 0;

                foreach(var item in orders)
                {
                    if(item.FoodId==order.FoodId)
                    {
                        int tq1 = item.Quantity;
                        int tt1 = item.OrderTotal;
                        int tt = tt1 / tq1;
                        item.Quantity = order.Quantity;
                        item.OrderTotal = order.Quantity * tt;
                        count++;

                    }
                 

                   
                }
                if(orders.Count==0)
                {
                    orders.Add(order);
                }

                else if (orders.Count != 0 && count == 0)
                {
                    orders.Add(order);
                }

           
               
                    
                

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
            #endregion
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            #region Cinfirming Order From The Cart
            TempData["OrderedTime"]=DateTime.Now;
            TempData.Keep();
           
            TempData["status"] = false;
            TempData.Keep();

            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();
            using (HttpClient client = new HttpClient())
            {
                int count = 0;
               
                foreach (var item in orders)
                {   if (item.HallTableId == hallTableId1)
                    {

                        StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                        string endPoint = _configuration["WebApiBaseUrl"] + "Order/AddOrder";//api controller name and its function
                        count++;
                        using (var response = await client.PostAsync(endPoint, content))
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {   //dynamic viewbag we can create any variable name in run time
                                ViewBag.status = "Ok";
                                if (count == 1)
                                    ViewBag.message = " " + count + "Item Ordered Successfully!";
                                else
                                    ViewBag.message = " " + count + "Items Ordered Successfully!";

                            }

                            else
                            {
                                ViewBag.status = "Error";
                                ViewBag.message = "Not able to order Items";
                            }

                        }
                    }

                                  

                }
                orders.Clear();
                return View();
            }
            #endregion
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFoods(Food food)
        {
            #region getting All Food Items
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
        public async Task<IActionResult> GetOrders1()
        {
            #region Getting orders            

            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();
            List<Food> foodresult = new List<Food>();
            foreach(var item in orders)
            {
                if (hallTableId1==item.HallTableId) {
                    Food food = null;
                    using (HttpClient client = new HttpClient())


                    {


                        string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + item.FoodId;//movieId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in moviecontroller of api

                        using (var response = await client.GetAsync(endPoint))
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {   //dynamic viewbag we can create any variable name in run time
                                var result = await response.Content.ReadAsStringAsync();
                                food = JsonConvert.DeserializeObject<Food>(result);
                            }

                            foodresult.Add(food);

                        }

                    }
                }                
            }

            List<Order>customerorderview=new List<Order>();
            foreach(var item in orders)
            {
                if(hallTableId1 == item.HallTableId)
                {
                    customerorderview.Add(item);
                }
            }

            var tupeluser = new Tuple<List<Order>, List<Food>>(customerorderview, foodresult);

            return View(tupeluser);
            #endregion
        }

        public async Task<IActionResult> CancelOrder()
        {
            #region Cancelling/Removing Orders From Cart
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();                

            DateTime orderedtime = Convert.ToDateTime(TempData["OrderedTime"]);
            TempData.Keep();
            DateTime maxTime = orderedtime.AddMinutes(0.5); 
            DateTime cancelTime = DateTime.Now;

            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByHallById?HallId=" + hallTableId1;
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
            //to make hall table empty
            HallTable hallTable = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTableById?hallTableId=" + hallTableId1;//api controller name and its function
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        hallTable = JsonConvert.DeserializeObject<HallTable>(result);
                    }
                }
            }
            hallTable.HallTableStatus = true;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(hallTable), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/UpdateHallTable";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content)) ;              
            }
            return View();
            #endregion
        }

        public async Task<IActionResult> ViewOrdered()
        {
            #region Viewing Orders of The Customer
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();

            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByTableId?hallTableId=" + hallTableId1;
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
            List<AssignWork> assignWorkslist = new List<AssignWork>();
            using (HttpClient client = new HttpClient())
            {
            string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/GetAssignWorks";
            //api controller name and httppost name given inside httppost in moviecontroller of api
            using (var response = await client.GetAsync(endPoint))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {   //dynamic viewbag we can create any variable name in run time
                    var result = await response.Content.ReadAsStringAsync();
                    assignWorkslist = JsonConvert.DeserializeObject<List<AssignWork>>(result);
                }
            }
            }
            var tupeluser = new Tuple<IEnumerable<Order>, List<AssignWork>>(orderresult, assignWorkslist);
            return View(tupeluser);
            #endregion
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrder1(int OrderId)
        {
            #region Updating Order before Preparing order
            Order order= null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrderById?orderId=" + OrderId;
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
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder1(Order order)
        {
            #region Updating Order Before Preparing Order In Post method
            ViewBag.status = "";
            
            DateTime orderedtime = Convert.ToDateTime(TempData["OrderedTime"]);
            TempData.Keep();
            DateTime maxTime = orderedtime.AddMinutes(0.5);
            DateTime cancelTime = DateTime.Now;

            Food food = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + order.FoodId;
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
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/UpdateOrder";

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
            #endregion
        }

        [HttpGet]
        public async Task<IActionResult> DeleteOrder1(int OrderId)
        {
            #region Deleting Order before Preparing Order
            DateTime orderedtime = Convert.ToDateTime(TempData["OrderedTime"]);
            TempData.Keep();
            DateTime maxTime = orderedtime.AddMinutes(1);
            DateTime cancelTime = DateTime.Now;
            ViewBag.status = "";
            Order order = null;
            
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrderById?orderId=" + OrderId;
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
                        ViewBag.message = " Order On Stove can't cancel now";
                        return View();
                    }
                }
            }
            Food food = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + order.FoodId;
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
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/DeleteOrder?orderId=" + OrderId;
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
            #endregion
        }

        public IActionResult ClearCart()
        {
            #region Clearing cart
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
            #endregion
        }
        public async Task<IActionResult> UserTableEntry()
        {
            #region Entering Halltable Number
            IEnumerable<HallTable> halltable = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTables";
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
                tableId.Add(new SelectListItem { Value = (item.HallTableId).ToString(), Text = "Table Size : " + (item.HallTableSize) + " Table No : " + item.HallTableId.ToString() + " Table Status : " + (item.HallTableStatus) });
      
            }

            
            ViewBag.TableId = tableId;

            return View();
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> UserTableEntry(Order order)
        {
            #region Entering HallTable number

            ViewBag.status = "";
         
            if (order.HallTableId == 0)
            {
                ViewBag.status = "Ok";
                ViewBag.message = "Please Select Hall Table Number";
                return View();
            }
                


            TempData["halltableuserid"] = order.HallTableId;
            TempData.Keep();

            return RedirectToAction("Index","Order");
            #endregion
        }
    }
}
