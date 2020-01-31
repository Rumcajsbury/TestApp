using FluentAssertions;
using Microsoft.eShopWeb.Web.Extensions;
using NUnit.Framework;

namespace eShopOnWeb.UnitTests.Extensions
{
    [TestFixture]
    public class CacheHelpersTests
    {
        [Test]
        public void GenerateBrandsCacheKey_ShouldReturn_Brands()
        {
            var result = CacheHelpers.GenerateBrandsCacheKey();

            result.Should().Be("brands");
        }

        [Test]      
        public void GenerateTypesCacheKey_ShouldReturn_Brands()
        {
            var result = CacheHelpers.GenerateTypesCacheKey();

            result.Should().Be("types");
        }

        [Test]
        public void GenerateCatalogItemCacheKey_ShouldReturn_ValidData()
        {
            var pageIndex = 100;
            var itemsPage = 11;
            var brandId = 55;
            var typeId = 1;

            var result = CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, itemsPage, brandId, typeId);

            result.Should().Be("items-100-11-55-1");
        }
    }
}
