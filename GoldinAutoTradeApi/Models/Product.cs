﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldinAutoTradeApi.Models
{
    public class Product
    {
        public int PID { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}