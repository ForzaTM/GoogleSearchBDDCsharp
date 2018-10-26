using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAutoTest.Specs.Core
{
    public class WebDriver : IWebDriver
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public WebDriver(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public string CurrentWindowHandle
        {
            get
            {
                return _driver.CurrentWindowHandle;
            }
        }

        public string PageSource
        {
            get
            {
                return _driver.PageSource;
            }
        }
        public string Title
        {
            get
            {
                return _driver.Title;
            }
        }
        public string Url
        {
            get
            {
                return _driver.Url;
            }

            set
            {
                _driver.Url = value;
            }
        }
        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                return _driver.WindowHandles;
            }
        }

        public void Close()
        {
            _driver.Close();
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
        public WebElement FindElement(By by, bool wait = true)
        {
            try
            {
                if (wait) _wait.Until(ExpectedConditions.ElementIsVisible(by));
                return new WebElement(_driver.FindElement(by));
            }
            catch
            {
                if (wait)
                    throw new Exception($"Element was not found and page seems to be loaded. Element {by.ToString()}");

                return null;
            }
        }

        public List<WebElement> FindElements(By by)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(by));
                return _driver.FindElements(by).Select(e => new WebElement(e)).ToList();
            }
            catch
            {
                throw new Exception($"Elements were not found and page seems to be loaded. Element {by.ToString()}");
            }
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        IWebElement ISearchContext.FindElement(By by)
        {
            try
            {
                return new WebElement(_driver.FindElement(by));
            }
            catch (Exception ex)
            {
                throw new Exception($"Element by {by} not found on the page. Error: {ex.Message}");
            }
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            try
            {
                return _driver.FindElements(by);
            }
            catch (Exception ex)
            {
                throw new Exception($"Elements by {by} not found on the page. Error: {ex.Message}");
            }
        }
    }
}
