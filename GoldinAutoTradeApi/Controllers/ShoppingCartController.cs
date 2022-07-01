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
        public IHttpActionResult GetShoppingCart(int CID)
        {
            try
            {
                var shoppingCarts = shoppingCartRepository.GetShoppingCart(CID);
                return Ok(shoppingCarts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

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

        [Route("api/ShoppingCart/UpdateCart")]
        [HttpPost]
        public IHttpActionResult UpdateCart(ShoppingCart shoppingCart)
        {
            try
            {
                var shopping = shoppingCartRepository.UpdateCart(shoppingCart);
                return Ok(shopping);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ShoppingCart/GetShoppingCartProduct")]
        [HttpGet]
        public IHttpActionResult GetShoppingCartProduct(int CID, int PID)
        {
            try
            {
                var productInBag = shoppingCartRepository.GetShoppingCartProduct(CID,PID);
                return Ok(productInBag);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
