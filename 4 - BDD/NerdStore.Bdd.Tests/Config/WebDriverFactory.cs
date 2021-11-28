using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace NerdStore.Bdd.Tests.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser, string caminhoDriver, bool headlees)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Chrome:
                    var optionsChrome = new ChromeOptions();

                    if (headlees)
                    {
                        optionsChrome.AddArguments("--headlees");
                    }

                    webDriver = new ChromeDriver(caminhoDriver, optionsChrome);
                    break;
                case Browser.Firefox:
                    var optionsFirefox = new FirefoxOptions();

                    if (headlees)
                    {
                        optionsFirefox.AddArguments("--headlees");
                    }

                    webDriver = new FirefoxDriver(caminhoDriver, optionsFirefox);
                    break;
            }

            return webDriver;
        }
    }
}
