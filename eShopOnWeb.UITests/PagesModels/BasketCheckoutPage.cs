using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShopOnWeb.UITests.PagesModels
{
    public class BasketCheckoutPage
    {
        private IWebDriver _driver;

        public BasketCheckoutPage(IWebDriver driver) => _driver = driver;
        public IWebElement ContinueShoppingButton => _driver.FindElements(By.XPath("//a")).FirstOrDefault(x => x.Text.Equals("Continue Shopping..."));

        public HomePage ContinueShopping()
        {
            ContinueShoppingButton.Click();
            return new HomePage(_driver);
        }

    }
}
