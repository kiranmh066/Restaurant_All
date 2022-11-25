using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private BillService _billService;

        public BillController(BillService billService)
        {
            _billService = billService;
        }

        [HttpGet("GetBills")]
        public IEnumerable<Bill> GetBills()
        {
            return _billService.GetBill();
        }



        [HttpDelete("DeleteBill")]
        public IActionResult DeleteBill(int BillId)
        {
            _billService.DeleteBill(BillId);
            return Ok("Bill deleted Successfully");
        }

        [HttpPut("UpdateBill")]
        public IActionResult UpdateBill([FromBody] Bill Bill)
        {
            _billService.UpdateBill(Bill);
            return Ok("Bill Updated Successfully");
        }

        [HttpGet("GetBillById")]
        public Bill GetBillById(int BillId)
        {
            return _billService.GetBillById(BillId);
        }

        [HttpPost("AddBill")]
        public IActionResult AddBill(Bill BillInfo)
        {
            _billService.AddBill(BillInfo);
            return Ok("Register successfully!!");
        }

        [HttpGet("GetBillByHallTableId")]
        public Bill GetBillByHallTableId(int hallId)
        {
            return _billService.GetBillbyHallTableId(hallId);
        }


    }
}
