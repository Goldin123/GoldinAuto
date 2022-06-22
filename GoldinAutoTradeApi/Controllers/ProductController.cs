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
    public class ProductController : ApiController
    {
        IProductRepository productRepository = new ProductRepository();
        [Route("api/Product/GetProducts")]
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            try
            {
                var products = productRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
