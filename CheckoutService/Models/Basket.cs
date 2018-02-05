using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutService.Models
{
    public class Basket
    {
        public List<BasketItem> Items { get; set; }

        public class BasketItem
        {
            public string SKU { get; set; }
            public Int32 Quantity { get; set; }
        }
    }
}