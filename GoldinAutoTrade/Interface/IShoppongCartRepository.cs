using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTrade.Interface
{
    internal interface IShoppongCartRepository
    {
        Task<Tuple<bool>> AddToCart(ShoppingCart shoppingCart);
        Task<Tuple<ShoppingCart>> GetProductInBag(int Id);
        Task<Tuple<List<ShoppingCart>>> GetShoppingCart();

        
    }
}
