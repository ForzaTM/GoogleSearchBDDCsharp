using GoogleAutoTest.Specs.Core;
using OpenQA.Selenium;

namespace GoogleAutoTest.Specs.Pages
{
    public class GoogleSearchPage: BasePage
    {      
        private const string SEARCH_TEXT_FIELD_PATH = ".//input[contains(@role,'combobox')]";

        private TextElement _searchTextFieldElement;        

        public static By MainElementInfo => By.XPath(SEARCH_TEXT_FIELD_PATH);
        public GoogleSearchPage(WebDriver driver) : base(driver)
        {
            _searchTextFieldElement = new TextElement(Driver.FindElement(By.XPath(SEARCH_TEXT_FIELD_PATH), false));           

            MainElementOnPage = new TextElement(Driver.FindElement(MainElementInfo));
        }

        public void EnterWord(string str)
        {
            _searchTextFieldElement.SendKeys(str);            
        }

        public void Search()
        {
            _searchTextFieldElement.SendKeys(Keys.Enter);
        }
    }
}
