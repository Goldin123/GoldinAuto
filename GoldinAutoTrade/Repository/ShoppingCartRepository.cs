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
    public class ShoppingCartRepository : IShoppongCartRepository
    {
        private readonly HttpClient client = Helper.ApiHttpClient.GetApiClient();

        async Task<Tuple<bool>> IShoppongCartRepository.AddToCart(ShoppingCart shoppingCart)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync($"api/ShoppingCart/AddShoppingCard",
                        new StringContent(JsonConvert.SerializeObject(shoppingCart), System.Text.Encoding.Unicode, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var products = JsonConvert.DeserializeObject<bool>(content);
                        return new Tuple<bool>(true);
                    }
                }
                return new Tuple<bool>(false);
            }
            catch (Exception ex)
            {
                return new Tuple<bool>(false);
            }
        }

       async Task<Tuple<ShoppingCart>> IShoppongCartRepository.GetProductInBag(int PID)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/ShoppingCart/GetShoppingCartProduct?CID={Globals.CID}&PID={PID}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var products = JsonConvert.DeserializeObject<ShoppingCart>(content);
                        return new Tuple<ShoppingCart>(products);
                    }
                }
                return new Tuple<ShoppingCart>(null);
            }
            catch (Exception ex)
            {
                return new Tuple<ShoppingCart>(null);
            }
        }

        async Task<Tuple<List<ShoppingCart>>> IShoppongCartRepository.GetShoppingCart(int CID)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/ShoppingCart/GetShoppingCart?CID={Globals.CID}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var products = JsonConvert.DeserializeObject<List<ShoppingCart>>(content);
                        return new Tuple<List<ShoppingCart>>(products);
                    }
                }
                return new Tuple<List<ShoppingCart>>(new List<ShoppingCart>());
            }
            catch (Exception ex)
            {
                return new Tuple<List<ShoppingCart>>(new List<ShoppingCart>());
            }
        }

        async Task<Tuple<bool>> IShoppongCartRepository.UpdateCart(ShoppingCart shoppingCart)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync($"api/ShoppingCart/UpdateCart",
                        new StringContent(JsonConvert.SerializeObject(shoppingCart), System.Text.Encoding.Unicode, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var products = JsonConvert.DeserializeObject<bool>(content);
                        return new Tuple<bool>(true);
                    }
                }
                return new Tuple<bool>(false);
            }
            catch (Exception ex)
            {
                return new Tuple<bool>(false);
            }
        }
    }
}