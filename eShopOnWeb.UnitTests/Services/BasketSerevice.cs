using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Moq;
using NUnit.Framework;

namespace eShopOnWeb.UnitTests.Services
{
    [TestFixture]
    public class BasketServiceTests
    {
        [Test]
        public async Task AddItemToBasket_Should_ThrowNullReferenceException_WhenBasketIsNull()
        {
            var basketRepoMock = new Mock<IAsyncRepository<Basket>>();
            var loggerMock = new Mock<IAppLogger<BasketService>>();
            basketRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Basket) null);
            var basketService = new BasketService(basketRepoMock.Object, loggerMock.Object);

            var act = new Func<Task>(() => basketService.AddItemToBasket(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>()));

            await act.Should()
                .ThrowAsync<NullReferenceException>();
        }
        [Test]
        public async Task AddItemToBasket_Should_AddNewItem_ToGivenBasket()
        {
            var basketRepoMock = new Mock<IAsyncRepository<Basket>>();
            var loggerMock = new Mock<IAppLogger<BasketService>>();
            var basket = new Basket()
            {
                BuyerId = Guid.NewGuid().ToString(),
                Id = 1
            };
            basketRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(basket);
            var basketService = new BasketService(basketRepoMock.Object, loggerMock.Object);

            await basketService.AddItemToBasket(
                1,
                2,
                20,
                1);

            basket.Items.Should()
                .NotBeEmpty();
            basket.Items.FirstOrDefault()
                .UnitPrice.Should()
                .Be(20);
        }
        [Test]
        public async Task AddItemToBasket_Should_Execute_UpdateAsync_ExactlyOnce()
        {
            var basketRepoMock = new Mock<IAsyncRepository<Basket>>();
            var loggerMock = new Mock<IAppLogger<BasketService>>();
            var basket = new Basket()
            {
                BuyerId = Guid.NewGuid().ToString(),
                Id = 1
            };
            basketRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(basket);
            var basketService = new BasketService(basketRepoMock.Object, loggerMock.Object);

            await basketService.AddItemToBasket(
                1,
                2,
                20,
                1);

            basketRepoMock.Verify(x => x.UpdateAsync(basket), Times.Once);
        }
        [Test]
        public async Task DeleteBasketAsync_Should_Execute_DeleteAsync_ExactlyOnce()
        {
            var basketRepoMock = new Mock<IAsyncRepository<Basket>>();
            var loggerMock = new Mock<IAppLogger<BasketService>>();
            var basket = new Basket()
            {
                BuyerId = Guid.NewGuid().ToString(),
                Id = 1
            };
            basketRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(basket);
            var basketService = new BasketService(basketRepoMock.Object, loggerMock.Object);

            await basketService.DeleteBasketAsync(1);

            basketRepoMock.Verify(x => x.DeleteAsync(basket), Times.Once);
        }
    }
}
