using Ardalis.GuardClauses;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Moq;
using NUnit.Framework;
using System;

namespace eShopOnWeb.UnitTests.Exceptions
{
    [TestFixture]
    public class BasketNotFoundExceptionTests
    {
        [Test]
        public void BasketNotFoundException_MessageShouldBeGood()
        {
            var basketId = 11;

            var basketException = new BasketNotFoundException(basketId);

            basketException.Message.Should().Be($"No basket found with id {basketId}");
        }

        [Test]
        public void NullBasket_ShouldThrow_BasketNotFoundException()
        {
            var guardClause = new Mock<IGuardClause>();

            Action act = () => guardClause.Object.NullBasket(11, null);

            act.Should()
                .Throw<BasketNotFoundException>()
                .WithMessage("No basket found with id 11");
        }
    }
}
