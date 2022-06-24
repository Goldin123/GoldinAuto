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
            var shoppingItems = await shoppingCartRepository.GetShoppingCart();
            if (shoppingItems != null)
                return View(shoppingItems.Item1);

            return View(new List<ShoppingCart>());
        }

        [Authorize]
        public async Task<ActionResult> Purchase() 
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            var email = userClaims?.FindFirst("preferred_username")?.Value;
            var name = userClaims?.FindFirst("name")?.Value;
            if (!string.IsNullOrEmpty(email))
            {
                Customer customer = new Customer();
                Order order = new Order();
                List<ShoppingCart> shoppingCartItems = new List<ShoppingCart>();
                var getCustomer = await customerRepository.GetCustomer(email);
                if (getCustomer.Item1 != null)                 
                {
                    customer = getCustomer.Item1;

                }
                else 
                {
                    var names = name.Split(' ');
                    customer.FirstName = names[0];
                    customer.LastName = names[1];
                    customer.Email = email;
                    var addCustomer = await customerRepository.AddEditCustomer(customer);
                    if (addCustomer.Item1 != null)                     
                    {
                        customer = addCustomer.Item1;
                    }
                }

                var addOrder = await orderReposotiry.AddOrder(customer.CID);
                
                if (addOrder != null) 
                {
                    order = addOrder.Item1;

                }

                var getShoppingCart = await shoppingCartRepository.GetShoppingCart();

                if(getShoppingCart != null) 
                {
                    shoppingCartItems = getShoppingCart.Item1;
                    if(shoppingCartItems != null) 
                    {
                        if(shoppingCartItems.Count > 0) 
                        {
                            foreach(var shoppingCartItem in shoppingCartItems) 
                            {
                                var orderedProduct = new OrderProducts 
                                {
                                    OID = order.OID,
                                    PID = shoppingCartItem.PID,
                                    Quatity = shoppingCartItem.Quantity,
                                    TotalPrice = (decimal)(shoppingCartItem.UnitPrice * shoppingCartItem.Quantity)
                                };
                                var addOrderProduct = await orderReposotiry.AddOrderProducts(orderedProduct);

                            }
                        }
                    }


                }
            }

            return RedirectToAction("Success");
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