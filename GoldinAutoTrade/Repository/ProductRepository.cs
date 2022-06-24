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

        async Task<Tuple<Product>> IProductRepository.GetProduct(int Id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Product/GetProduct?Id={Id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var product = JsonConvert.DeserializeObject<Product>(content);
                        return new Tuple<Product>(product);
                    }
                }
                return new Tuple<Product>(new Product());
            }
            catch (Exception ex)
            {
                return new Tuple<Product>(new Product());
            }
        }

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
                return new Tuple<List<Product>, bool>(new List<Product>(), false);
            }
        }

        async Task<Tuple<Product>> IProductRepository.UpdateProductInStock(int Id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Product/UpdateProductInStock?Id={Id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var product = JsonConvert.DeserializeObject<Product>(content);
                        return new Tuple<Product>(product);
                    }
                }
                return new Tuple<Product>(new Product());
            }
            catch (Exception ex)
            {
                return new Tuple<Product>(new Product());
            }
        }
    }
}