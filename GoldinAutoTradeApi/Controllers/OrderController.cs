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
            => Ok(orderRepository.AddOrder(order.CID));


        [Route("api/Order/AddOrderProduct")]
        [HttpPost]
        public IHttpActionResult AddOrderProduct(OrderProducts orderedProduct)
            => Ok(orderRepository.AddOrderProduct(orderedProduct));

        [Route("api/Order/OrderHistory")]
        [HttpGet]
        public IHttpActionResult OrderHistory(int CID)
            => Ok(orderRepository.GetOrderHistory(CID));

    }
}
