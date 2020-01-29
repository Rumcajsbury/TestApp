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

            act.Should()
                .ThrowAsync<NullReferenceException>();
        }
    }
}
