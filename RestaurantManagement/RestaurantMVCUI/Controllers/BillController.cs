using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Linq;

namespace RestaurantMVCUI.Controllers
{
    public class BillController : Controller
    {
        private IConfiguration _configuration;
        public BillController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            #region Index of Bill Checks whether Ordered Anything
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();

            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByTableId?hallTableId=" + hallTableId1;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        orderresult = JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
                    }
                }
            }
            int k = orderresult.Count();
            if (k ==0)
            {
                ViewBag.status = "Error";
                ViewBag.message = "Not Ordered Anything Anything Get Bill";
                return RedirectToAction("GetOrders1", "Order");
            }
            else
            {
                return View();
            }
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Index(Bill bill)
        {
            #region Index of Bill Getting Order By Table id and Adding Bill to Database     
            Bill bill1 = new Bill();
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();

            IEnumerable<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByTableId?hallTableId=" + hallTableId1;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        orderresult = JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
                    }
                }
            }
            double total1 = 0;
            foreach (var item in orderresult)
            {
                total1 += item.OrderTotal;
            }
            ViewBag.status = "";

            bill1.BillStatus = false;
            bill1.HallTableId = hallTableId1;
            bill1.BillTotal = total1;
            bill1.BillDate = DateTime.Now;
            bill1.UserName = bill.UserName;
            bill1.UserEmail = bill.UserEmail;            

            TempData["UserEmail"] = bill.UserEmail;
            TempData["UserName"] = bill.UserName;
            TempData["BillDate"] = bill1.BillDate;
            TempData["BillStatus"] = bill1.BillStatus;         

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(bill1), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Bill/AddBill";
                //api controller name and its function
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        /*ViewBag.status = "Ok";
                        ViewBag.message = "Bill Paid Successfull!!";*/
                        return RedirectToAction("GenerateBill","Bill");
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }

                }
            }
            /*else
            {
                ViewBag.status = "Error";
                ViewBag.message = "Not Ordered Anything Anything Get Bill";
            }*/
            return View();
            #endregion
        }
        public async Task<IActionResult> paymentgateway(int BillId)
        {
            #region getting Bills of The Latest user in the payment Oage
            int num = 0;
            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();

            IEnumerable<Bill> billresult = null;
            List<Bill>billatest=new List<Bill>();
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Bill/GetBills";
                //api controller name and httppost name given inside httppost in moviecontroller of api
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        billresult = JsonConvert.DeserializeObject<IEnumerable<Bill>>(result);
                    }
                }
            }
            foreach(var item in billresult)
            {
                if(hallTableId1==item.HallTableId)
                {
                    billatest.Add(item);
                    num++;
                }
            }
            return View(billatest[num-1]);
            #endregion
        }
        public async Task<IActionResult> GenerateBill()
        {
            #region Genrating Bill for the User
            List<Bill> billlist = new List<Bill>();
            Bill bill = new Bill();
            bill.UserName =Convert.ToString(TempData["UserName"]);
            bill.UserEmail= Convert.ToString(TempData["UserEmail"]);
            bill.BillDate=Convert.ToDateTime(TempData["BillDate"]);
            bill.BillStatus =Convert.ToBoolean(TempData["BillStatus"]);
            bill.HallTableId= Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();
            billlist.Add(bill);

            int hallTableId1 = Convert.ToInt32(TempData["halltableuserid"]);
            TempData.Keep();
            List<Order> orderresult = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Order/GetOrdersByTableId?hallTableId=" + hallTableId1;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        orderresult = JsonConvert.DeserializeObject<List<Order>>(result);
                    }                    
                }
            }
            var tupeluser = new Tuple<List<Bill>, List<Order>>(billlist, orderresult);
            return View(tupeluser);
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> GenerateBill(Bill bill)
        {
            #region Displaying the message of bill paid Succcessfully
            ViewBag.status = "Ok";
            ViewBag.message = "Bill Paid Successfully!!";
            return View();
            #endregion
        }

        public IActionResult BillSuccess()
        {
            #region Bill paid Page (After payment)
            if (true)
            {
                ViewBag.status = "Ok";
                ViewBag.message = "Bill Paid Successfully!!";
            }
            else
            {
                ViewBag.status = "Error";
                ViewBag.message = "Could Not Initiate Payment..!!!";
            }
            return View();
            #endregion
        }
    }
}