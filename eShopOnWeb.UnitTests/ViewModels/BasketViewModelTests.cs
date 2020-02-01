using FluentAssertions;
using Microsoft.eShopWeb.Web.Pages.Basket;
using NUnit.Framework;
using System.Collections.Generic;

namespace eShopOnWeb.UnitTests.ViewModels
{
    [TestFixture]
    public class BasketViewModelTests
    {
        [Test]
        public void Total_ShouldReturn_GoodResult()
        {
            var basketItemsList = new List<BasketItemViewModel>{
                new BasketItemViewModel{ UnitPrice = 2, Quantity=10},
                new BasketItemViewModel{ UnitPrice = (decimal)2.2, Quantity = 1 }
            };
            var basket = new BasketViewModel{ Items = basketItemsList };

            var result = basket.Total();

            result.Should().Be((decimal)22.2);
        }
    }
}
