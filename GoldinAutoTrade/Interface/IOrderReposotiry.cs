using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTrade.Interface
{
    public interface IOrderReposotiry
    {
        Task<Tuple<Order>> AddOrder(Order order);
        Task<Tuple<OrderProducts>> AddOrderProducts(OrderProducts orderProduct);
        Task<Tuple<List<OrderHistory>>> GetOrderHistory(int CID);

    }
}
