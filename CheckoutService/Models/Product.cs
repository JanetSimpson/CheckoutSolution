using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutService.Models
{
    public class Product
    {
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}