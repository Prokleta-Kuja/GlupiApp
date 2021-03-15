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

        public static Func<IWebDriver, IWebElement> ClickedAndExists(By locator, By exists)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    element.Click();
                    var expectedElement = driver.FindElement(exists);
                    return expectedElement;
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

        public static Func<IWebDriver, IWebElement> ElementExists(By locator)
        {
            return (driver) => { return driver.FindElement(locator); };
        }
    }
}