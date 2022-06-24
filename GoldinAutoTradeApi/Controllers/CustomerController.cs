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
        [Route("api/Customer/AddEditCustomer")]
        [HttpPost]
        public IHttpActionResult AddEditCustomer(Customer customer)
        {
            try
            {
                var cust = customerRepository.AddEditCustomer(customer);
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/Customer/GetCustomer")]
        [HttpGet]
        public IHttpActionResult GetCustomer(string email)
        {
            try
            {
                var cust = customerRepository.GetCustomer(email);
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
