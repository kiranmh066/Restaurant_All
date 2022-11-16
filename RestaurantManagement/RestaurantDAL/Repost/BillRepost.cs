using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class BillRepost : IBillRepost
    {
        RestaurantDbContext _dbContext;
        public void AddBill(Bill bill)
        {
            _dbContext.tbl_Bill.Add(bill);
            _dbContext.SaveChanges();
        }

        public void DeleteBill(int billId)
        {
            var bill = _dbContext.tbl_Bill.Find(billId);
            _dbContext.tbl_Bill.Remove(bill);
            _dbContext.SaveChanges();
        }

        public Bill GetBillByHallTableId(int hallId)
        {
            List<Bill> bills = _dbContext.tbl_Bill.Include(obj => obj.Order.HallTable).ToList();

            foreach (var item in bills)
            {
                if (item.Order.HallTable.HallTableId == hallId)
                {
                    return item;
                }

            }
            return null;
        }

        public Bill GetBillById(int billId)
        {
            
            return _dbContext.tbl_Bill.Find(billId);

        }

        public IEnumerable<Bill> GetBills()
        {
            return _dbContext.tbl_Bill.Include(obj=>obj.Order.Food).ToList(); ;
        }

        public void UpdateBill(Bill bill)
        {
            _dbContext.Entry(bill).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
