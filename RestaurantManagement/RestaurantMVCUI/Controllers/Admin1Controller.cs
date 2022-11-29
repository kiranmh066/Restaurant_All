﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{
    public class Admin1Controller : Controller
    {

        IConfiguration _configuration;
        public Admin1Controller(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEmployee()
        {

            List<SelectListItem> Gender = new List<SelectListItem>()
            {

                  new SelectListItem { Value = "Select", Text = "select" },
                  new SelectListItem { Value = "M", Text = "Male" },
                  new SelectListItem { Value = "F", Text = "Female" },
            };
            ViewBag.Gender = Gender;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            ViewBag.status = "";
            /*            if (Request.Form.Files.Count > 0)
                        {
                            MemoryStream ms = new MemoryStream();
                            Request.Form.Files[0].CopyTo(ms);
                            Employeev.ImgPoster = ms.ToArray();
                        }*/
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/AddEmployee";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Employee Added Successfull!!";
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
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int employeeId)
        {
            Employee employee = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + employeeId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        employee = JsonConvert.DeserializeObject<Employee>(result);
                    }



                }
            }
            return View(employee);

        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            ViewBag.status = "";
            /* if (Request.Form.Files.Count > 0)
             {
                 MemoryStream ms = new MemoryStream();
                 Request.Form.Files[0].CopyTo(ms);
                 employee.ImgPoster = ms.ToArray();
             }*/
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Employees Details Updated Successfull!!";
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
        //public async Task<IActionResult> DeleteEmployee(int employeeId)
        //{
        //    Employee employee = null;
        //    using (HttpClient client = new HttpClient())
        //    {


        //        string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?employeeId=" + employeeId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

        //        using (var response = await client.GetAsync(endPoint))
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {   //dynamic viewbag we can create any variable name in run time
        //                var result = await response.Content.ReadAsStringAsync();
        //                employee = JsonConvert.DeserializeObject<Employee>(result);
        //            }



        //        }
        //    }
        //    return View(employee);

        //}
       
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/DeleteEmployee?employeeId=" + employeeId;  //api controller name and its function

                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Employees Details Deleted Successfull!!";
                       
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
        public IActionResult GetAllEmployees()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(Employee employee)
        {
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
            return View(employeeresult);
        }

        public IActionResult AddFood()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFood(Food food)
        {
            ViewBag.status = "";
            if (Request.Form.Files.Count > 0)
            {
                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);
                food.FoodImage = ms.ToArray();
            }
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(food), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Food/AddFood";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Food Added Successfull!!";
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
        [HttpGet]
        public async Task<IActionResult> EditFood(int foodId)
        {
            Food food = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + foodId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        food = JsonConvert.DeserializeObject<Food>(result);
                    }



                }
            }
            return View(food);

        }
        [HttpPost]
        public async Task<IActionResult> EditFood(Food food)
        {
            ViewBag.status = "";
            if (Request.Form.Files.Count > 0)
            {
                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);
                food.FoodImage = ms.ToArray();
            }
            
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(food), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Food/UpdateFood";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Food Details Updated Successfull!!";
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
        //public async Task<IActionResult> DeleteFood(int foodId)
        //{
        //    Food food = null;
        //    using (HttpClient client = new HttpClient())
        //    {


        //        string endPoint = _configuration["WebApiBaseUrl"] + "Food/GetFoodById?foodId=" + foodId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

        //        using (var response = await client.GetAsync(endPoint))
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {   //dynamic viewbag we can create any variable name in run time
        //                var result = await response.Content.ReadAsStringAsync();
        //                food = JsonConvert.DeserializeObject<Food>(result);
        //            }



        //        }
        //    }
        //    return View(food);

        //}
        //[HttpPost]
        public async Task<IActionResult> DeleteFood(int foodId)
        {
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBaseUrl"] + "Food/DeleteFood?foodId=" + foodId;  //api controller name and its function

                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Food Details Deleted Successfull!!";
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
        public IActionResult GetAllFoods()
        {
            return View();
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

        public IActionResult AddHallTable()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHallTable(HallTable hallTableInfo)
        {
            ViewBag.status = "";

            hallTableInfo.HallTableStatus = true;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(hallTableInfo), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/AddHallTable";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "HallTable Added Successfull!!";
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

        [HttpGet]
        public async Task<IActionResult> EditHallTable(int hallTableId)
        {
            HallTable hallTable = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTableById?hallTableId=" + hallTableId;

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        hallTable = JsonConvert.DeserializeObject<HallTable>(result);
                    }



                }
            }
            return View(hallTable);

        }
        [HttpPost]
        public async Task<IActionResult> EditHallTable(HallTable hallTable)
        {
            ViewBag.status = "";
            hallTable.HallTableStatus = true;
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(hallTable), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/UpdateHallTable";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "HallTable Details Updated Successfull!!";
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
        //public async Task<IActionResult> DeleteHallTable(int hallTableId)
        //{
        //    HallTable hallTable = null;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTableById?hallTableId=" + hallTableId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

        //        using (var response = await client.GetAsync(endPoint))
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {   //dynamic viewbag we can create any variable name in run time
        //                var result = await response.Content.ReadAsStringAsync();
        //                hallTable = JsonConvert.DeserializeObject<HallTable>(result);
        //            }
        //        }
        //    }
        //    return View(hallTable);

        //}
        //[HttpPost]
        public async Task<IActionResult> DeleteHallTable(int hallTableId)
        {
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/DeleteHallTable?hallTableId=" + hallTableId;  //api controller name and its function

                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "HallTable Details Deleted Successfull!!";
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
        public IActionResult GetAllHallTables()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHallTables(HallTable hallTable)
        {
            IEnumerable<HallTable> hallTableresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTables";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        hallTableresult = JsonConvert.DeserializeObject<IEnumerable<HallTable>>(result);
                    }
                }
            }
            return View(hallTableresult);
        }
       

        public IActionResult GetAllBills()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBills(Bill bill)
        {
            IEnumerable<Bill> billresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Bill/GetBills";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        billresult = JsonConvert.DeserializeObject<IEnumerable<Bill>>(result);
                    }
                }
            }
            return View(billresult);
        }

        public IActionResult GetAllFeedbacks()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks(Feedback feedback)
        {
            IEnumerable<Feedback> feedbackresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/GetAllFeedbacks";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        feedbackresult = JsonConvert.DeserializeObject<IEnumerable<Feedback>>(result);
                    }
                }
            }
            return View(feedbackresult);
        }
    }
}
