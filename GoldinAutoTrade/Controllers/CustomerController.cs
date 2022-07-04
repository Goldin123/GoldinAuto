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
    public class CustomerController : Controller
    {
        readonly ICustomerRepository _customerRepository;

        // GET: Customer

        public CustomerController(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<ActionResult> Index()
        {
            var customer = await _customerRepository.GetCustomer(Globals.Email);
            if (customer.Item1 != null)
                return View(customer.Item1);
            else
                return View(new Customer());
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomer(Customer customer) 
        {
            if (ModelState.IsValid) 
            {
                await _customerRepository.EditCustomer(customer);
            }
            return RedirectToAction("Index", "Customer");
        }
        
    }
}