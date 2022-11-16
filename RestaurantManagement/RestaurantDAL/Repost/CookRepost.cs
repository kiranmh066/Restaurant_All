using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class CookRepost:ICookRepost
    {
        RestaurantDbContext _dbContext;//default private

        public CookRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddCook(Cook cook)
        {
            _dbContext.tbl_Cook.Add(cook);
            _dbContext.SaveChanges();
        }

        public void DeleteCook(int cookId)
        {
            var cook = _dbContext.tbl_Cook.Find(cookId);
            _dbContext.tbl_Cook.Remove(cook);
            _dbContext.SaveChanges();
        }

        public Cook GetCookById(int cookId)
        {
            return _dbContext.tbl_Cook.Find(cookId);
        }

        public IEnumerable<Cook> GetCooks()
        {
            return _dbContext.tbl_Cook.Include(obj=>obj.Employee).ToList();
        }

        public IEnumerable<Cook> GetCooksBySpeciality(string speciality)
        {
            throw new NotImplementedException();
        }

        public void UpdateCook(Cook cook)
        {

            _dbContext.Entry(cook).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
