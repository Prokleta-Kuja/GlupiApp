using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GlupiApp.Tests.Enums;
using SeleniumExtras.WaitHelpers;
using GlupiApp.Tests.Mappings;

namespace GlupiApp.Tests.PageObjectModels
{
    public class HomePage
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Waiter { get; set; }
        public HomePage(IWebDriver driver)
        {
            Driver = driver;
            Waiter = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
        }
        public void Open()
        {
            Driver.Navigate().GoToUrl("http://localhost:5000");
            var pageTitle = Waiter.Until(d => d.FindElement(By.Id("page-title")));

            if (pageTitle.Text != "Hello, world!")
                throw new Exception("Could not open home page");
        }
        public void NavigateTo(Navigation to)
        {
            var locator = NavigationMapping.GetBySelector(to);
            Waiter.Until(ExpectedConditions.ElementExists(locator)).Click();
        }
    }
}