using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace eShopOnWeb.UnitTests.Features
{
    [TestFixture]
    public class GetOrderDetailsHandlerTests
    {
        public async Task Handle_Should_ReturnNull_IfOrderNotFound()
        {
            var orderRepoMock = new Mock<IOrderRepository>();
            orderRepoMock.Setup(x => x.ListAsync(It.IsAny<ISpecification<Order>>()))
                .ReturnsAsync(
                    new List<Order>()
                    {
                        new Order("1", new Address("s", "b", "e", "f", "g"), new List<OrderItem>()) {Id = 10},
                        new Order("2", new Address("s", "b", "e", "f", "g"), new List<OrderItem>()) {Id = 12},
                        new Order("3", new Address("s", "b", "e", "f", "g"), new List<OrderItem>()) {Id = 13},
                    });

            var getOrderDetailsHandler = new GetOrderDetailsHandler(orderRepoMock.Object);
            var result = await getOrderDetailsHandler.Handle(new GetOrderDetails("pawel", 1), CancellationToken.None);

            result.Should()
                .BeNull();

        }
    }
}
