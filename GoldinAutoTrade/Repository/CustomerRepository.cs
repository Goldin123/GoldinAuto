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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HttpClient client = Helper.ApiHttpClient.GetApiClient();
        async Task<Tuple<Customer>> ICustomerRepository.AddEditCustomer(Customer customer)
        {
            try
            {
                customer.Address1 = "1 street";
                customer.Address2 = "2 street";
                customer.Address3 = "3 street";
                customer.Address4 = "4 street";
                customer.CardType = "Cheque";
                customer.CardNumber = "1234567987";
                customer.ExpiryDate = DateTime.Now.AddYears(10);

                HttpResponseMessage response = await client.PostAsync($"api/Customer/AddEditCustomer",
                        new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.Unicode, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var cust = JsonConvert.DeserializeObject<Customer>(content);
                        return new Tuple<Customer>(cust);
                    }
                }
                return new Tuple<Customer>(null);
            }
            catch (Exception ex)
            {
                return new Tuple<Customer>(null);
            }
        }

        private async Task<Tuple<Customer>> GetCustomer(string email)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Customer/GetCustomer?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var customer = JsonConvert.DeserializeObject<Customer>(content);
                        return new Tuple<Customer>(customer);
                    }
                }
                return new Tuple<Customer>(null);
            }
            catch (Exception ex)
            {
                return new Tuple<Customer>(null);
            }
        }

        async Task<Tuple<Customer>> ICustomerRepository.GetCustomer(string email)
        {
            return await GetCustomer(email);
        }

        async Task<Tuple<bool>> ICustomerRepository.SetGlobalVariable(System.Security.Claims.ClaimsIdentity claims)
        {
            Globals.Name = claims?.FindFirst("name")?.Value;
            Globals.Email = claims?.FindFirst("preferred_username")?.Value;
            Globals.Subject = claims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Globals.TenantId = claims?.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value;
            var getCustomerDetails = await GetCustomer(Globals.Email);
            if (getCustomerDetails.Item1 != null)
                Globals.CID = getCustomerDetails.Item1.CID;

            return new Tuple<bool>(true);
        }



    }
}