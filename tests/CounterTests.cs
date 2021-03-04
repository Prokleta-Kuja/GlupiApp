using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace GlupiApp.Tests
{
    public class CounterTests
    {
        [Fact]
        public void TestName()
        {
            NewMethod();
        }

        private static void NewMethod()
        {
            //Given
            var chromDriverPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "drivers");
            var options = new ChromeOptions();
            options.AddArgument("--remote-debugging-port=9225");

            var driver = new ChromeDriver("/home/tonko/repos/GlupiApp/drivers", options);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);

            driver.Navigate().GoToUrl("http://localhost:5000");
            driver.Manage().Window.Size = new System.Drawing.Size(1366, 768);
            driver.FindElement(By.LinkText("Counter")).Click();

            var pageTile = wait.Until(d => d.FindElement(By.Id("page-title")));
            wait.Until(ExpectedConditions.TextToBePresentInElement(pageTile, "Counters"));

            driver.FindElement(By.Id("drugi-button")).Click();
            driver.FindElement(By.Id("drugi-button")).Click();
            driver.FindElement(By.Id("drugi-button")).Click();
            driver.FindElement(By.Id("drugi-button")).Click();

            System.Threading.Thread.Sleep(5000);

            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("result-2"), "Current count: 4"));

            driver.Quit();
        }
    }
}