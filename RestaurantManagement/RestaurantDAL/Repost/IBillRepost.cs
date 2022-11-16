using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
    public interface IBillRepost
    {
        void AddBill(Bill bill);
        void UpdateBill(Bill bill);

        void DeleteBill(int billId);

        Bill GetBillById(int billId);

        Bill GetBillByHallTableId(int hallId);

        IEnumerable<Bill> GetBills();
    }
}
