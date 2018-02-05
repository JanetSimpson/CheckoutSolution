using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using CheckoutService.Models;

namespace CheckoutService.Controllers
{
    public class CheckoutController : ApiController
    {

        // Retrieved from data source
        Product[] products = new Product[]
        {
            new Product { SKU = "A99", Description = "Apple", Price = 0.50m },
            new Product { SKU = "B15", Description = "Biscuits", Price = 0.30m },
            new Product { SKU = "C40", Description = "Coffee", Price = 1.80m },
            new Product { SKU = "T23", Description = "Tissues", Price = 0.99m }
        };

        // Retrieved from data source
        SpecialOffer[] specialOffers = new SpecialOffer[]
        {
            new SpecialOffer { SKU = "A99", Quantity = 3, Price = 1.50m, },
            new SpecialOffer { SKU = "B15", Quantity = 2, Price = 0.45m }
        };

        /// <summary>
        /// Method returns shopping basket total value. Usage = IHttpActionResult x = await BasketTotal()
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("BasketTotal")]
        public async Task<IHttpActionResult> GetBasketTotal([FromUri] string basketJSON)
        {
            decimal basketTotal = 0.0m;

            Basket basket = new Basket();
            basket = JsonConvert.DeserializeObject<Basket>(basketJSON);

            if (basket != null)
            {
                foreach (Basket.BasketItem item in basket.Items)
                {
                    decimal itemTotal = 0.0m;

                    var product = Array.Find(products, p => p.SKU == item.SKU);
                    var specialOffer = Array.Find(specialOffers, o => o.SKU == item.SKU);

                    if (product != null)
                    {
                        if (specialOffer != null && (item.Quantity >= specialOffer.Quantity))
                        {
                            itemTotal = ((item.Quantity / specialOffer.Quantity) * specialOffer.Price) + ((item.Quantity % specialOffer.Quantity) * product.Price);
                        }
                        else
                        {
                            itemTotal = (item.Quantity * product.Price);
                        }
                    }

                    basketTotal += itemTotal;
                }
            }

            return await Task.FromResult(Ok(basketTotal));
        }
    }
}
