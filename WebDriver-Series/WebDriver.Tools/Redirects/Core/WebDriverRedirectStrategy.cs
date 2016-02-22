using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WebDriver.Tools.Redirects.Core
{
    public class WebDriverRedirectStrategy : IRedirectStrategy
    {
        private IWebDriver driver;

        public void Initialize()
        {
            this.driver = new FirefoxDriver();
        }

        public void Dispose()
        {
            this.driver.Quit();
        }

        public string NavigateToFromUrl(string fromUrl)
        {
            this.driver.Navigate().GoToUrl(fromUrl);
            string currentSitesUrl = this.driver.Url;

            return currentSitesUrl;
        }
    }
}
