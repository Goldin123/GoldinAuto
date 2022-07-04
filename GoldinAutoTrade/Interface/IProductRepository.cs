using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTrade.Interface
{
    public interface IProductRepository
    {
        Task<Tuple<List<Product>, bool>> GetProducts();

        Task<Tuple<Product>> GetProduct(int Id);

        Task<Tuple<Product>> UpdateProductInStock(int Id);

        Task<Tuple<bool>> AddProduct(Product product);
    }
}
