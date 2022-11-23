using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class HallTableRepost:IHallTableRepost
    {
        RestaurantDbContext _dbContext;//default private

        public HallTableRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddHallTable(HallTable hallTable)
        {
            _dbContext.tbl_HallTable.Add(hallTable);
            _dbContext.SaveChanges();
        }

        public void DeleteHallTable(int hallTableId)
        {
            var hallTable = _dbContext.tbl_HallTable.Find(hallTableId);
            _dbContext.tbl_HallTable.Remove(hallTable);
            _dbContext.SaveChanges();
        }

        public HallTable GetHallTableById(int hallTableId)
        {
            return _dbContext.tbl_HallTable.Find(hallTableId);
        }

        public IEnumerable<HallTable> GetHallTables()
        {
            return _dbContext.tbl_HallTable.ToList();
        }



        public void UpdateHallTable(HallTable hallTable)
        {

            _dbContext.Entry(hallTable).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
