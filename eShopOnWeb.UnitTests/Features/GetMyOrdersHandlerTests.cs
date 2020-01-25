using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.eShopWeb.Web.ViewModels;
using Moq;
using NUnit.Framework;

namespace eShopOnWeb.UnitTests.Features
{
    [TestFixture]
    public class GetMyOrdersHandlerTests
    {
        [Test]
        public async Task Handle_Should_ReturnOrdersList_ForProvidedUserName()
        {
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.ListAsync(It.IsAny<ISpecification<Order>>()))
                .ReturnsAsync(
                    new List<Order>() {new Order("1", new Address("a", "b", "c", "d", "e"), new List<OrderItem>())});
            var getMyordershandler = new GetMyOrdersHandler(orderRepositoryMock.Object);
            var list = await getMyordershandler.Handle(new GetMyOrders("pawel"), CancellationToken.None);

            list.Should()
                .NotBeEmpty();
        }
        [Test]
        public async Task Handle_Should_ThrowNullReferenceException_If_RequstParameterIsNull()
        {
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var getMyordershandler = new GetMyOrdersHandler(orderRepositoryMock.Object);
            var act = new Func<Task<IEnumerable<OrderViewModel>>>(() => getMyordershandler.Handle(null, CancellationToken.None));

            await act.Should()
                .ThrowAsync<NullReferenceException>();
        }
    }
}
