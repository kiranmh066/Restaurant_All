using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class OrderRepost:IOrderRepost
    {

        RestaurantDbContext _dbContext;//default private

        public OrderRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddOrder(Order order)
        {
            _dbContext.tbl_Order.Add(order);
            _dbContext.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _dbContext.tbl_Order.Find(orderId);
            _dbContext.tbl_Order.Remove(order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrderByUserId(int hallId)
        {
            List<Order>orderlistbyuser=new List<Order>();
            
            orderlistbyuser = _dbContext.tbl_Order.ToList();
            List<Order> list = new List<Order>();

            foreach (var item in orderlistbyuser)
            {
                if (hallId == item.HallTableId)
                {
                     list.Add(item);
                }

            }


            return list;


        }
    

        public Order GetOrderById(int orderId)
        {
            return _dbContext.tbl_Order.Find(orderId);
        }

        
        public IEnumerable<Order> GetOrders()
        {
            return _dbContext.tbl_Order.ToList();
        }



        public void UpdateOrder(Order order)
        {

            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
