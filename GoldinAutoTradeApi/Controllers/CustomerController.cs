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
    public class CustomerController : ApiController
    {
        ICustomerRepository customerRepository = new CustomerRepository();

        // POST: api/Customer
        [Route("api/Customer/AddCustomer")]
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer customer)
            => Ok(customerRepository.AddCustomer(customer));


        [Route("api/Customer/EditCustomer")]
        [HttpPost]
        public IHttpActionResult EditCustomer(Customer customer)
            => Ok(customerRepository.EditCustomer(customer));
        
        [Route("api/Customer/GetCustomer")]
        [HttpGet]
        public IHttpActionResult GetCustomer(string email)
            => Ok(customerRepository.GetCustomer(email));
       

    }
}
