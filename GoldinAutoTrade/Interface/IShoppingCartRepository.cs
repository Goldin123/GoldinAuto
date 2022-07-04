using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTrade.Interface
{
    public interface IShoppingCartRepository
    {
        Task<Tuple<bool>> AddToCart(ShoppingCart shoppingCart);
        Task<Tuple<ShoppingCart>> GetProductInBag(int PID);
        Task<Tuple<List<ShoppingCart>>> GetShoppingCart(int CID);
        Task<Tuple<bool>> UpdateCart(ShoppingCart shoppingCart);

        Task<Tuple<ShoppingCart>> IncreaseShoppingCartProductItem(ShoppingCart shoppingCart);
        Task<Tuple<ShoppingCart>> DecreaseShoppingCartProductItem(ShoppingCart shoppingCart);
        Task<Tuple<ShoppingCart>> RemoveShoppingCartProductItem(ShoppingCart shoppingCart);


    }
}
