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
        //ICustomerRepository customerRepository = new CustomerRepository();
        //IShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
        //IOrderReposotiry orderReposotiry = new OrderRepository();

        readonly ICustomerRepository _customerRepository;
        readonly IShoppingCartRepository _shoppingCartRepository;
        readonly IOrderReposotiry _orderReposotiry;

        public ShoppingController(ICustomerRepository customerRepository, IShoppingCartRepository shoppingCartRepository, IOrderReposotiry orderReposotiry)
        {
            this._customerRepository = customerRepository;
            this._shoppingCartRepository = shoppingCartRepository;
            this._orderReposotiry = orderReposotiry;
        }


        [Authorize]
        // GET: Shopping
        public async Task<ActionResult> Index()
        {
            var shoppingItems = await _shoppingCartRepository.GetShoppingCart(Globals.CID);
            if (shoppingItems.Item1 != null)
            {
                Globals.ShoppingCartItems = shoppingItems.Item1.Count();
                return View(shoppingItems.Item1);
            }


            return View(new List<ShoppingCart>());
        }

        [Authorize]
        public async Task<ActionResult> Purchase()
        {

            Customer customer = new Customer();
            Order order = new Order();
            order.CID = Globals.CID;
            List<ShoppingCart> shoppingCartItems = new List<ShoppingCart>();
            var getCustomer = await _customerRepository.GetCustomer(Globals.Email);
            if (getCustomer.Item1 != null)
                customer = getCustomer.Item1;

            var addOrder = await _orderReposotiry.AddOrder(order);

            if (addOrder.Item1 != null)
                order = addOrder.Item1;

            var getShoppingCart = await _shoppingCartRepository.GetShoppingCart(Globals.CID);

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
                            var addOrderProduct = await _orderReposotiry.AddOrderProducts(orderedProduct);

                        }
                    }
                }
                var shoppingCart = await _shoppingCartRepository.GetShoppingCart(Globals.CID);
                if (shoppingCart.Item1 != null)
                    Globals.ShoppingCartItems = shoppingCart.Item1.Count();
                TempData["PaymentStatus"] = "Payment seccessfully processed.";
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> QuanityChange(int type, int pId)
        {

            var product = await _shoppingCartRepository.GetProductInBag(pId);
            if (product.Item1 == null)
            {
                return RedirectToAction("Index", "Shopping");
            }
            ShoppingCart shoppingCartProduct = new ShoppingCart();
            shoppingCartProduct = product.Item1;          
            
            switch (type)
            {
                case 0: //decrease
                    await _shoppingCartRepository.DecreaseShoppingCartProductItem(shoppingCartProduct);
                    break;
                case 1://increase
                    await _shoppingCartRepository.IncreaseShoppingCartProductItem(shoppingCartProduct);
                    break;
                case -1://remove
                    await _shoppingCartRepository.RemoveShoppingCartProductItem(shoppingCartProduct);
                    break;
                default:
                    return RedirectToAction("Index", "Shopping");
            }

            return RedirectToAction("Index","Shopping");
        }

        [Authorize]
        public async Task<ActionResult> OrderHistory() 
        {
            var orderHistory = await _orderReposotiry.GetOrderHistory(Globals.CID);
            if (orderHistory != null)
                return View(orderHistory.Item1);

            return View(new List<OrderHistory>());
        }
    }
}