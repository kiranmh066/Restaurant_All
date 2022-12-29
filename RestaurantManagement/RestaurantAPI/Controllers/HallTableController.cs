using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallTableController : ControllerBase
    {
        private HallTableService _hallTableService;

        public HallTableController(HallTableService hallTableService)
        {
            _hallTableService = hallTableService;
        }

        [HttpGet("GetHallTables")]//
        public IEnumerable<HallTable> GetHallTables()
        {
            #region Function for Updating the HallTable 

            return _hallTableService.GetHallTable();
            #endregion
        }



        [HttpDelete("DeleteHallTable")]
        public IActionResult DeleteHallTable(int hallTableId)
        {
            #region Function for Delete the HallTable by its hallTableid
            try
            {
                _hallTableService.DeleteHallTable(hallTableId);
                return Ok("HallTable deleted Successfully");
            }
            catch
            {
                return BadRequest(400);
            }

            #endregion
        }

        [HttpPut("UpdateHallTable")]
        public IActionResult UpdateHallTable([FromBody] HallTable hallTable)
        {
            #region Function for Updating the HAllTable by its object
            try
            {
                _hallTableService.UpdateHallTable(hallTable);
                return Ok("HallTable Updated Successfully");
            }
            catch
            {
                return BadRequest(400);
            }

            #endregion
        }

        [HttpGet("GetHallTableById")]
        public HallTable GetHallTableById(int hallTableId)
        {
            #region Function for Updating the HallTable by its hallTableId

            return _hallTableService.GetHallTableById(hallTableId);
            #endregion
        }

        [HttpPost("AddHallTable")]
        public IActionResult AddHallTable([FromBody] HallTable hallTableInfo)
        {
            #region Function for Adding the HallTable by its object
            try
            {
                if (ModelState.IsValid)
                {
                    _hallTableService.AddHallTable(hallTableInfo);
                    return Ok("Register successfully!!");
                }
                else
                {
                    return BadRequest(400);
                }
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }
    }
}
