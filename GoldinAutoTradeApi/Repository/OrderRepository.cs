using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldinAutoTradeApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        
        Order IOrderRepository.AddOrder(int CID)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    var order = new EF.Order
                    {
                       CID = CID,
                       DeliveryDate = DateTime.Now.AddDays(5),
                       OrderDate = DateTime.Now,
                       
                    };

                    context.Orders.Add(order);
                    context.SaveChanges();

                    return new Order 
                    {
                        CID = CID,
                        OID = order.OID,
                        DeliveryDate = (DateTime)order.DeliveryDate,
                        OrderDate = (DateTime)order.OrderDate 
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        OrderProducts IOrderRepository.AddOrderProduct(OrderProducts orderedProduct)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    var order = new EF.Order_Product
                    {
                       OID = orderedProduct.OID,
                       PID = orderedProduct.PID,
                       Quantity = orderedProduct.Quatity,
                       TotalPrice = orderedProduct.TotalPrice,
                    };

                    context.Order_Product.Add(order);
                    context.SaveChanges();

                    IShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
                    shoppingCartRepository.RemoveCartItem(orderedProduct.PID,orderedProduct.CID);
                    
                    return new OrderProducts
                    {
                       OID = (int)order.OID,
                       PID = (int)order.PID,
                       OPID = order.OPID,
                       Quatity = (int)order.Quantity,
                       TotalPrice = (decimal)order.TotalPrice
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<OrderHistory> IOrderRepository.GetOrderHistory(int CID)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.Order_Product
                            join b in context.Orders on a.OID equals b.OID
                            join c in context.Products on a.PID equals c.PID
                            where b.CID == CID
                            select new OrderHistory
                            {
                                DeliveryDate = (DateTime)b.DeliveryDate,
                                OID = b.OID,
                                PID = c.PID,
                                OrderDate = (DateTime)b.OrderDate,
                                ProductName = c.ProductName,
                                Quantity = (int)a.Quantity,
                                TotalPrice = (decimal)a.TotalPrice

                            }).ToList<OrderHistory>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}