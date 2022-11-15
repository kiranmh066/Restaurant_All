using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class FoodRepost:IFoodRepost
    {
        RestaurantDbContext _dbContext;//default private

        public FoodRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddFood(Food food)
        {
            _dbContext.tbl_Food.Add(food);
            _dbContext.SaveChanges();
        }

        public void DeleteFood(int foodId)
        {
            var food = _dbContext.tbl_Food.Find(foodId);
            _dbContext.tbl_Food.Remove(food);
            _dbContext.SaveChanges();
        }

        public Food GetFoodById(int foodId)
        {
            return _dbContext.tbl_Food.Find(foodId);
        }

        public IEnumerable<Food> GetFoods()
        {
            return _dbContext.tbl_Food.ToList();
        }

      

        public void UpdateFood(Food food)
        {

            _dbContext.Entry(food).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
