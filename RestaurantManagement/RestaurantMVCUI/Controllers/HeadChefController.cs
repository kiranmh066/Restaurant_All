using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace RestaurantMVCUI.Controllers
{
    public class HeadChefController : Controller
    {
        private IConfiguration _configuration;

        public HeadChefController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Order> orderResult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrders";

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        orderResult = JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
                    }
                }
            }
            return View(orderResult);
        }

        public async Task<IActionResult> cheflist(int OrderId)
        {
            Order order = new Order();
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
            TempData["OrderIdforAssign"] = OrderId;
            TempData.Keep();
            IEnumerable<Employee> employeeresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployees";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        employeeresult = JsonConvert.DeserializeObject<IEnumerable<Employee>>(result);
                    }



                }
            }
            List<Employee> employeeresultchef = new List<Employee>();

            foreach (var item in employeeresult)
            {
                if (item.EmpDesignation == "CHEF" && item.EmpSpeciality == order.Food.FoodCuisine)
                    employeeresultchef.Add(item);
            }
            return View(employeeresultchef);
        }
    }
}
