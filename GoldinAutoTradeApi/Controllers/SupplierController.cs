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
    public class SupplierController : ApiController
    {
        ISupplierRepository supplierRepository = new SupplierRepository();
        [Route("api/Supplier/GetSuppliers")]
        [HttpGet]
        public IHttpActionResult GetSuppliers()
        {
            try
            {
                var suppliers = supplierRepository.GetSuppliers();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
