using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GlupiApp.Tests
{
    public class Infrastructure
    {
        public static IWebDriver GetInstance(int width = 1366, int height = 768)
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
            driver.Manage().Window.Size = new System.Drawing.Size(width, height);
            return driver;
        }
        public static void DisposeDriver(IWebDriver driver)
        {
            driver.Quit();
        }
    }
}