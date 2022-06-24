using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTrade.Interface
{
    internal interface IOrderReposotiry
    {
        Task<Tuple<Order>> AddOrder(int CID);
        Task<Tuple<OrderProducts>> AddOrderProducts(OrderProducts orderProduct);
        Task<Tuple<List<OrderHistory>>> GetOrderHistory();

    }
}
