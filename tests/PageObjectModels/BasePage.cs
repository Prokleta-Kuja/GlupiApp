using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GlupiApp.Tests.PageObjectModels
{
    public class BasePage
    {
        public static readonly By PageTitle = By.Id("page-title");

        public static readonly By HomeNav = By.CssSelector("body > div.page > div.sidebar > div.collapse > ul > li:nth-child(1) > a");
        public static readonly By CountersNav = By.CssSelector("body > div.page > div.sidebar > div.collapse > ul > li:nth-child(2) > a");
        public static readonly By FetchDataNav = By.CssSelector("body > div.page > div.sidebar > div.collapse > ul > li:nth-child(3) > a");

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