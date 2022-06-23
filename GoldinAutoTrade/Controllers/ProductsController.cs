using GoldinAutoTrade.Interface;
using GoldinAutoTrade.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GlodinAutoTradeModel.Models;

namespace GoldinAutoTrade.Controllers
{
    public class ProductsController : Controller
    {
        IProductRepository productRepository = new ProductRepository();
        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products  = await  productRepository.GetProducts();
            if (products != null)
                if (products.Item2)
                    return View(products.Item1);

            return View(new List<Product>());
        }
    }
}