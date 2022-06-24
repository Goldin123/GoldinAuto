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
        Product IProductRepository.GetProduct(int id)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.Products
                            where a.PID == id
                            select new Product
                            {
                                PID = a.PID,
                                Brand = a.Brand,
                                Category = a.Category,
                                Description = a.Description,
                                ProductName = a.ProductName,
                                UnitPrice = (decimal)a.UnitPrice,
                                UnitsInStock = (int)a.UnitsInStock
                            }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<Product> IProductRepository.GetProducts()
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.Products
                            select new Product
                            {
                                PID = a.PID,
                                Brand = a.Brand,
                                Category = a.Category,
                                Description = a.Description,
                                ProductName = a.ProductName,
                                UnitPrice = (decimal)a.UnitPrice,
                                UnitsInStock = (int)a.UnitsInStock
                            }).ToList<Product>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Product IProductRepository.UpdateProductInStock(int id)
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    var product = context.Products.Where(a => a.PID == id).SingleOrDefault();

                    if (product != null)
                    {
                        product.UnitsInStock--;
                        context.SaveChanges();
                    }
                    return new Product()
                    {
                        PID = id,
                        Brand = product.Brand,
                        ProductName = product.ProductName,
                        UnitsInStock = (int)product.UnitsInStock,
                        Category = product.Category,
                        Description = product.Description,
                        UnitPrice = (decimal)product.UnitPrice
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}