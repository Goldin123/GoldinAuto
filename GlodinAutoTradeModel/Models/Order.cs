using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlodinAutoTradeModel.Models
{
    public class Order
    {
        public int OID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CID { get; set; }
    }

    public class OrderProducts 
    {
        public int OPID { get; set; }
        public int OID { get; set; }
        public int PID { get; set; }
        public int Quatity { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class OrderHistory 
    {
        public int PID { get; set; }
        public int OID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
