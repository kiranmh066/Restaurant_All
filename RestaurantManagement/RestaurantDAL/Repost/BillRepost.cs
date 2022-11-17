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
<<<<<<< HEAD
        public BillRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
=======
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419
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
<<<<<<< HEAD
            List<Bill> bills = _dbContext.tbl_Bill.Include(obj=>obj.HallTable).ToList();

            foreach (var item in bills)
            {
                if (item.HallTableId == hallId)
=======
            List<Bill> bills = _dbContext.tbl_Bill.Include(obj => obj.Order.HallTable).ToList();

            foreach (var item in bills)
            {
                if (item.Order.HallTable.HallTableId == hallId)
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419
                {
                    return item;
                }

            }
            return null;
        }

        public Bill GetBillById(int billId)
        {
<<<<<<< HEAD

=======
            
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419
            return _dbContext.tbl_Bill.Find(billId);

        }

        public IEnumerable<Bill> GetBills()
        {
<<<<<<< HEAD
            return _dbContext.tbl_Bill.ToList(); ;
=======
            return _dbContext.tbl_Bill.Include(obj=>obj.Order.Food).ToList(); ;
>>>>>>> 2325327b6f06a37008972b3df8c79290e3c85419
        }

        public void UpdateBill(Bill bill)
        {
            _dbContext.Entry(bill).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
