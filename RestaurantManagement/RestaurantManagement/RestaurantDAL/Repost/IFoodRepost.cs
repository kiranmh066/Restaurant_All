using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
    public interface IFoodRepost
    {
        void AddFood(Food food);
        void UpdateFood(Food food);

        void DeleteFood(int foodId);

        Food GetFoodById(int foodId);

        IEnumerable<Food> GetFoods();
           
       
    }
}
