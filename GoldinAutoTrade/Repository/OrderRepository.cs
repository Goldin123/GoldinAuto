using GlodinAutoTradeModel.Models;
using GoldinAutoTrade.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GoldinAutoTrade.Repository
{
    public class OrderRepository : IOrderReposotiry
    {
        private readonly HttpClient client = Helper.ApiHttpClient.GetApiClient();
        async Task<Tuple<Order>> IOrderReposotiry.AddOrder(Order order)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync($"api/Order/AddOrder",
                        new StringContent(JsonConvert.SerializeObject(order), System.Text.Encoding.Unicode, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var newOrder = JsonConvert.DeserializeObject<Order>(content);
                        return new Tuple<Order>(newOrder);
                    }
                }
                return new Tuple<Order>(null);
            }
            catch (Exception ex)
            {
                return new Tuple<Order>(null);
            }
        }
        async Task<Tuple<OrderProducts>> IOrderReposotiry.AddOrderProducts(OrderProducts orderProduct)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync($"api/Order/AddOrderProduct",
                        new StringContent(JsonConvert.SerializeObject(orderProduct), System.Text.Encoding.Unicode, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var orderProd = JsonConvert.DeserializeObject<OrderProducts>(content);
                        return new Tuple<OrderProducts>(orderProd);
                    }
                }
                return new Tuple<OrderProducts>(null);
            }
            catch (Exception ex)
            {
                return new Tuple<OrderProducts>(null);
            }
        }

        async Task<Tuple<List<OrderHistory>>> IOrderReposotiry.GetOrderHistory(int CID)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Order/OrderHistory?CID={CID}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var orderHistory = JsonConvert.DeserializeObject<List<OrderHistory>>(content);
                        return new Tuple<List<OrderHistory>>(orderHistory);
                    }
                }
                return new Tuple<List<OrderHistory>>(new List<OrderHistory>());
            }
            catch (Exception ex)
            {
                return new Tuple<List<OrderHistory>>(new List<OrderHistory>());
            }
        }
    }
}