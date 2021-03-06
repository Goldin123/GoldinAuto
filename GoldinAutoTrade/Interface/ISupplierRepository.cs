using GlodinAutoTradeModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldinAutoTrade.Interface
{
    public interface ISupplierRepository
    {
        Task<Tuple<List<Supplier>>> GetSuppliers();
    }
}
