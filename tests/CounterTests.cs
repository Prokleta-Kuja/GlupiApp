using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using GlupiApp.Tests.PageObjectModels;
using GlupiApp.Tests.Enums;
using System.Globalization;
using System.Collections.Generic;

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


        [Fact]
        public void Navigacija()
        {
            var homePage = new HomePage(Driver, Waiter);
            homePage.Open();

            IWebElement countersGumb = Driver.FindElement(BasePage.CountersNav);
            IWebElement fetchDataGumb = Driver.FindElement(BasePage.FetchDataNav);
            IWebElement homeGumb = Driver.FindElement(BasePage.HomeNav);

            fetchDataGumb.Click();
            Waiter.Until(Expected.TextToBePresentInElementLocated(CountersPage.PageTitle, "Weather forecast"));

            countersGumb.Click();
            Waiter.Until(Expected.TextToBePresentInElementLocated(CountersPage.PageTitle, "Counters"));

            homeGumb.Click();
            Waiter.Until(Expected.TextToBePresentInElementLocated(HomePage.PageTitle, "Hello, world!"));
        }



        [Fact]
        public void Datumi()
        {
            var fetchData = new FetchDataPage(Driver, Waiter);
            fetchData.Open();

            
            var rows = fetchData.GetRows();

            var dates = new List<string>();
            var expectedDates = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                var dt = DateTime.UtcNow.AddDays(i+1);
                expectedDates.Add(dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            }


            foreach (var row in rows)
            {
                var cells = row.FindElements(By.CssSelector("td"));
                var dateCell = cells[0];
                var dateText = dateCell.Text;

                dates.Add(dateText);

            }

            Assert.Equal(expectedDates, dates);

            
        }


        [Fact]
        public void AreFahrenheitTemperaturesProperlyConvertedToCelsiusTemperatures()
        {
            var fetchData = new FetchDataPage(Driver, Waiter);
            fetchData.Open();


            var rows = fetchData.GetRows();
            
            foreach (var row in rows)
            {
                
                var cells = row.FindElements(By.CssSelector("td"));
                var cellC = cells[1];
                var cellF = cells[2];

                if (!int.TryParse(cellC.Text, out var tempC))
                    throw new Exception();

                if(!int.TryParse(cellF.Text, out var tempF))
                    throw new Exception();

                var expectedF = 32 + (int)(tempC / 0.5556);
                Assert.Equal(expectedF, tempF);
            }

        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        public void Counter1(int count)
        {
            var counters = new CountersPage(Driver, Waiter);
            counters.Open();

            for (int i = 0; i < count; i++)
            {
                counters.ClickLocator(CountersPage.Button1);
            }

            Waiter.Until(Expected.TextToBePresentInElementLocated(CountersPage.Result1, $"Current count: {count}"));
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

        public void Dispose()
        {
            Infrastructure.DisposeDriver(Driver);
        }

    }
}