using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Reflection;
using System.Configuration;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Edge;
using Serilog;

namespace GoogleAutoTest.Specs.Core
{
    class WebDriverManage
    {
        private static IWebDriver driver;
        private static string _executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _urlToGoogleSearch = ConfigurationSettings.AppSettings["GoogleSearchUrl"];
      
        public WebDriver GetDriver(string driverName)
        {
            var log = new Logger();

            switch (driverName)
            {
                case ("Chrome"):
                    //new DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new WebDriver(new ChromeDriver());break;                    
                case ("IE"):
                    //new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    driver = new WebDriver(new InternetExplorerDriver()); break;
                case ("Firefox"):
                    //new DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new WebDriver(new FirefoxDriver()); break;                
            }
            if (driver != null)
            {
                driver.Manage().Window.Maximize();
                driver.Url = _urlToGoogleSearch;
                Log.Logger.Information($"Driver: {driverName} was used to run test");
            }            
            return (WebDriver)driver;            
        }
    }   
}

