using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldinAutoTradeApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        List<Product> IProductRepository.GetProducts()
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.Products
                            select new Product { 
                                PID  = a.PID,
                                Brand = a.Brand,
                                Category = a.Category,
                                Description = a.Description,
                               ProductName = a.ProductName, 
                               UnitPrice  = (decimal)a.UnitPrice,
                               UnitsInStock = (int)a.UnitsInStock
                            }).ToList<Product>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}