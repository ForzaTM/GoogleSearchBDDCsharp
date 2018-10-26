using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAutoTest.Specs.Core
{
    public class Button : WebElement
    {
        public Button(WebElement element) : base(element)
        {

        }

        public string GetButtonState()
        {
            return !Enabled
                ? "Not Displayed"
                : GetAttribute("class") == "disabled" ? "Disabled" : "Enabled";
        }

        public void Click(IWebDriver driver, By element)
        {
            try
            {
                try
                {
                    Element.Click();
                }
                catch
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", Element);
                }

                WaitForElement(driver, element);
            }
            catch (Exception ex)
            {
                throw new Exception($"Element {Element.ToString()} not found for the click. Error {ex.Message}");
            }
        }
    }
}
