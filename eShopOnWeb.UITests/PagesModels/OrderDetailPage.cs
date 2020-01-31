using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShopOnWeb.UITests.PagesModels
{
    public class OrderDetailPage
    {
        private IWebDriver _driver;
        public OrderDetailPage(IWebDriver driver) => _driver = driver;
    }
}
