using System;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Http.Results;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutService.Controllers;
using CheckoutService.Models;

namespace CheckoutService.Tests
{
    [TestClass]
    public class CheckoutUnitTest
    {
        [TestMethod]
        public async Task CheckoutAsyncReturnsCorrectBasketTotal()
        {
            var controller = new CheckoutController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // create basket object to pass in (value £9.29)
            var basketJSON = "{\"Items\":[{\"SKU\":\"T23\",\"Quantity\":1},{\"SKU\":\"A99\",\"Quantity\":7},{\"SKU\":\"C40\",\"Quantity\":2},{\"SKU\":\"B15\",\"Quantity\":5}]}";

            var response = await controller.GetBasketTotal(basketJSON) as OkNegotiatedContentResult<decimal>;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(response.Content, 9.29m);
        }
    }
}
