using OpenQA.Selenium;
using System.Linq;

namespace eShopOnWeb.UITests.PagesModels
{
    public class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver) => _driver = driver;

        private IWebElement LoginInput => _driver.FindElements(By.ClassName("form-control")).First(x => x.GetAttribute("type") == "email");
        private IWebElement PasswordInput => _driver.FindElements(By.ClassName("form-control")).First(x => x.GetAttribute("type") == "password");
        private IWebElement LoginBtn => _driver.FindElements(By.ClassName("btn")).First(x => x.Text == "Log in");
        private IWebElement NotLogedInAlert => _driver.FindElements(By.ClassName("validation-summary-errors")).First(x => x.Text == "Invalid login attempt.");

        public HomePage LogIntoApp(string password = "")
        {
            LoginInput.SendKeys(LogonUser.Login);
            PasswordInput.SendKeys(LogonUser.Password + password);
            LoginBtn.Click();

            return new HomePage(_driver);
        }

        public string GetNotLogedInAlert => NotLogedInAlert.Text;
    }
}
