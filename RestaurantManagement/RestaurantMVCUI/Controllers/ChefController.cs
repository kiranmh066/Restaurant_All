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
            #region Showing All the Work Assigned to him by HeadChef in Index page
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
            #endregion
        }        
        public async Task<IActionResult> ChefProfile()
        {
            #region getting All The Details Of chef Through Profile
            int employeeId = Convert.ToInt32(TempData["empId"]);
            TempData.Keep();

            Employee employee = null;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + employeeId;
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            return View(employee);
            #endregion
        }
        public async Task<IActionResult> PreparedFood(int AssignId)
        {
            #region Changing the WorkStatus of Chef's assigned Work By Getting assigned Work and Updating Status
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
            #endregion
        }

    }
}
