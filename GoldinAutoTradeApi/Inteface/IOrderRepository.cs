using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTradeApi.Inteface
{
    internal interface IOrderRepository
    {
        Order AddOrder(int CID);
        OrderProducts AddOrderProduct(OrderProducts orderedProduct);
        List<OrderHistory> GetOrderHistory(int CID);
    }
}
