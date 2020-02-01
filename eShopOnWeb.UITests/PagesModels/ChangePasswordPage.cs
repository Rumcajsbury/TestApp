using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace eShopOnWeb.UITests.PagesModels
{
    public class ChangePasswordPage
    {
        private IWebDriver _driver;
        private string _url;
        private IWebElement CurrentPasswordInput => _driver.FindElement(By.Id("OldPassword"));
        private IWebElement NewPasswordInput => _driver.FindElement(By.Id("NewPassword"));
        private IWebElement ConfirmPasswordInput => _driver.FindElement(By.Id("ConfirmPassword"));
        private IWebElement UpdateBtn => _driver.FindElements(By.ClassName("btn")).FirstOrDefault(x => x.Text == "Update password");
        public ChangePasswordPage(IWebDriver driver, string baseUrl)
        {
            _driver = driver;
            _url = baseUrl + "manage/change-password";
        }

        public void GoToPage() => _driver.Navigate().GoToUrl(_url);

        public void ChangePassword(string oldPass, string newPass, string newPassConfirm)
        {
            CurrentPasswordInput.SendKeys(oldPass);
            NewPasswordInput.SendKeys(newPass);
            ConfirmPasswordInput.SendKeys(newPassConfirm);
            UpdateBtn.Click();
        }

        public List<string> ErrorValidationResponse()
        {
            var items = _driver.FindElement(By.ClassName("validation-summary-errors")).FindElement(By.TagName("ul"))
                .FindElements(By.TagName("li"));
            return items.Select(x => x.Text).ToList();
        }
    }
}
