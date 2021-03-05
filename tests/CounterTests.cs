using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using GlupiApp.Tests.PageObjectModels;
using GlupiApp.Tests.Enums;

namespace GlupiApp.Tests
{
    public class CounterTests : IDisposable
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Waiter { get; set; }
        public CounterTests()
        {
            Driver = Infrastructure.GetInstance();
            Waiter = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void TestName()
        {
            var home = new HomePage(Driver);
            home.Open();
            home.NavigateTo(Navigation.Counters);

            Waiter.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("page-title"), "Counters"));

            var drugiButton = Driver.FindElement(By.Id("drugi-button"));
            drugiButton.Click();
            drugiButton.Click();
            drugiButton.Click();
            drugiButton.Click();

            Waiter.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("result-2"), "Current count: 4"));
        }

        public void Dispose()
        {
            Infrastructure.DisposeDriver(Driver);
        }
    }
}