using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using GoldinAutoTradeApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoldinAutoTradeApi.Controllers
{
    public class OrderController : ApiController
    {

        IOrderRepository orderRepository = new OrderRepository();
        [Route("api/Order/AddOrder")]
        [HttpPost]
        public IHttpActionResult AddOrder(Order order)
        {
            try
            {
                var newOrder = orderRepository.AddOrder(order.CID);
                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Order/AddOrderProduct")]
        [HttpPost]
        public IHttpActionResult AddOrderProduct(OrderProducts orderedProduct)
        {
            try
            {
                var orderProduct = orderRepository.AddOrderProduct(orderedProduct);
                return Ok(orderProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/Order/OrderHistory")]
        [HttpGet]
        public IHttpActionResult OrderHistory() 
        {
            try
            {
                var orderHistory = orderRepository.GetOrderHistory();
                return Ok(orderHistory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
