using eShopOnWeb.UITests.PagesModels;
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
            _driver = new ChromeDriver(@"C:\drivers");
            _driver.Manage().Window.Maximize();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30.00));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Close();
        }
            

        [Test]
        public void FirstTest()
        {
            var homePage = new HomePage(_driver);
            homePage.gotToPage();
            
            var loginPage = homePage.goToLoginPage();
            loginPage.LogIntoApp();
        }


    }
}
