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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly HttpClient client = Helper.ApiHttpClient.GetApiClient();
        async Task<Tuple<List<Supplier>>> ISupplierRepository.GetSuppliers()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Supplier/GetSuppliers");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(content);
                        return new Tuple<List<Supplier>>(suppliers);
                    }
                }
                return new Tuple<List<Supplier>>(new List<Supplier>());
            }
            catch (Exception ex)
            {
                return new Tuple<List<Supplier>>(new List<Supplier>());
            }
        }
    }
}