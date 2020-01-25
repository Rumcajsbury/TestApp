using OpenQA.Selenium;
using System.Linq;

namespace eShopOnWeb.UITests.PagesModels
{
    public class HomePage
    {
        private IWebDriver _driver;
        private string HomePageUrl = $"https://localhost:44315/";

        public HomePage(IWebDriver driver) => _driver = driver;
        public IWebElement LoginBtn => _driver.FindElements(By.ClassName("esh-identity-name")).First(x => x.Text.Equals("LOGIN"));

        public void gotToPage() => _driver.Navigate().GoToUrl(HomePageUrl);
        public LoginPage goToLoginPage()
        {
            LoginBtn.Click();
            return new LoginPage(_driver);
        }

    }
}
