using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
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

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddFeedback1(Feedback feedback)
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
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/AddFeedback";//api controller name and its function

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
        }
        [HttpGet]
        public async Task<IActionResult> EditFeedack(int feedbackId)
        {
            Feedback feedback = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/GetFeedbackById?feedbackId=" + feedbackId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

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

        }
        [HttpPost]
        public async Task<IActionResult> EditFeedback(Feedback feedback)
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
                StringContent content = new StringContent(JsonConvert.SerializeObject(feedback), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/UpdateFeedback";//api controller name and its function

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


        }
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            Feedback feedback = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/GetFeedbackByid?feedbackId=" + feedbackId;//EmployeeId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Employeecontroller of api

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

        }
        [HttpPost]
        public async Task<IActionResult> DeleteFedback(Feedback feedback)
        {
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/DeleteFeedback?feedbackId=" + feedback.FeedbackId;  //api controller name and its function

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

        }
        public IActionResult GetAllFeedbacks()
        {
            return View();
        }


        [HttpGet]

        public async Task<IActionResult> GetAllFeedbacks(Feedback feedback)
        {
            IEnumerable<Feedback> employeeresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Feedback/GetFeedback";//api controller name and httppost name given inside httppost in moviecontroller of api

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
        }
    }
}

