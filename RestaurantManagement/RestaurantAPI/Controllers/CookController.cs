using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookController : ControllerBase
    {
        private CookService _cookService;

        public CookController(CookService cookService)
        {
            _cookService = cookService;
        }

        [HttpGet("GetCooks")]//
        public IEnumerable<Cook> GetCooks()
        {
            return _cookService.GetCook();
        }



        [HttpDelete("DeleteCook")]
        public IActionResult DeleteCook(int cookId)
        {
            _cookService.DeleteCook(cookId);
            return Ok("Cook deleted Successfully");
        }

        [HttpPut("UpdateCook")]
        public IActionResult UpdateCook([FromBody] Cook cook)
        {
            _cookService.UpdateCook(cook);
            return Ok("Cook Updated Successfully");
        }

        [HttpGet("GetCookById")]
        public Cook GetCookById(int cookId)
        {
            return _cookService.GetCookById(cookId);
        }

        [HttpGet("GetCookBySpeciality")]
        public IEnumerable<Cook> GetCookBySpeciality(string speciality)
        {
            return _cookService.GetCookBySpeciality(speciality);
        }

        [HttpPost("AddCook")]
        public IActionResult AddCook([FromBody] Cook cookInfo)
        {
            _cookService.AddCook(cookInfo);
            return Ok("Register successfully!!");
        }
    }
}
