using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTradeApi.Inteface
{
    internal interface IShoppingCartRepository
    {
        bool AddToCart(ShoppingCart shoppingCart);
        bool UpdateCart(ShoppingCart shoppingCart);
        ShoppingCart GetShoppingCartProduct(int CID, int PID);
        List<ShoppingCart> GetShoppingCart(int CID);
        void RemoveCartItem(int PID);

    }
}
