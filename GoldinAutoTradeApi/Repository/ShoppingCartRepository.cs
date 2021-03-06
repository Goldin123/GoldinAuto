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

        bool IShoppingCartRepository.UpdateCart(ShoppingCart shoppingCart) 
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                var shpcart = context.ShoppingCarts.Where(x => x.CID == shoppingCart.CID && x.PID== shoppingCart.PID).FirstOrDefault();
                if(shpcart != null) 
                {
                    shpcart.Quantity++;
                    context.SaveChanges();
                }
                return true;
            }
        }

        List<ShoppingCart> IShoppingCartRepository.GetShoppingCart(int CID)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.ShoppingCarts
                            join b in context.Products on a.PID equals b.PID
                            where a.CID == CID
                            select new ShoppingCart
                            {
                                PID = (int)a.PID,
                                CID = (int)a.CID,
                                ProductName = a.ProductName,
                                Quantity = (int)a.Quantity,
                                UnitPrice = (double)a.UnitPrice,
                                SCID = a.SCID,
                                ProductsAvailable = (int)b.UnitsInStock

                            }).ToList<ShoppingCart>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ShoppingCart IShoppingCartRepository.GetShoppingCartProduct(int CID,int PID)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                return (from a in context.ShoppingCarts
                        where a.PID == PID && a.CID == CID
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

        void IShoppingCartRepository.RemoveCartItem(int PID,int CID)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    var cartItem = context.ShoppingCarts.Where(a=>a.PID == PID  && a.CID==CID).FirstOrDefault();
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

        ShoppingCart IShoppingCartRepository.IncreaseShoppingCartProduct(ShoppingCart shoppingCart)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                var shpcart = context.ShoppingCarts.Where(x => x.CID == shoppingCart.CID && x.PID == shoppingCart.PID).FirstOrDefault();
                if (shpcart != null)
                {
                    shpcart.Quantity++;
                    context.SaveChanges();
                }
                var product = context.Products.Where(x => x.PID == shoppingCart.PID).FirstOrDefault();

                if(product != null) 
                {
                    product.UnitsInStock--;
                    context.SaveChanges();
                }

                return shoppingCart;
            }
        }

        ShoppingCart IShoppingCartRepository.DecreaseShoppingCartProduct(ShoppingCart shoppingCart)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                var shpcart = context.ShoppingCarts.Where(x => x.CID == shoppingCart.CID && x.PID == shoppingCart.PID).FirstOrDefault();
                if (shpcart != null)
                {
                    shpcart.Quantity--;
                    context.SaveChanges();
                }
                var product = context.Products.Where(x => x.PID == shoppingCart.PID).FirstOrDefault();

                if (product != null)
                {
                    product.UnitsInStock++;
                    context.SaveChanges();
                }

                return shoppingCart;
            }
        }

        ShoppingCart IShoppingCartRepository.RemoveShoppingCartProduct(ShoppingCart shoppingCart)
        {
            using (var context = new EF.GoldinAutoEntities())
            {
                var shpcart = context.ShoppingCarts.Where(x => x.CID == shoppingCart.CID && x.PID == shoppingCart.PID).FirstOrDefault();
                if (shpcart != null)
                {
                   context.ShoppingCarts.Remove(shpcart);
                    context.SaveChanges();
                }
                var product = context.Products.Where(x => x.PID == shoppingCart.PID).FirstOrDefault();

                if (product != null)
                {
                    product.UnitsInStock = product.UnitsInStock + shoppingCart.Quantity;
                    context.SaveChanges();
                }

                return shoppingCart;
            }
        }
    }
}