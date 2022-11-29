using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{
    public class FeedbackController : Controller
    {
        
        IConfiguration _configuration;
        public FeedbackController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFeedback1()
        {
            List<SelectListItem> status = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Select", Text = "select" },
                 new SelectListItem { Value = "true", Text = "Satisfactory" },
                  new SelectListItem { Value ="false" , Text = "Unsatisfactory" },
            };

            ViewBag.Feed = status;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFeedback1(Feedback feedback)
        {
            #region Taking user Feedback
            ViewBag.status = "";           
            feedback.HallTableId =Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/AddFeedback";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Feedback Added Successfull!!";
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
        [HttpGet]
        public async Task<IActionResult> EditFeedack(int feedbackId)
        {
            #region Updaing Feedback
            Feedback feedback = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/GetFeedbackById?feedbackId=" + feedbackId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        feedback = JsonConvert.DeserializeObject<Feedback>(result);
                    }
                }
            }
            return View(feedback);
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> EditFeedback(Feedback feedback)
        {
            #region Updating Feedback Post method
            ViewBag.status = "";            
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/UpdateFeedback";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Feedback Details Updated Successfull!!";
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
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            #region Deleting Feedback Of Customer
            Feedback feedback = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/GetFeedbackByid?feedbackId=" + feedbackId;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        feedback     = JsonConvert.DeserializeObject<Feedback>(result);
                    }
                }
            }
            return View(feedback);
            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFedback(Feedback feedback)
        {
            #region Deleting Feedback in Post method
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/DeleteFeedback?feedbackId=" + feedback.FeedbackId;
                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Feedback Details Deleted Successfull!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }
                }
            }
            return View(feedback);
            #endregion
        }
        public IActionResult GetAllFeedbacks()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks(Feedback feedback)
        {
            #region get all feedbacks Post method
            IEnumerable<Feedback> employeeresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/GetFeedback";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        employeeresult = JsonConvert.DeserializeObject<IEnumerable<Feedback>>(result);
                    }
                }
            }
            return View(employeeresult);
            #endregion
        }
    }
}

