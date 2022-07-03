using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using GoldinAutoTradeApi.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GoldinAutoTradeApi.Controllers
{
    public class ProductController : ApiController
    {
        IProductRepository productRepository = new ProductRepository();
        [Route("api/Product/GetProducts")]
        [HttpGet]
        public IHttpActionResult GetProducts()
            => Ok(productRepository.GetProducts());
       

        [Route("api/Product/GetProduct")]
        [HttpGet]
        public IHttpActionResult GetProduct(int Id)
            => Ok(productRepository.GetProduct(Id));
        

        [Route("api/Product/UpdateProductInStock")]
        [HttpGet]
        public IHttpActionResult UpdateProductInStock(int Id)
            =>Ok(productRepository.UpdateProductInStock(Id));
       

        [Route("api/Product/AddProduct")]
        [HttpPut]
        public IHttpActionResult AddProduct()
        {
            var httpContext = HttpContext.Current;

            try
            {
                // Check for any uploaded file  
                if (httpContext.Request.Files.Count == 1)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[0];
                    if (httpPostedFile != null)
                    {
                        var product = JsonConvert.DeserializeObject<Product>(httpContext.Request.Form["Product"]);

                        productRepository.AddProduct(product, httpPostedFile);
                    }
                }
                else
                {
                    return InternalServerError();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
