using eShopOnWeb.UITests.PagesModels;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace eShopOnWeb.UITests
{
    [TestFixture]
    public class CommonUITests
    {
        private IWebDriver _driver;
        private string _url = $"https://localhost:44315/";
        IWait<IWebDriver> _wait;

        [OneTimeSetUp]
        public void OneTimesSetUp()
        {
            _driver = new ChromeDriver(@"C:\drivers");
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30.00));
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
            
            var loginPage = homePage.goToLoginPage();
            loginPage.LogIntoApp("invalid login");

            loginPage.GetNotLogedInAlert.Should().Be("Invalid login attempt.");
        }


        [Test]
        public void TryToChangePassword_NotMatchingPassword_ShouldNotUpdate()
        {
            var homepage = LogIntoApp();
            var changePassword = new ChangePasswordPage(_driver, _url);
            changePassword.GoToPage();
            changePassword.ChangePassword(LogonUser.Password, "aaa", "bbb");
            var result = changePassword.ErrorValidationResponse();

            result.Should().Contain("The new password and confirmation password do not match.");
        }



        //Helper function for loggin in with valid data
        private HomePage LogIntoApp()
        {
            var homePage = new HomePage(_driver);
            homePage.gotToPage();

            var loginPage = homePage.goToLoginPage();
            return loginPage.LogIntoApp();
        }

    }
}
