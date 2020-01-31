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
using Microsoft.eShopWeb.Web.ViewModels;

namespace eShopOnWeb.UnitTests.Services
{
    [TestFixture]
    class CatalogItemViewModelServiceTests
    {
        [Test]
        public async Task CatalogItemViewModelService_updateCatalogItem_assertValuesUpdated()
        {
            var itemRepository = new Mock<IAsyncRepository<CatalogItem>>();

            var givenCatalogItem = new CatalogItem()
            {
                Id = 1,
                Name = "Some catalog item",
                PictureUri = "http://picture.com/pic1.png",
                Price = 13
            };

            var updatedCatalogItem = new CatalogItemViewModel()
            {
                Id = 1,
                Name = "New catalog item",
                Price = 20
            };

            itemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(givenCatalogItem);
           
            CatalogItem resultCatalogItem = null;
            itemRepository.Setup(x => x.UpdateAsync(It.IsAny<CatalogItem>()))
                .Returns((CatalogItem item) => { resultCatalogItem = item; return Task.CompletedTask; });

            var catalogItemViewModelService = new CatalogItemViewModelService(itemRepository.Object);
            await catalogItemViewModelService.UpdateCatalogItem(updatedCatalogItem);

            resultCatalogItem.Id.Should().Be(1);
            resultCatalogItem.Name.Should().Be("New catalog item");
            resultCatalogItem.Price.Should().Be(20);

        }
    }
}
