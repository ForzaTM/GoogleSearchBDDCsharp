using GoogleAutoTest.Specs.Core;
using OpenQA.Selenium;
using System.Collections.Generic;


namespace GoogleAutoTest.Specs.Pages
{
    class SearchResultPage: BasePage
    {
        TextElement _searchBar;
        WebElement _googleSearchResults;
        public WebElement _firstOfGoogleSearchResults;
        WebElement _switchPage;
             
        private const string SEARCHFIELD_PATH = ".//input[contains(@role,'combobox')]";
        private const string SEARCHRESULT_PATH = "search";
        private const string FIRSTOFSEARCHRESULTS_PATH = "h3.LC20lb";      
        private const string LINKSINSEARCHSECTION_PATH = "cite.iUh30";

        public static By searchField => By.XPath(SEARCHFIELD_PATH);
        public static By googleSearchRes => By.Id(SEARCHRESULT_PATH);
        public static By firstOfGoogleSearchRes => By.CssSelector(FIRSTOFSEARCHRESULTS_PATH);     
        public static By linksInSS => By.CssSelector(LINKSINSEARCHSECTION_PATH);
        public static By MainElementInfo => By.CssSelector(FIRSTOFSEARCHRESULTS_PATH);

        public SearchResultPage(WebDriver driver) : base(driver)            
        {
            _searchBar = new TextElement(Driver.FindElement(searchField));
            _googleSearchResults = new WebElement(Driver.FindElement(googleSearchRes));
            _firstOfGoogleSearchResults = new WebElement(Driver.FindElement(firstOfGoogleSearchRes));                    

            MainElementOnPage = new WebElement(Driver.FindElement(MainElementInfo));
        }

        public void switcPageClick(int pagenum)
        {
            _switchPage = Driver.FindElement(By.XPath(".//a[contains(@aria-label, 'Page " + pagenum + "')]"));
            _switchPage.Click();            
        }

        public void FirstOfSearchResultsClick()
        {
            WaitForElement(Driver, firstOfGoogleSearchRes);
            _firstOfGoogleSearchResults.Click();
        }
        public List<IWebElement> linksInSearchList()
        {            
            return new List<IWebElement>(Driver.FindElements(linksInSS));
        }
    }
}
