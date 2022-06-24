using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldinAutoTradeApi.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        bool IShoppingCartRepository.AddToCart(ShoppingCart shoppingCart)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                var shpcart = new EF.ShoppingCart 
                {
                    CID = shoppingCart.CID,
                    PID = shoppingCart.PID,
                    ProductName = shoppingCart.ProductName,
                    Quantity = shoppingCart.Quantity,
                    UnitPrice = (decimal)shoppingCart.UnitPrice
                };
                context.ShoppingCarts.Add(shpcart);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        List<ShoppingCart> IShoppingCartRepository.GetShoppingCart()
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.ShoppingCarts
                            select new ShoppingCart
                            {
                                PID = (int)a.PID,
                                CID = (int)a.CID,
                                ProductName = a.ProductName,
                                Quantity = (int)a.Quantity,
                                UnitPrice = (double)a.UnitPrice,
                                SCID = a.SCID

                            }).ToList<ShoppingCart>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ShoppingCart IShoppingCartRepository.GetShoppingCartProduct(int Id)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                return (from a in context.ShoppingCarts
                        where a.PID == Id
                        select new ShoppingCart
                        {
                             CID = (int)a.CID,
                             PID = (int)a.PID,
                             ProductName =a.ProductName,
                             Quantity  = (int)a.Quantity,
                             UnitPrice = (double)a.UnitPrice,
                             SCID = a.SCID
                            
                        }).FirstOrDefault();
            }
        }

        void IShoppingCartRepository.RemoveCartItem(int PID)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    var cartItem = context.ShoppingCarts.Where(a=>a.PID == PID).FirstOrDefault();
                    if(cartItem != null) 
                    {
                        context.ShoppingCarts.Remove(cartItem);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}