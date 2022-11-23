using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

    }
}
