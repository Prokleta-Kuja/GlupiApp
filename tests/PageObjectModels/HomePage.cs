using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GlupiApp.Tests.Enums;
using SeleniumExtras.WaitHelpers;
using GlupiApp.Tests.Mappings;

namespace GlupiApp.Tests.PageObjectModels
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver, WebDriverWait waiter) : base(driver, waiter) { }
        public void Open()
        {
            Driver.Navigate().GoToUrl("http://localhost:5000");
            var titleLocator = By.Id("page-title");
            Waiter.Until(Expected.TextToBePresentInElementLocated(titleLocator, "Hello, world!"));
        }
        public void NavigateTo(Navigation to)
        {
            var locator = NavigationMapping.GetBySelector(to);
            Waiter.Until(Expected.Clicked(locator));
        }
        public CountersPage NavigateToCounters()
        {
            var locator = NavigationMapping.GetBySelector(Navigation.Counters);
            Waiter.Until(Expected.Clicked(locator));

            var titleLocator = By.Id("page-title");
            Waiter.Until(Expected.TextToBePresentInElementLocated(titleLocator, "Counters"));

            var page = new CountersPage(Driver, Waiter);
            return page;
        }
    }
}