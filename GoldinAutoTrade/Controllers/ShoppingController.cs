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

        [Authorize]
        public async Task<ActionResult> Success()=> View();

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