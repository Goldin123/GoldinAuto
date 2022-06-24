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
        IShoppongCartRepository shoppingCartRepository = new ShoppingCartRepository();
        // GET: Products
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var products = await productRepository.GetProducts();
            if (products != null)
                if (products.Item2)
                    return View(products.Item1);

            return View(new List<Product>());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToCart(Product products)
        {
            //int ID = Convert.ToInt32(id);
            var cart = new ShoppingCart();
            var getProduct = await productRepository.GetProduct(products.PID);
            if (getProduct != null)
            {
                Product product = getProduct.Item1;
                if (product != null && product.UnitsInStock > 0)
                {
                    var getProductInBag = await shoppingCartRepository.GetProductInBag(products.PID);
                    if (getProductInBag.Item1 != null)
                    {
                        cart = getProductInBag.Item1;
                    }
                    else
                    {
                        cart.ProductName = product.ProductName;
                        cart.PID = product.PID;
                        cart.UnitPrice = (double)product.UnitPrice;
                        cart.Quantity = 1;
                        cart.CID = 1;
                    }
                    var addUpdateCart = await shoppingCartRepository.AddToCart(cart);
                    if(addUpdateCart != null) 
                    {
                        if (addUpdateCart.Item1) 
                        {
                            var updateProduct = await productRepository.UpdateProductInStock(products.PID);
                        }
                    }
                   
                }
            }

            return RedirectToAction("Index");
        }
    }
}