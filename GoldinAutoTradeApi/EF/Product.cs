//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoldinAutoTradeApi.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Order_Products = new HashSet<Order_Products>();
        }
    
        public int PID { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> UnitsInStock { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Nullable<int> SID { get; set; }
        public byte[] Image { get; set; }
        public string FileName { get; set; }
    
        public virtual ICollection<Order_Products> Order_Products { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
