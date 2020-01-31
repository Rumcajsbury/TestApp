using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShopOnWeb.UITests.PagesModels
{
    public class BasketPage
    {
        private IWebDriver _driver;

        public BasketPage(IWebDriver driver) => _driver = driver;
        public IWebElement QuantityInput => _driver.FindElements(By.ClassName("esh-basket-input")).FirstOrDefault();
        public IWebElement UpdateBasketButton => _driver.FindElements(By.Name("updatebutton")).FirstOrDefault();
        public IWebElement CheckoutBasketButton => _driver.FindElements(By.Name("action")).FirstOrDefault();

        public void SetQuantity(int quantity)
        {
            QuantityInput.SendKeys(quantity.ToString());
            UpdateBasketButton.Click();
        }

        public LoginPage Checkout()
        {
            CheckoutBasketButton.Click();
            return new LoginPage(_driver);
        }
    }
}
