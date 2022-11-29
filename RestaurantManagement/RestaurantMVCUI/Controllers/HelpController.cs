using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class HelpController : Controller
    {
        IConfiguration _configuration;
        static int count = 0;
        public HelpController(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddHelp()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHelp(Help help)
        {
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();
            ViewBag.status = "";
            help.HallTableId = hallTableId1;

            //var data = ViewBag.Message;
            
            
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(help), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Help1/AddHelp";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        count++;
                        TempData["notifyhelp"] = count;
                        TempData.Keep();
                        ViewBag.status = "Ok";
                        ViewBag.message = "Message send succesfully !!";
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

        public IActionResult GetAllHelps()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHelps(Help help)
        {
            IEnumerable<Help> helpresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Help1/GetAllHelps";//api controller name and httppost name given inside httppost in moviecontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        TempData["notifyhelp"] = 0;
                        var result = await response.Content.ReadAsStringAsync();
                        helpresult = JsonConvert.DeserializeObject<IEnumerable<Help>>(result);
                    }



                }
            }
            return View(helpresult);
        }


    }
}
