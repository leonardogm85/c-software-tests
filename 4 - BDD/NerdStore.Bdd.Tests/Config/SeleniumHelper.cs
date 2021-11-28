using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace NerdStore.Bdd.Tests.Config
{
    public class SeleniumHelper : IDisposable
    {
        public readonly ConfigurationHelper Configuration;

        public IWebDriver WebDriver;
        public WebDriverWait Wait;

        public SeleniumHelper(Browser browser, ConfigurationHelper configuration, bool headlees = true)
        {
            Configuration = configuration;
            WebDriver = WebDriverFactory.CreateWebDriver(browser, Configuration.WebDrivers, headlees);
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public string ObterUrl()
        {
            return WebDriver.Url;
        }

        public void IrParaUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void ClicarLinkPorTexto(string linkText)
        {
            Wait.Until(webDriver => webDriver.FindElement(By.LinkText(linkText))).Click();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
