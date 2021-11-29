using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

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

        public bool ValidarConteudoUrl(string conteudo)
        {
            return Wait.Until(ExpectedConditions.UrlContains(conteudo));
        }

        public void ClicarLinkPorTexto(string linkText)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText))).Click();
        }

        public void ClicarBotaoPorId(string botaoId)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(botaoId))).Click();
        }

        public void ClicarPorXpath(string xPath)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath))).Click();
        }

        public IWebElement ObterElementoPorClasseCss(string classeCss)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(classeCss)));
        }

        public IWebElement ObterElementoPorXpath(string xPath)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
        }

        public void PreencherTextBoxPorId(string idCampo, string valorCampo)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo))).SendKeys(valorCampo);
        }

        public void PreencherDropDownPorId(string idCampo, string valorCampo)
        {
            new SelectElement(Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo)))).SelectByValue(valorCampo);
        }

        public string ObterTextoElementoPorClasseCss(string classeCss)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(classeCss))).Text;
        }

        public string ObterTextoElementoPorId(string idCampo)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo))).Text;
        }

        public string ObterValorTextBoxPorId(string idCampo)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(idCampo))).GetAttribute("value");
        }

        public IEnumerable<IWebElement> ObterListaPorClasseCss(string classeCss)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(classeCss)));
        }

        public bool ValidarSeElementoExistePorId(string idCampo)
        {
            return ElementoExistente(By.Id(idCampo));
        }

        public void VoltarNavegacao(int vezes = 1)
        {
            for (var i = 0; i < vezes; i++)
            {
                WebDriver.Navigate().Back();
            }
        }

        public void ObterScreenshot(string nome)
        {
            SalvarScreenshot(WebDriver.TakeScreenshot(), $"{DateTime.Now.ToFileTime()}_{nome}.png");
        }

        private void SalvarScreenshot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile($"{Configuration.FolderPicture}{fileName}", ScreenshotImageFormat.Png);
        }

        private bool ElementoExistente(By by)
        {
            try
            {
                WebDriver.FindElement(by);

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
