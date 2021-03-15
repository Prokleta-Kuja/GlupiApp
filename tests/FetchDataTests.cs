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
    public class FetchDataTests : IDisposable
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Waiter { get; set; }
        public FetchDataTests()
        {
            Driver = Infrastructure.GetInstance();
            Waiter = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            Waiter.PollingInterval = TimeSpan.FromMilliseconds(250);
        }



        [Fact]
        public void AreDatesInTableValid()
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
        public void AreCelsiusTemperaturesProperlyConvertedToFahrenheitTemperatures()
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

        public void Dispose()
        {
            Infrastructure.DisposeDriver(Driver);
        }

    }
}