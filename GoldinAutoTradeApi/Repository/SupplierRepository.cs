using GlodinAutoTradeModel.Models;
using GoldinAutoTradeApi.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldinAutoTradeApi.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        List<Supplier> ISupplierRepository.GetSuppliers()
        {
            try
            {
                using (var context = new EF.GoldinAutoEntities())
                {
                    return (from a in context.Suppliers
                            select new Supplier
                            {
                                SID = a.SID,
                                SupplierName = a.SupplierName

                            }).ToList<Supplier>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}