using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrders")]//
        public IEnumerable<Order> GetOrders()
        {
            return _orderService.GetOrder();
        }



        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int orderId)
        {
            _orderService.DeleteOrder(orderId);
            return Ok("Order deleted Successfully");
        }

        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] Order order)
        {
            _orderService.UpdateOrder(order);
            return Ok("Order Updated Successfully");
        }

        [HttpGet("GetOrderById")]
        public Order GetOrderById(int orderId)
        {
            return _orderService.GetOrderById(orderId);
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody] Order orderInfo)
        {
            _orderService.AddOrder(orderInfo);
            return Ok("Register successfully!!");
        }

        [HttpGet("GetOrdersByHallById")]//
        public IEnumerable<Order> GetOrdersByHallId(int HallId)
        {
            return _orderService.GetOrderByHallId(HallId);
        }
    }
}
