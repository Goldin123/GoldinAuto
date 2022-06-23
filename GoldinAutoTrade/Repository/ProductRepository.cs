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
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient client = Helper.ApiHttpClient.GetApiClient();
        async Task<Tuple<List<Product>, bool>> IProductRepository.GetProducts()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Product/GetProducts");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var products = JsonConvert.DeserializeObject<List<Product>>(content);
                        return new Tuple<List<Product>, bool>(products, true);
                    }
                }
                return new Tuple<List<Product>, bool>(new List<Product>(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}