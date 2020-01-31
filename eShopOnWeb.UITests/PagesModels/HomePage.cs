using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Linq;

namespace eShopOnWeb.UITests.PagesModels
{
    public class HomePage
    {
        private IWebDriver _driver;
        private string HomePageUrl = $"https://localhost:44315/";

        public HomePage(IWebDriver driver) => _driver = driver;
        public IWebElement LoginBtn => _driver.FindElements(By.ClassName("esh-identity-name")).FirstOrDefault(x => x.Text.Equals("LOGIN"));
        public IWebElement ShopItemButton => _driver.FindElements(By.ClassName("esh-catalog-button")).FirstOrDefault();
        public IWebElement OpenOrders => _driver.FindElements(By.CssSelector("[href*='/order/my-orders']")).FirstOrDefault();

        public void gotToPage() => _driver.Navigate().GoToUrl(HomePageUrl);
        public LoginPage GoToLoginPage()
        {
            LoginBtn.Click();
            return new LoginPage(_driver);
        }

        public BasketPage AddShoppingItemToBasket()
        {
            ShopItemButton.Click();
            return new BasketPage(_driver);
        }

        public OrdersPage OpenOrdersPage()
        {
            OpenOrders.Click();
            return new OrdersPage(_driver);
        }
    }
}
