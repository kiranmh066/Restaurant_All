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
            #region Function for geeting the Food 

            return _foodService.GetFood();
            #endregion
        }



        [HttpDelete("DeleteFood")]
        public IActionResult DeleteFood(int foodId)

        {
            #region  Function for deleting the Food by its foodId.
            try
            {
                _foodService.DeleteFood(foodId);
                return Ok("Food deleted Successfully");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }

        [HttpPut("UpdateFood")]
        public IActionResult UpdateFood([FromBody] Food food)
        {
            #region Function for Updating the Food by its object
            try
            {
                _foodService.UpdateFood(food);
                return Ok("Food Updated Successfully");
            }
            catch
            {
                return BadRequest(400);
            }
            #endregion
        }

        [HttpGet("GetFoodById")]
        public Food GetFoodById(int foodId)
        {
            #region Function for geeting the Food by its foodId

            return _foodService.GetFoodById(foodId);
            #endregion
        }

        [HttpPost("AddFood")]
        public IActionResult AddFood([FromBody] Food foodInfo)
        {
            #region Function for Adding the Food by its foodId
            try
            {
                _foodService.AddFood(foodInfo);
                return Ok("Register successfully!!");
            }
            catch
            {
                return BadRequest(400);
            }

            #endregion
        }

    }
}
