using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTradeApi.Inteface
{
    internal interface ICustomerRepository
    {
        Customer AddCustomer(Customer customer);
        Customer EditCustomer(Customer customer);
        Customer GetCustomer(string email);
    }
}
