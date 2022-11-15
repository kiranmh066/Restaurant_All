using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private FoodService _foodService;

        public FoodController(FoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("GetFoods")]//
        public IEnumerable<Food> GetFoods()
        {
            return _foodService.GetFood();
        }



        [HttpDelete("DeleteFood")]
        public IActionResult DeleteFood(int foodId)
        {
            _foodService.DeleteFood(foodId);
            return Ok("Food deleted Successfully");
        }

        [HttpPut("UpdateFood")]
        public IActionResult UpdateFood([FromBody] Food food)
        {
            _foodService.UpdateFood(food);
            return Ok("Food Updated Successfully");
        }

        [HttpGet("GetFoodById")]
        public Food GetFoodById(int foodId)
        {
            return _foodService.GetFoodById(foodId);
        }

        [HttpPost("AddFood")]
        public IActionResult AddFood([FromBody] Food foodInfo)
        {
            _foodService.AddFood(foodInfo);
            return Ok("Register successfully!!");
        }
       
    }
}
