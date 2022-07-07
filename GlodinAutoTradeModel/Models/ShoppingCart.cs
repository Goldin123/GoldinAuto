using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlodinAutoTradeModel.Models
{
    public class ShoppingCart
    {
        public int SCID { get; set; }
        public int CID { get; set; }
        public int PID { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductsAvailable { get; set; }
    }
}
