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
    public class HomeController : Controller
    {
        readonly ICustomerRepository _customerRepository;
        public HomeController(ICustomerRepository customerRepository) 
        {
            this._customerRepository = customerRepository;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            await _customerRepository.SetGlobalVariable(userClaims);
            ViewBag.Name = Globals.Name;
            ViewBag.Username = Globals.Email;
            ViewBag.Subject = Globals.Subject;
            ViewBag.TenantId = Globals.TenantId;
            
            return View();
        }

       
    }
}