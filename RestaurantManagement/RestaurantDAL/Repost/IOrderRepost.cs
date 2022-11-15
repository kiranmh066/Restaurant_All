using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
    public interface IOrderRepost
    {
        void AddOrder(Order order);
        void UpdateOrder(Order order);

        void DeleteOrder(int orderId);

        Order GetOrderById(int orderId);

        IEnumerable<Order> GetOrders();

        IEnumerable<Order> GetAllOrderByUserId(int userId);
    }
}
