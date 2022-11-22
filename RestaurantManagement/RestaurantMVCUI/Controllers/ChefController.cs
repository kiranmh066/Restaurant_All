using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{
    public class ChefController : Controller
    {
        IConfiguration _configuration;
        public ChefController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AssignWork> assignWork = null;
            using (HttpClient client = new HttpClient())
            {
                int EmpId =Convert.ToInt32(TempData["empId"]);
                TempData.Keep();
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/GetAssignWorkByEmpId?empId=" + EmpId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        assignWork = JsonConvert.DeserializeObject<IEnumerable<AssignWork>>(result);
                    }
                }
            }
            return View(assignWork);
        }
        /*[HttpPost]
        public async Task<IActionResult> Index(AssignWork assignWork)
        {
            return View();
        }*/

        public async Task<IActionResult> PreparedFood(int AssignId)
         {
            AssignWork assignWork = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/GetAssignWorkById?assignWorkId=" + AssignId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        assignWork = JsonConvert.DeserializeObject<AssignWork>(result);
                    }
                }
            }
            return View(assignWork);

        }
        [HttpPost]
        public async Task<IActionResult> PreparedFood(AssignWork assignWork)
        {
            ViewBag.status = "";            
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/GetAssignWorkById?assignWorkId=" + assignWork.AssignId;
                //EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        assignWork = JsonConvert.DeserializeObject<AssignWork>(result);
                    }
                }
            }
            assignWork.WorkStatus = true;

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(assignWork), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "AssignWork/UpdateAssignWork";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Assigned Work Prepared and Ready to Serve!!";
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
