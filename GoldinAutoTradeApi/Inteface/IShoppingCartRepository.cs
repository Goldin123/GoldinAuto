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
        ShoppingCart GetShoppingCartProduct(int Id);
        List<ShoppingCart> GetShoppingCart();
        void RemoveCartItem(int PID);

    }
}
