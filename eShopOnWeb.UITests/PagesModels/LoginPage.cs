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

        public void LogIntoApp()
        {
            LoginInput.SendKeys(LogonUser.Login);
            PasswordInput.SendKeys(LogonUser.Password);
            LoginBtn.Click();
        }
    }
}
