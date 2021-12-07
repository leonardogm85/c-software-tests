using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace NerdStore.Bdd.Tests.Config
{
    public static class ExpectedConditions
    {
        public static Func<IWebDriver, bool> TitleIs(string title)
        {
            return (driver) => { return title == driver.Title; };
        }

        public static Func<IWebDriver, bool> TitleContains(string title)
        {
            return (driver) => { return driver.Title.Contains(title); };
        }

        public static Func<IWebDriver, bool> UrlToBe(string url)
        {
            return (driver) => { return driver.Url.ToLowerInvariant().Equals(url.ToLowerInvariant()); };
        }

        public static Func<IWebDriver, bool> UrlContains(string fraction)
        {
            return (driver) => { return driver.Url.ToLowerInvariant().Contains(fraction.ToLowerInvariant()); };
        }

        public static Func<IWebDriver, bool> UrlMatches(string regex)
        {
            return (driver) =>
            {
                var currentUrl = driver.Url;
                var pattern = new Regex(regex, RegexOptions.IgnoreCase);
                var match = pattern.Match(currentUrl);
                return match.Success;
            };
        }

        public static Func<IWebDriver, IWebElement> ElementExists(By locator)
        {
            return (driver) => { return driver.FindElement(locator); };
        }

        public static Func<IWebDriver, IWebElement> ElementIsVisible(By locator)
        {
            return (driver) =>
            {
                try
                {
                    return ElementIfVisible(driver.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, ReadOnlyCollection<IWebElement>> VisibilityOfAllElementsLocatedBy(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var elements = driver.FindElements(locator);
                    if (elements.Any(element => !element.Displayed))
                    {
                        return null;
                    }

                    return elements.Any() ? elements : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, ReadOnlyCollection<IWebElement>> VisibilityOfAllElementsLocatedBy(ReadOnlyCollection<IWebElement> elements)
        {
            return (driver) =>
            {
                try
                {
                    if (elements.Any(element => !element.Displayed))
                    {
                        return null;
                    }

                    return elements.Any() ? elements : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, ReadOnlyCollection<IWebElement>> PresenceOfAllElementsLocatedBy(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var elements = driver.FindElements(locator);
                    return elements.Any() ? elements : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, bool> TextToBePresentInElement(IWebElement element, string text)
        {
            return (driver) =>
            {
                try
                {
                    var elementText = element.Text;
                    return elementText.Contains(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
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

        public static Func<IWebDriver, bool> TextToBePresentInElementValue(IWebElement element, string text)
        {
            return (driver) =>
            {
                try
                {
                    var elementValue = element.GetAttribute("value");
                    if (elementValue != null)
                    {
                        return elementValue.Contains(text);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, bool> TextToBePresentInElementValue(By locator, string text)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    var elementValue = element.GetAttribute("value");
                    if (elementValue != null)
                    {
                        return elementValue.Contains(text);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, IWebDriver> FrameToBeAvailableAndSwitchToIt(string frameLocator)
        {
            return (driver) =>
            {
                try
                {
                    return driver.SwitchTo().Frame(frameLocator);
                }
                catch (NoSuchFrameException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, IWebDriver> FrameToBeAvailableAndSwitchToIt(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var frameElement = driver.FindElement(locator);
                    return driver.SwitchTo().Frame(frameElement);
                }
                catch (NoSuchFrameException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, bool> InvisibilityOfElementLocated(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        public static Func<IWebDriver, bool> InvisibilityOfElementWithText(By locator, string text)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    var elementText = element.Text;
                    if (string.IsNullOrEmpty(elementText))
                    {
                        return true;
                    }

                    return !elementText.Equals(text);
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        public static Func<IWebDriver, IWebElement> ElementToBeClickable(By locator)
        {
            return (driver) =>
            {
                var element = ElementIfVisible(driver.FindElement(locator));
                try
                {
                    if (element != null && element.Enabled)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, IWebElement> ElementToBeClickable(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    if (element != null && element.Displayed && element.Enabled)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, bool> StalenessOf(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    return element == null || !element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        public static Func<IWebDriver, bool> ElementToBeSelected(IWebElement element)
        {
            return ElementSelectionStateToBe(element, true);
        }

        public static Func<IWebDriver, bool> ElementToBeSelected(IWebElement element, bool selected)
        {
            return (driver) =>
            {
                return element.Selected == selected;
            };
        }

        public static Func<IWebDriver, bool> ElementSelectionStateToBe(IWebElement element, bool selected)
        {
            return (driver) =>
            {
                return element.Selected == selected;
            };
        }

        public static Func<IWebDriver, bool> ElementToBeSelected(By locator)
        {
            return ElementSelectionStateToBe(locator, true);
        }

        public static Func<IWebDriver, bool> ElementSelectionStateToBe(By locator, bool selected)
        {
            return (driver) =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Selected == selected;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, IAlert> AlertIsPresent()
        {
            return (driver) =>
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            };
        }

        public static Func<IWebDriver, bool> AlertState(bool state)
        {
            return (driver) =>
            {
                var alertState = false;
                try
                {
                    driver.SwitchTo().Alert();
                    alertState = true;
                    return alertState == state;
                }
                catch (NoAlertPresentException)
                {
                    alertState = false;
                    return alertState == state;
                }
            };
        }

        private static IWebElement ElementIfVisible(IWebElement element)
        {
            return element.Displayed ? element : null;
        }
    }
}
