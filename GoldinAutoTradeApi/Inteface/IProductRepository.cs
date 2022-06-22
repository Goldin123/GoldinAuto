using GoldinAutoTradeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTradeApi.Inteface
{
    internal interface IProductRepository
    {
        List<Product> GetProducts();
    }
}
