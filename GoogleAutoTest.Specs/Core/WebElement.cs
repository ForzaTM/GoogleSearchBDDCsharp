using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleAutoTest.Specs.Core
{
    public class WebElement : IWebElement
    {
        private const int DELAY_IN_SEC = 20;        
        protected IWebElement Element;        
 
        public WebElement(IWebElement element)
        {                  
            Element = element;             
        }       

        public bool Displayed
        {
            get
            {
                return Element == null ? false : Element.Displayed;
            }
        }

        public bool Enabled
        {
            get
            {
                return Element == null ? false : Element.Enabled;
            }
        }

        public Point Location
        {
            get
            {
                return Element.Location;
            }
        }

        public bool Selected
        {
            get
            {
                return Element == null ? false : Element.Selected;
            }
        }

        public Size Size
        {
            get
            {
                return Element.Size;
            }
        }

        public string TagName
        {
            get
            {
                return Element == null ? null : Element.TagName;
            }
        }

        public string Text
        {
            get
            {
                return Element == null ? null : Element.Text;
            }
        }

        public void Clear()
        {
            Element?.Clear();
        }

        public virtual void Click()
        {                 
            try
            {
                Element.Click();                                               
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Element: {element} failed to Click", Element);
                throw new Exception($"Element {Element.ToString()} not found for the click. Error {ex.Message}");
            }

            Log.Logger.Information("Element: {element} Click successfull", Element);
        }

        public void WaitForElement(IWebDriver driver, By by)
        {
            Log.Logger.Information($"Wait for the {by.ToString()} element");            

            bool completed = false;
            int counter = 0;
            int count = DELAY_IN_SEC * 2 + 1;
            do
            {
                try
                {
                    Thread.Sleep(500);

                    var element = driver.FindElement(by);

                    if (element?.Displayed == true)
                    {
                        completed = true;
                    }

                }
                catch { }

                counter++;
            }
            while (!completed & counter <= count);
        }

        public string GetAttribute(string attributeName)
        {
            return Element == null ? null : Element.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return Element == null ? null : Element.GetCssValue(propertyName);
        }

        public void SendKeys(string text)
        {
            Element?.SendKeys(text);
        }

        public void Submit()
        {
            Element?.Submit();
        }

        IWebElement ISearchContext.FindElement(By by)
        {
            try
            {                
                return new WebElement(Element.FindElement(by));                
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Element by {by} not found on the page. Error: " + ex.ToString());               
                return null;
            }
        }      

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return Element.FindElements(by);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Elements by {by} not found on the page. Error: " + ex.ToString());
                return null;
            }
        }

        public string GetProperty(string propertyName)
        {
            try
            {
                return Element.GetProperty(propertyName);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Cannot get property {propertyName}. Error: " + ex.ToString());                
                return null;
            }
        }
    }
}
