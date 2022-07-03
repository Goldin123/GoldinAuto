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
            =>Ok(shoppingCartRepository.GetShoppingCart(CID));


        [Route("api/ShoppingCart/AddShoppingCard")]
        [HttpPost]
        public IHttpActionResult AddShoppingCard(ShoppingCart shoppingCart)
            => Ok(shoppingCartRepository.AddToCart(shoppingCart));

        [Route("api/ShoppingCart/UpdateCart")]
        [HttpPost]
        public IHttpActionResult UpdateCart(ShoppingCart shoppingCart)
            => Ok(shoppingCartRepository.UpdateCart(shoppingCart));

        [Route("api/ShoppingCart/GetShoppingCartProduct")]
        [HttpGet]
        public IHttpActionResult GetShoppingCartProduct(int CID, int PID)
            =>Ok(shoppingCartRepository.GetShoppingCartProduct(CID, PID));

        [Route("api/ShoppingCart/IncreaseShoppingCartProductItem")]
        [HttpPost]
        public IHttpActionResult IncreaseShoppingCartProductItem(ShoppingCart shoppingCart)
            => Ok(shoppingCartRepository.IncreaseShoppingCartProduct(shoppingCart));


        [Route("api/ShoppingCart/DecreaseShoppingCartProductItem")]
        [HttpPost]
        public IHttpActionResult DecreaseShoppingCartProductItem(ShoppingCart shoppingCart)
            => Ok(shoppingCartRepository.DecreaseShoppingCartProduct(shoppingCart));


        [Route("api/ShoppingCart/RemoveShoppingCartProductItem")]
        [HttpPost]
        public IHttpActionResult RemoveShoppingCartProductItem(ShoppingCart shoppingCart)
            => Ok(shoppingCartRepository.RemoveShoppingCartProduct(shoppingCart));


    }
}
