using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GlupiApp.Tests.Enums;
using SeleniumExtras.WaitHelpers;
using GlupiApp.Tests.Mappings;

namespace GlupiApp.Tests.PageObjectModels
{
    public class CountersPage : BasePage
    {
        public static readonly By Button1 = By.Id("prvi-button");
        public static readonly By Button2 = By.Id("drugi-button");
        public static readonly By Button3 = By.Id("treci-button");
        public static readonly By Result1 = By.Id("result-1");
        public static readonly By Result2 = By.Id("result-2");
        public static readonly By Result3 = By.Id("result-3");

        public CountersPage(IWebDriver driver, WebDriverWait waiter) : base(driver, waiter) { }

        public void Open()
        {
            Driver.Navigate().GoToUrl("http://localhost:5000/counter");

            Waiter.Until(Expected.TextToBePresentInElementLocated(PageTitle, "Counters"));
        }
    }
}