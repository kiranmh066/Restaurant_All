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
            #region Function for Geeting the Order 
            return _orderService.GetOrder();
            #endregion
        }



        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int orderId)
        {
            #region Function for Delete the Order by its orderId

            _orderService.DeleteOrder(orderId);
            return Ok("Order deleted Successfully");
            #endregion
        }

        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] Order order)
        {
            #region Function for Updating the Order by its object
            _orderService.UpdateOrder(order);
            return Ok("Order Updated Successfully");
            #endregion
        }

        [HttpGet("GetOrderById")]
        public Order GetOrderById(int orderId)
        {
            #region Function for Getting the Order by its orderId
            return _orderService.GetOrderById(orderId);
            #endregion
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody] Order orderInfo)
        {
            #region Function for Adding the Order by its object

            _orderService.AddOrder(orderInfo);
            return Ok("Register successfully!!");
            #endregion
        }

        [HttpGet("GetOrdersByHallById")]//
        public IEnumerable<Order> GetOrdersByHallId(int HallId)
        {
            #region Function for Getting the Order by its HallID
            return _orderService.GetOrderByHallId(HallId);
            #endregion
        }

        [HttpGet("GetOrdersByTableId")]//
        public IEnumerable<Order> GetOrdersByTableId(int hallTableId)
        {
            #region Function for Getting the order by its hallTableId

            return _orderService.GetOrdersByTableId(hallTableId);
            #endregion
        }
    }
}
