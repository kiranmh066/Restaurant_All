using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{
    public class EmployeeController : Controller
    {
        private IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
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
        }

        public IActionResult Login1()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login1(Employee employee)
        {

            Employee employee1 = null ;
            ViewBag.status = "";


            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/Login";
                using (var response = await client.PostAsync(endPoint, content))
                {

                  
                    var result = await response.Content.ReadAsStringAsync();
                    employee1 = JsonConvert.DeserializeObject<Employee>(result);
                    string employee_designation = (employee1.EmpDesignation).ToString();
                    TempData["employee_designation"] = employee_designation;
                    TempData.Keep();
                    TempData["empId"] = Convert.ToInt32(employee1.EmpId);
                    TempData.Keep();

                    if (employee_designation == "CHEF")
                        return RedirectToAction("Index", "Chef");
                    else if (employee_designation == "HEADCHEF")
                        return RedirectToAction("Index", "HeadChef");
                    else if (employee_designation == "HALLMANAGER")
                        return RedirectToAction("Index", "HallManager");
                    else if (employee_designation == "ADMIN")
                        return RedirectToAction("Index", "Admin1");
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong credentials!";
                    }
                }
            }
            return View();



        }
        public IActionResult Forgot()
        {  

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Forgot(Employee employee)
        {
            string a = employee.EmpPassword;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + employee.EmpId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(result);
                    }



                }
            }
            employee.EmpPassword = a;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Password Updated Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Password not updated error";
                    }

                }
            }
            return View();

        }
    }
}
