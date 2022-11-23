using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class FoodService
    {
        IFoodRepost _foodRepository;


        //Unable to resolve ====>>>> Object issues
        public FoodService(IFoodRepost foodRepository)
        {
            _foodRepository = foodRepository;
        }


        //Update Food
        public void UpdateFood(Food food)
        {
            _foodRepository.UpdateFood(food);
        }

        //Delete Food
        public void DeleteFood(int foodId)
        {
            _foodRepository.DeleteFood(foodId);
        }

        //Get FoodByID
        public Food GetFoodById(int foodId)
        {
            return _foodRepository.GetFoodById(foodId);
        }

        //Get Foods
        public IEnumerable<Food> GetFood()
        {
            return _foodRepository.GetFoods();
        }

        //Registering Food
        public void AddFood(Food foodInfo)
        {
            _foodRepository.AddFood(foodInfo);
        }

     
    }
}
