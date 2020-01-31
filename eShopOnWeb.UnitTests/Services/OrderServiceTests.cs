using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace eShopOnWeb.UnitTests.Services
{
    [TestFixture]
    class OrderServiceTests
    {
        [Test]
        public async Task OrderServiceTests_createOrder_assertOrderValuesCorrect()
        {
            var orderRepository = new Mock<IAsyncRepository<Order>>();
            var basketRepository = new Mock<IAsyncRepository<Basket>>();
            var itemRepository = new Mock<IAsyncRepository<CatalogItem>>();

            var givenUserName = "user";
            var givenBasketId = 0;
            var givenCatalogItemId = 512;
            var givenBasket = new Basket
            {
                Id = givenBasketId,
                BuyerId = givenUserName
            };
            givenBasket.AddItem(givenCatalogItemId, 20, 10);
            var givenAddress = new Address("Cystersów", "Krakow", "Małopolska", "Polska", "32-012");
            var givenCatalogItem = new CatalogItem()
            {
                Id = 1,
                Name = "Some catalog item",
                PictureUri = "http://picture.com/pic1.png"
            };
            
            basketRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(givenBasket);   
            itemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(givenCatalogItem);

            Order resultOrder = null;
            orderRepository.Setup(x => x.AddAsync(It.IsAny<Order>()))
             .ReturnsAsync((Order order) => {
                 resultOrder = order;
                 return order; 
             });
            
            var orderService = new OrderService(basketRepository.Object, itemRepository.Object, orderRepository.Object);
            await orderService.CreateOrderAsync(givenBasketId, givenAddress);

            resultOrder.Id.Should().Be(0);
            resultOrder.BuyerId.Should().Be(givenUserName);
            resultOrder.ShipToAddress.Should().Be(givenAddress);
            resultOrder.Total().Should().Be(200);
            resultOrder.OrderItems.Count.Should().Be(1);
        }
    }
}
