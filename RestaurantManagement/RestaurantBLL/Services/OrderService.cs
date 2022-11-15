using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class OrderService
    {
        IOrderRepost _orderRepository;


        //Unable to resolve ====>>>> Object issues
        public OrderService(IOrderRepost orderRepository)
        {
            _orderRepository = orderRepository;
        }


        //Update Order
        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }

        //Delete Order
        public void DeleteOrder(int orderId)
        {
            _orderRepository.DeleteOrder(orderId);
        }

        //Get OrderByID
        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        //Get Orders
        public IEnumerable<Order> GetOrder()
        {
            return _orderRepository.GetOrders();
        }

        //Registering Order
        public void AddOrder(Order orderInfo)
        {
            _orderRepository.AddOrder(orderInfo);
        }

        public IEnumerable<Order> GetOrderByHallId(int HallId)
        { 
           return _orderRepository.GetAllOrderByUserId(HallId);
        }
    }
}
