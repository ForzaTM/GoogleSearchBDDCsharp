using GoogleAutoTest.Specs.Core;
using GoogleAutoTest.Specs.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace GoogleAutoTest.Specs
{
    [Binding]
    public class GoogleSearch1Steps
    {
        #region Variables

        private WebDriver _driver;
        private GoogleSearchPage _googleSearchPage;
        private SearchResultPage _resultPage;
        private string word;

        #endregion

        #region Steps

        [Given(@"I have entered searched word into the Google search")]
        public void GivenIHaveEnteredSearchedWordIntoTheGoogleSearch(Table table)
        {            
            var tableDetails = table.Rows.Select(x => new
            {
                GivenBrowser = x["GivenBrowser"],
                GivenWord = x["GivenWord"]                
            }).First();

            GoogleSearchByWordUsingSomeBrowser(tableDetails.GivenBrowser, tableDetails.GivenWord);            
        }

        [When(@"I press search button")]
        public void WhenIPressSearchButton()
        {
            _googleSearchPage.Search();
        }

        [Then(@"the title containss expected word")]
        public void ThenTheTitleContainssExpectedWord(Table table)
        {
            var tableDetails = table.Rows.Select(x => new
            {
                ExpectedWord = x["Word"]                
            }).First();

            CheckExpectedTitle(tableDetails.ExpectedWord);            
        }

        [Then(@"results on one of the result pages contain expected domain")]
        public void ThenResultsOnOneOfTheResultPagesContainExpectedDomain(Table table)
        {
            var tableDetails = table.Rows.Select(x => new
            {
                ExpectedDomain = x["Domain"],
                PagesToCheck = x["Pages"]
            }).First();

            CheckSearchPagesResults(tableDetails.ExpectedDomain, tableDetails.PagesToCheck);
        }

        #endregion

        #region Methods
        private void GoogleSearchByWordUsingSomeBrowser(string browser, string word)
        {
            this.word = word;
            _driver = new WebDriverManage().GetDriver(browser);
            _googleSearchPage = new GoogleSearchPage(_driver);
            _googleSearchPage.EnterWord(word);
        }

        private void CheckExpectedTitle(string word)
        {
            _resultPage = new SearchResultPage(_driver);
            _resultPage.FirstOfSearchResultsClick();
            Assert.IsTrue(_driver.Title.ToLower().Contains(word.ToLower()), "Title doesn't contain expected word " + word);                       
        }

        private void CheckSearchPagesResults(string domain, string amountOfPages)
        {
            for (int i = 1; i <= int.Parse(amountOfPages); i++)
            {
                _resultPage = new SearchResultPage(_driver);
                List<IWebElement> listOfElements = _resultPage.linksInSearchList();

                Log.Information($"Amount of links on page {i}: {listOfElements.Count}");

                bool result = false;

                foreach (IWebElement dom in listOfElements)
                {
                    var textInElement = dom.Text;

                    Log.Information($"Checking link: {textInElement} on the search page: " + i);

                    if (textInElement.Contains(domain))
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (!result && i != int.Parse(amountOfPages))
                {
                    Log.Information(domain + " is not present on page: " + i);
                    _resultPage.switcPageClick(i + 1);
                }
                else if (!result && i == int.Parse(amountOfPages))
                {
                    Log.Error(domain + " is not present on page: " + i);
                    Log.Error("No domains like: " + domain + " within pages: " + 1 + " - " + amountOfPages);
                    Assert.Fail("No domains like: " + domain + " within pages: " + 1 + " - " + amountOfPages);
                }
                else
                {                    
                    Log.Information(domain + " is present on page: " + i);
                    break;
                }
            }
        }

        #endregion

        #region Afterscenario
        
        [AfterScenario("GoogleSearch")]
        public void CloseDriver()
        {
            try
            {
                if (!(TestContext.CurrentContext.Result.FailCount > 0))
                {
                    Log.Information($"Test:{TestContext.CurrentContext.Test.Name} Finished successfully");
                }
                else
                {
                    Log.Error($"Test:{TestContext.CurrentContext.Test.Name} Failed");
                }
            }
            catch (Exception ex)
            {                
                throw ex;                
            }
            finally
            {
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Quit();
            }
        }

        #endregion        
    }
}
