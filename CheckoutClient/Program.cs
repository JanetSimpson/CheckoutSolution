using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

using Newtonsoft.Json;



namespace CheckoutClient
{
    public class Basket
    {
        public List<BasketItem> Items { get; set; }

        public Basket()
        {
            Items = new List<BasketItem>();
        }

        public class BasketItem
        {
            public string SKU { get; set; }
            public Int32 Quantity { get; set; }
        }
    }

    /// <summary>
    /// Calculates total value of items scanned into a shopping basket
    /// </summary>
    class Program
    {
        static HttpClient client = new HttpClient();

        static int Main(string[] args)
        {
            decimal basketTotal = 0.0m;

            if (args.Length == 0)
            {
                Console.WriteLine("Please scan at least one item.");
                Console.ReadLine();
                return 1;
            }

            Basket basket = new Basket();
            foreach (string arg in args)
            {
                AddBasketItem(arg, ref basket);
            }

            try
            {
                basketTotal = Checkout(basket).Result;
                Console.WriteLine("Basket Total is: {0}", basketTotal);
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

            return 0;
        }

        /// <summary>
        /// If SKU already in the basket, then update the quantity, else add item to basket.
        /// </summary>
        /// <param name="sku"></param>
        private static void AddBasketItem(string sku, ref Basket basket)
        {
            bool matchFound = false;
            foreach (Basket.BasketItem item in basket.Items)
            {
                if (sku.ToLower().Trim() == item.SKU.ToLower().Trim())
                {
                    matchFound = true;
                    item.Quantity++;
                    break;
                }
            }
            if (!matchFound)
            {
                Basket.BasketItem newItem = new Basket.BasketItem();
                newItem.SKU = sku;
                newItem.Quantity = 1;
                basket.Items.Add(newItem);
            }
        }

        /// <summary>
        /// Returns the basket total 
        /// </summary>
        /// <returns></returns>
        static async Task<decimal> Checkout(Basket basket)
        {
            decimal basketTotal = 0.0m;

            client.BaseAddress = new Uri("http://localhost:51022/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var json = JsonConvert.SerializeObject(basket);
                var response = await client.GetAsync("api/Checkout/GetBasketTotal?basketJSON=" + json);
                response.EnsureSuccessStatusCode();
                var contents = response.Content.ReadAsStringAsync().Result;
                if (!Decimal.TryParse(contents, out basketTotal))
                {
                    throw new Exception("Invalid return value.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return basketTotal;
        }

    }
}
