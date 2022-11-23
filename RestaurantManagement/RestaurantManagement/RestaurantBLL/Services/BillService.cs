using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class BillService
    {
        IBillRepost _billRepost;
        public BillService(IBillRepost billRepost)
        {
            _billRepost = billRepost;
        }


        //Update Bill
        public void UpdateBill(Bill Bill)
        {
            _billRepost.UpdateBill(Bill);
        }

        //Delete Bill
        public void DeleteBill(int BillId)
        {
            _billRepost.DeleteBill(BillId);
        }

        //Get BillByID
        public Bill GetBillById(int BillId)
        {
            return _billRepost.GetBillById(BillId);
        }

        //Get Bills
        public IEnumerable<Bill> GetBill()
        {
            return _billRepost.GetBills();
        }

        //Registering Bill
        public void AddBill(Bill BillInfo)
        {
            _billRepost.AddBill(BillInfo);
        }


        public Bill GetBillbyHallTableId(int hallId)
        {
            return _billRepost.GetBillByHallTableId(hallId);
        }


    }
}
