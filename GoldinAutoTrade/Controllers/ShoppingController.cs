using GlodinAutoTradeModel.Models;
using GoldinAutoTrade.Interface;
using GoldinAutoTrade.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoldinAutoTrade.Controllers
{
    public class ShoppingController : Controller
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        IShoppongCartRepository shoppingCartRepository = new ShoppingCartRepository();
        IOrderReposotiry orderReposotiry = new OrderRepository();
        IProductRepository productRepository = new ProductRepository();

        [Authorize]
        // GET: Shopping
        public async Task<ActionResult> Index()
        {
            var shoppingItems = await shoppingCartRepository.GetShoppingCart(Globals.CID);
            if (shoppingItems.Item1 != null)
                return View(shoppingItems.Item1);

            return View(new List<ShoppingCart>());
        }

        [Authorize]
        public async Task<ActionResult> Purchase()
        {

            Customer customer = new Customer();
            Order order = new Order();
            order.CID = Globals.CID;
            List<ShoppingCart> shoppingCartItems = new List<ShoppingCart>();
            var getCustomer = await customerRepository.GetCustomer(Globals.Email);
            if (getCustomer.Item1 != null)
                customer = getCustomer.Item1;

            var addOrder = await orderReposotiry.AddOrder(order);

            if (addOrder.Item1 != null)
                order = addOrder.Item1;

            var getShoppingCart = await shoppingCartRepository.GetShoppingCart(Globals.CID);

            if (getShoppingCart.Item1 != null)
            {
                shoppingCartItems = getShoppingCart.Item1;
                if (shoppingCartItems != null)
                {
                    if (shoppingCartItems.Count > 0)
                    {
                        foreach (var shoppingCartItem in shoppingCartItems)
                        {
                            var orderedProduct = new OrderProducts
                            {
                                OID = order.OID,
                                PID = shoppingCartItem.PID,
                                CID = shoppingCartItem.CID,
                                Quatity = shoppingCartItem.Quantity,
                                TotalPrice = (decimal)(shoppingCartItem.UnitPrice * shoppingCartItem.Quantity)
                            };
                            var addOrderProduct = await orderReposotiry.AddOrderProducts(orderedProduct);

                        }
                    }
                }
                var shoppingCart = await shoppingCartRepository.GetShoppingCart(Globals.CID);
                if (shoppingCart.Item1 != null)
                    Globals.ShoppingCartItems = shoppingCart.Item1.Count();
                TempData["PaymentStatus"] = "Payment seccessfully processed.";
            }

            return RedirectToAction("Index");
        }

        public async Task<JsonResult> QuanityChange(int type, int pId)
        {

            var product = await shoppingCartRepository.GetProductInBag(pId);
            if (product.Item1 == null)
            {
                return Json(new { d = "0" });
            }
            ShoppingCart shoppingCartProduct = new ShoppingCart();
            shoppingCartProduct = product.Item1;

            var getProduct  = await productRepository.GetProduct(pId);
            Product actualProduct = getProduct.Item1;

            int quantity;
            // if type 0, decrease quantity
            // if type 1, increase quanity
            switch (type)
            {
                case 0:
                    shoppingCartProduct.Quantity--;
                    actualProduct.UnitsInStock++;
                    break;
                case 1:
                    shoppingCartProduct.Quantity++;
                    actualProduct.UnitsInStock--;
                    break;
                case -1:
                    actualProduct.UnitsInStock += shoppingCartProduct.Quantity;
                    shoppingCartProduct.Quantity = 0;
                    break;
                default:
                    return Json(new { d = "0" });
            }

            if (shoppingCartProduct.Quantity == 0)
            {
                //context.ShoppingCartDatas.Remove(product);
                quantity = 0;
            }
            else
            {
                quantity = shoppingCartProduct.Quantity;
            }
            //context.SaveChanges();

            return Json(new { d = quantity });
        }

        [HttpGet]
        public async Task<JsonResult> UpdateTotal()
        {
            decimal total;
            try
            {
                var getShoppingCart = await shoppingCartRepository.GetShoppingCart(Globals.CID);

                total = (decimal)getShoppingCart.Item1.Select(p => p.UnitPrice * p.Quantity).Sum();
            }
            catch (Exception) { total = 0; }

            return Json(new { d = String.Format("{0:c}", total) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> OrderHistory() 
        {
            var orderHistory = await orderReposotiry.GetOrderHistory();
            if (orderHistory != null)
                return View(orderHistory.Item1);

            return View(new List<OrderHistory>());
        }
    }
}