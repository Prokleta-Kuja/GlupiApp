using System;
using System.Collections.Generic;
using GlupiApp.Tests.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace GlupiApp.Tests
{
    public class Infrastructure
    {
    

        private static Dictionary<Resolution, System.Drawing.Size> resolutions = new Dictionary<Resolution, System.Drawing.Size>{
            {Resolution.Mobile, new System.Drawing.Size(640, 610)},
            {Resolution.Tablet, new System.Drawing.Size(1024, 768)},
            {Resolution.Desktop, new System.Drawing.Size(1366, 768)},
        };

    

        public static IWebDriver GetInstance(Resolution resolution = Resolution.Desktop)
        {
            var driver = GetLocal();
            driver.Manage().Window.Size = resolutions[resolution];
            return driver;
        }


        private static IWebDriver GetRemote()
        {
            var options = new ChromeOptions();
            var driver = new RemoteWebDriver(new Uri("http://selenium-hub.grid:4444"), options);
            return driver;
        }
        private static IWebDriver GetLocal()
        {
            // var chromDriverPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "drivers");
            var options = new ChromeOptions();
            // options.AddArgument("--no-sandbox");
            // options.AddArgument("--headless");
            // options.AddArgument("--incognito");
            // options.AddArgument("--disable-dev-shm-usage");
            // options.AddArgument("--disable-gpu");
            options.AddArgument("--remote-debugging-port=9225");

            var driver = new ChromeDriver(options);
            return driver;
        }
        public static void DisposeDriver(IWebDriver driver)
        {
            driver.Quit();
        }
    }
}