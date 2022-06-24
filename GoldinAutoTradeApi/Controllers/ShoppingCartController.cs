using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using GoldinAutoTradeApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoldinAutoTradeApi.Controllers
{
    public class ShoppingCartController : ApiController
    {
        IShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
        // GET: api/ShoppingCart

        [Route("api/ShoppingCart/GetShoppingCart")]
        [HttpGet]
        public IHttpActionResult GetShoppingCart()
        {
            try
            {
                var shoppingCarts = shoppingCartRepository.GetShoppingCart();
                return Ok(shoppingCarts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Customer
        [Route("api/ShoppingCart/AddShoppingCard")]
        [HttpPost]
        public IHttpActionResult AddShoppingCard(ShoppingCart shoppingCart)
        {
            try
            {
                var shopping = shoppingCartRepository.AddToCart(shoppingCart);
                return Ok(shopping);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ShoppingCart/GetShoppingCartProduct")]
        [HttpGet]
        public IHttpActionResult GetShoppingCartProduct(int Id)
        {
            try
            {
                var productInBag = shoppingCartRepository.GetShoppingCartProduct(Id);
                return Ok(productInBag);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
