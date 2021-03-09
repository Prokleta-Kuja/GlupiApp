
using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace GlupiApp.Tests.PageObjectModels
{
    public class FetchDataPage : BasePage
    {
        

        public FetchDataPage(IWebDriver driver, WebDriverWait waiter) : base(driver, waiter) { }
        
        public void Open()
        {
            Driver.Navigate().GoToUrl("http://localhost:5000/fetchdata");

            Waiter.Until(Expected.TextToBePresentInElementLocated(PageTitle, "Weather forecast"));
        }

        internal ReadOnlyCollection<IWebElement> GetRows()
        {
            var rows = Driver.FindElements(By.CssSelector("tbody > tr"));
            return rows;
        }
    }
}