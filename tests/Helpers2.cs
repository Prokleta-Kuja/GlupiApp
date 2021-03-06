using System;
using System.Linq;
using OpenQA.Selenium;

namespace GlupiApp.Tests
{
    public static class Expected
    {
        public static Func<IWebDriver, IWebElement> Clicked(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    element.Click();
                    return element;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }
        public static Func<IWebDriver, bool> TextToBePresentInElementLocated(By locator, string text)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    var elementText = element.Text;
                    return elementText.Contains(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }
    }
}