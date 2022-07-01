using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GoldinAutoTradeApi.Inteface
{
    internal interface IProductRepository
    {
        List<Product> GetProducts();

        Product GetProduct(int id);
        Product UpdateProductInStock(int id);

        bool AddProduct(Product product,HttpPostedFile image);
    }
}
