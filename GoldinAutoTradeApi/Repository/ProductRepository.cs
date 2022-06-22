using GoldinAutoTradeApi.Inteface;
using GoldinAutoTradeApi.Models;
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
                            select new Models.Product { 
                                PID  = a.PID,
                                Brand = a.Brand,
                                Category = a.Category,
                                Description = a.Description,
                               ProductName = a.ProductName, 
                               UnitPrice  = (decimal)a.UnitPrice,
                               UnitsInStock = (int)a.UnitsInStock
                            }).ToList<Models.Product>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}