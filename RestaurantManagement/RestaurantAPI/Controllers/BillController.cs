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
            #region Function for Getting all the Bill
            
            return _billService.GetBill();
            #endregion
        }



        [HttpDelete("DeleteBill")]
        public IActionResult DeleteBill(int BillId)
        {
            #region Function for Deleting  the Bill given by the user to the hallmanager by its Bill id

            _billService.DeleteBill(BillId);
            return Ok("Bill deleted Successfully");
            #endregion
        }

        [HttpPut("UpdateBill")]
        public IActionResult UpdateBill([FromBody] Bill Bill)
        {
            #region Function for Deleting  the Bill given by the user to the hallmanager by its object 

            _billService.UpdateBill(Bill);
            return Ok("Bill Updated Successfully");
            #endregion
        }

        [HttpGet("GetBillById")]
        public Bill GetBillById(int BillId)
        {
            #region Function for getting the Bill given by the user to the hallmanager by its Billid

            return _billService.GetBillById(BillId);
            #endregion
        }

        [HttpPost("AddBill")]
        public IActionResult AddBill(Bill BillInfo)
        {
            #region Function for Adding the Bill given by the user to the hallmanager by its object 


            _billService.AddBill(BillInfo);
            return Ok("Register successfully!!");
            #endregion
        }

        [HttpGet("GetBillByHallTableId")]
        public Bill GetBillByHallTableId(int hallId)
        {
            #region Function for getting  the HallTable given by the user to the hallmanager by its hallid

            return _billService.GetBillbyHallTableId(hallId);

            #endregion
        }


    }
}
