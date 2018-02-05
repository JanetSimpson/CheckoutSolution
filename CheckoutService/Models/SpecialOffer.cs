using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutService.Models
{
    public class SpecialOffer
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}