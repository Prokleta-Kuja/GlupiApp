using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GlupiApp.Tests.PageObjectModels
{
    public class BasePage
    {
        public static readonly By PageTitle = By.Id("page-title");

        public IWebDriver Driver { get; set; }
        public WebDriverWait Waiter { get; set; }

        public BasePage(IWebDriver driver, WebDriverWait waiter)
        {
            Driver = driver;
            Waiter = waiter;
        }
        public void ClickLocator(By locator)
        {
            Waiter.Until(Expected.Clicked(locator));
        }
    }
}