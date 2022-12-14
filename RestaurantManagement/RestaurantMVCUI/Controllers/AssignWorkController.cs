using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantMVCUI.Controllers
{
    public class AssignWorkController : Controller
    {
        IConfiguration _configuration;
        public AssignWorkController(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public async Task<IActionResult> Index(int EmpId)
        {
            #region Index of Assigning Work To Chef of Each order
            int data = Convert.ToInt32(TempData["OrderIdforAssign"]);
            TempData.Keep();
            Order order = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrderById?orderId=" + data;
                //OrderId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Ordercontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        order = JsonConvert.DeserializeObject<Order>(result);
                    }
                }
            }
            order.OrderStatus = true;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/UpdateOrder";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content)) ;
    
            }
            AssignWork assignWork = new AssignWork();
            assignWork.EmpId = EmpId;
            assignWork.OrderId = data;

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(assignWork), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/AddAssignWork";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Work Assigned Successfull!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Not Assigned";
                    }
                }
            }
            return View();
            #endregion
        }

        public async Task<IActionResult> ViewAll()
        {
            #region getting All Assigned Chefs and Orders Assigned To Them.
            List<AssignWork> workresult = null;            
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/GetAssignWorks";
                //api controller name and httppost name given inside httppost in ordercontroller of api
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        workresult = JsonConvert.DeserializeObject<List<AssignWork>>(result);
                    }
                }
            }
            List<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrders";
                //api controller name and httppost name given inside httppost in ordercontroller of api
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        orderresult = JsonConvert.DeserializeObject<List<Order>>(result);
                    }
                }
            }
            var tupeluser = new Tuple<List<Order>, List<AssignWork>>(orderresult, workresult);
            return View(tupeluser);
            #endregion
        }

        public async Task<IActionResult> EditAssignWork(int AssignWorkID)
        {
            #region Updating assigned Work of Chef
            AssignWork assignWork = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/GetAssignWorkById?assignWorkId=" + AssignWorkID;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        assignWork = JsonConvert.DeserializeObject<AssignWork>(result);
                    }
                }
            }
            return View(assignWork);
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> EditAssignWork(AssignWork assignWork)
        {
            #region Updating Assigned work of Chef in Post method
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(assignWork), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/UpdateAssignWork";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Assigned Work deatils Updated Successfully";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries....!!!  :)";
                    }
                }
            }
            return View();
            #endregion
        }
        public async Task<IActionResult> DeleteAssignWork1(int AssignWorkID)
        {
            #region Getting Assigned Work of Chef by Id for deleting Chef Work
            AssignWork assignWork = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/GetAssignWorkById?assignWorkId=" + AssignWorkID;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        assignWork = JsonConvert.DeserializeObject<AssignWork>(result);
                    }
                }
            }
            return View(assignWork);
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAssignWork1(AssignWork assignWork)
        {
            #region deleting Assigned Work of Chef in Post Method
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {

                string endpoint = _configuration["WebApiBaseUrl"] + "AssignWork/DeleteAssignWork?assignWorkId=" + assignWork.AssignId;
                using (var response = await client.DeleteAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Assigned Work detaisl deleted succesfully";
                    }
                    else
                    {
                        ViewBag.staus = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }
            return View();
            #endregion
        }

    }
}
