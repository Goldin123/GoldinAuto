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
        ISupplierRepository supplierRepository = new SupplierRepository();
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

        [Authorize]
        public async Task<ActionResult> AddToCart(string ID )
        {
            int id = Convert.ToInt32(ID);
            var cart = new ShoppingCart();
            var getProduct = await productRepository.GetProduct(id);
            if (getProduct != null)
            {
                Product product = getProduct.Item1;
                if (product != null && product.UnitsInStock > 0)
                {
                    var getProductInBag = await shoppingCartRepository.GetProductInBag(id);
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
                        cart.CID = Globals.CID;
                    }
                    var addUpdateCart = await shoppingCartRepository.AddToCart(cart);
                    if(addUpdateCart != null) 
                    {
                        if (addUpdateCart.Item1) 
                        {
                            await productRepository.UpdateProductInStock(id);
                        }
                    }
                   
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> AddProduct()
        {
            var getSuppliers = await supplierRepository.GetSuppliers();
            if (getSuppliers.Item1 != null)
            {
                List<SelectListItem> options = new List<SelectListItem>();

                foreach (var supplier in getSuppliers.Item1)
                {
                    var option = new SelectListItem
                    {
                        Text = supplier.SupplierName,
                        Value = supplier.SID.ToString()
                    };

                    options.Add(option);
                }

                ViewBag.Suppliers = options;
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProductToInventory(Product product) 
        {
            if (ModelState.IsValid) 
            {
                var result =  await productRepository.AddProduct(product);
                if (result.Item1)
                {
                    var products = await productRepository.GetProducts();
                    if (products.Item1 != null)
                        Globals.TotalProducts = products.Item1.Count();

                    TempData["ProductSuccess"] = "product added successfully.";
                }
            }

            return RedirectToAction("AddProduct");
        }
    }
}