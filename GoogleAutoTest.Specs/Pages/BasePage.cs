using GoogleAutoTest.Specs.Core;
using OpenQA.Selenium;

namespace GoogleAutoTest.Specs.Pages
{
    public class BasePage
    {
        private const int DELAY_IN_SEC = 20;

        protected WebDriver Driver;
        public WebElement MainElementOnPage;        

        public BasePage(WebDriver driver)
        {                              
            Driver = driver;
        }

        public bool IsDisplayed
        {
            get { return MainElementOnPage.Displayed; }
        }

        public void WaitForElement(WebDriver driver, By by)
        {
            MainElementOnPage.WaitForElement(driver, by);
        }
    }
}
