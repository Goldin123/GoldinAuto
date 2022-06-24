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
                    var order = new EF.Order_Products
                    {
                       OID = orderedProduct.OID,
                       PID = orderedProduct.PID,
                       Quantity = orderedProduct.Quatity,
                       TotalPrice = orderedProduct.TotalPrice,
                    };

                    context.Order_Products.Add(order);
                    context.SaveChanges();

                    IShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
                    shoppingCartRepository.RemoveCartItem(orderedProduct.PID);
                    
                    return new OrderProducts
                    {
                       OID = order.OID,
                       PID = order.PID,
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

        List<OrderHistory> IOrderRepository.GetOrderHistory()
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.Order_Products
                            select new OrderHistory
                            {
                                DeliveryDate = (DateTime)a.Order.DeliveryDate,
                                OID = a.OID,
                                PID = a.PID,
                                OrderDate = (DateTime)a.Order.OrderDate,
                                ProductName = a.Product.ProductName,
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