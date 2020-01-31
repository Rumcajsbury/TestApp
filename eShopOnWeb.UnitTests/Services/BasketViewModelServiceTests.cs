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

namespace eShopOnWeb.UnitTests.Services
{
    [TestFixture]
    class BasketViewModelServiceTests
    {
        [Test]
        public async Task BasketViewModelService_userWithoutBaskets_createBasket()
        {
            var basketRepository = new Mock<IAsyncRepository<Basket>>();
            var uriComposer = new Mock<IUriComposer>();
            var itemRepository = new Mock<IAsyncRepository<CatalogItem>>();

            var givenUserName = "user";
            var givenBasketId = 0;
            var givenBasket = new Basket
            {
                Id = givenBasketId,
                BuyerId = givenUserName
            };

            basketRepository.Setup(x => x.ListAsync(It.IsAny<BasketWithItemsSpecification>()))
              .ReturnsAsync(new List<Basket>());
            basketRepository.Setup(x => x.AddAsync(It.IsAny<Basket>()))
              .ReturnsAsync(givenBasket);

            var viewModel = new BasketViewModelService(basketRepository.Object, itemRepository.Object, uriComposer.Object);

            var resultBasket = await viewModel.GetOrCreateBasketForUser(givenUserName);
            resultBasket.Id.Should().Be(givenBasketId);
            resultBasket.BuyerId.Should().Be(givenUserName);
            resultBasket.Items.Should().BeEmpty();
        }

        [Test]
        public async Task BasketViewModelService_userWithBaskets_createBasketViewModel()
        {
            var basketRepository = new Mock<IAsyncRepository<Basket>>();
            var uriComposer = new Mock<IUriComposer>();
            var itemRepository = new Mock<IAsyncRepository<CatalogItem>>();

            var givenUserName = "user";
            var givenBasketId = 0;
            var givenCatalogItemName = "CatalogItem";
            var givenCatalogItemId = 512;
            var givenCatalogItemId2 = 112;
            var givenBasket = new Basket
            {
                Id = givenBasketId,
                BuyerId = givenUserName,
            };
            givenBasket.AddItem(givenCatalogItemId, 20);
            givenBasket.AddItem(givenCatalogItemId2, 10, 10);
            var givenBaskets = new List<Basket>
            {
                givenBasket
            };

            var givenCatalogItem = new CatalogItem()
            {
                Id = givenCatalogItemId,
                Name = givenCatalogItemName
            };

            var givenCatalogItem2 = new CatalogItem()
            {
                Id = givenCatalogItemId2,
                Name = givenCatalogItemName
            };
            var givenUri = "http://uri.com/";

            basketRepository.Setup(x => x.ListAsync(It.IsAny<BasketWithItemsSpecification>()))
              .ReturnsAsync(givenBaskets);
            basketRepository.Setup(x => x.AddAsync(It.IsAny<Basket>()))
              .ReturnsAsync(givenBasket);
            itemRepository.Setup(x => x.GetByIdAsync(givenCatalogItemId))
                .ReturnsAsync(givenCatalogItem);
            itemRepository.Setup(x => x.GetByIdAsync(givenCatalogItemId2))
                .ReturnsAsync(givenCatalogItem2);

            uriComposer.Setup(x => x.ComposePicUri(It.IsAny<string>()))
                .Returns(givenUri);

            var viewModel = new BasketViewModelService(basketRepository.Object, itemRepository.Object, uriComposer.Object);

            var resultBasket = await viewModel.GetOrCreateBasketForUser(givenUserName);
            resultBasket.Id.Should().Be(givenBasketId);
            resultBasket.BuyerId.Should().Be(givenUserName);
            resultBasket.Items.Should().HaveCount(2);

            resultBasket.Items[0].Id.Should().Be(0);
            resultBasket.Items[0].CatalogItemId.Should().Be(givenCatalogItemId);
            resultBasket.Items[0].ProductName.Should().Be(givenCatalogItemName);
            resultBasket.Items[0].Quantity.Should().Be(1);
            resultBasket.Items[0].PictureUrl.Should().Be(givenUri);

            resultBasket.Items[1].Id.Should().Be(0);
            resultBasket.Items[1].CatalogItemId.Should().Be(givenCatalogItemId2);
            resultBasket.Items[1].ProductName.Should().Be(givenCatalogItemName);
            resultBasket.Items[1].Quantity.Should().Be(10);
            resultBasket.Items[1].PictureUrl.Should().Be(givenUri);
        }
    }
}
