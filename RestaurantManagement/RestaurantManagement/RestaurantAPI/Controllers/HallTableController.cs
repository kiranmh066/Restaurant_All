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
            return _hallTableService.GetHallTable();
        }



        [HttpDelete("DeleteHallTable")]
        public IActionResult DeleteHallTable(int hallTableId)
        {
            _hallTableService.DeleteHallTable(hallTableId);
            return Ok("HallTable deleted Successfully");
        }

        [HttpPut("UpdateHallTable")]
        public IActionResult UpdateHallTable([FromBody] HallTable hallTable)
        {
            _hallTableService.UpdateHallTable(hallTable);
            return Ok("HallTable Updated Successfully");
        }

        [HttpGet("GetHallTableById")]
        public HallTable GetHallTableById(int hallTableId)
        {
            return _hallTableService.GetHallTableById(hallTableId);
        }

        [HttpPost("AddHallTable")]
        public IActionResult AddHallTable([FromBody] HallTable hallTableInfo)
        {
            _hallTableService.AddHallTable(hallTableInfo);
            return Ok("Register successfully!!");
        }
    }
}
