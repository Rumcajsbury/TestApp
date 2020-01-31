using eShopOnWeb.UITests.PagesModels;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace eShopOnWeb.UITests
{
    [TestFixture]
    public class CommonUITests
    {
        private IWebDriver _driver;
        private string URL = $"https://localhost:44315/";
        IWait<IWebDriver> wait;

        [OneTimeSetUp]
        public void OneTimesSetUp()
        {
            _driver = new ChromeDriver(@"C:\Drivers");
            _driver.Manage().Window.Maximize();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30.00));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Close();
        }

        [Test]
        public void TryLogIn_InvalidPassword_ShouldNotLogin()
        {
            var homePage = new HomePage(_driver);
            homePage.gotToPage();

            var loginPage = homePage.GoToLoginPage();
            loginPage.LogIntoApp("invalid login");

            loginPage.GetNotLogedInAlert.Should().Be("Invalid login attempt.");
        }

        [Test]
        public void AddItemsToBasketUnloggedIn_proceedThroughWholeProcess_checkItemsOrderedProperly()
        {
            var homePage = new HomePage(_driver);
            homePage.gotToPage();

            var basketPage = homePage.AddShoppingItemToBasket();
            basketPage.SetQuantity(10);

            var moneyAmount = basketPage.Checkout()
                .LogIntoAppFromCheckout()
                .ContinueShopping()
                .OpenOrdersPage()
                .OpenOrderDetailPage();
        }

        //Helper function for loggin in with valid data
        private HomePage LogIntoApp()
        {
            var homePage = new HomePage(_driver);
            homePage.gotToPage();

            var loginPage = homePage.GoToLoginPage();
            return loginPage.LogIntoApp();
        }

    }
}
