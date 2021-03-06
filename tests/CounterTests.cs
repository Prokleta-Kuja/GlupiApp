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
            Waiter.PollingInterval = TimeSpan.FromMilliseconds(250);
        }

        public void Dispose()
        {
            Infrastructure.DisposeDriver(Driver);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        public void Counter3(int count)
        {
            var counters = new CountersPage(Driver, Waiter);
            counters.Open();

            for (int i = 0; i < count; i++)
            {
                counters.ClickLocator(CountersPage.Button3);
            }

            Waiter.Until(Expected.TextToBePresentInElementLocated(CountersPage.Result3, $"Current count: {count}"));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        public void Counter2(int count)
        {
            var counters = new CountersPage(Driver, Waiter);
            counters.Open();

            for (int i = 0; i < count; i++)
            {
                counters.ClickLocator(CountersPage.Button2);
            }

            Waiter.Until(Expected.TextToBePresentInElementLocated(CountersPage.Result2, $"Current count: {count}"));
        }
    }
}