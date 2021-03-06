using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTrade.Interface
{
    public interface ICustomerRepository
    {
        Task<Tuple<Customer>> AddCustomer(Customer customer);
        Task<Tuple<Customer>> EditCustomer(Customer customer);

        Task<Tuple<Customer>> GetCustomer(string email);
        Task<Tuple<bool>> SetGlobalVariable(System.Security.Claims.ClaimsIdentity claims);

    }
}
