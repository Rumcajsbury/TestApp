using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShopOnWeb.UITests.PagesModels
{
    public class OrdersPage
    {
        private IWebDriver _driver;
        public OrdersPage(IWebDriver driver) => _driver = driver;
        public IWebElement OpenOrderDetails => _driver.FindElements(By.ClassName("esh-orders-link")).FirstOrDefault();

        public OrderDetailPage OpenOrderDetailPage()
        {
            OpenOrderDetails.Click();
            return new OrderDetailPage(_driver);
        }
    }
}
