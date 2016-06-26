using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Web;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : INavigationService
    {
        public event EventHandler<Core.Events.PageEventArgs> Navigated;

        public string Url
        {
            get
            {
                return this.driver.Url;
            }
        }

        public string Title
        {
            get
            {
                return this.driver.Title;
            }
        }

        public void Navigate(string relativeUrl, string currentLocation, bool sslEnabled = false)
        {
            throw new NotImplementedException();
        }

        public void NavigateByAbsoluteUrl(string absoluteUrl, bool useDecodedUrl = true)
        {
            var urlToNavigateTo = absoluteUrl;
            if (useDecodedUrl)
            {
                urlToNavigateTo = HttpUtility.UrlDecode(urlToNavigateTo);
            }
            this.driver.Navigate().GoToUrl(urlToNavigateTo);
        }

        public void Navigate(string currentLocation, bool sslEnabled = false)
        {
            throw new NotImplementedException();
        }

        public void WaitForUrl(string url)
        {
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.browserSettings.ScriptTimeout));
            wait.PollingInterval = TimeSpan.FromSeconds(0.8);
            wait.Until(x => string.Compare(x.Url, url, StringComparison.InvariantCultureIgnoreCase) == 0);
            this.RaiseNavigated(this.driver.Url);
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3)); 
        }

        public void WaitForPartialUrl(string url)
        {
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
            WebDriverWait wait = new WebDriverWait(
                this.driver, 
                TimeSpan.FromSeconds(this.browserSettings.ScriptTimeout));
            wait.PollingInterval = TimeSpan.FromSeconds(0.8);
            wait.Until(x => x.Url.Contains(url) == true);
            this.RaiseNavigated(this.driver.Url);
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3)); 
        }

        private void RaiseNavigated(string url)
        {
            if (this.Navigated != null)
            {
                this.Navigated(this, new PageEventArgs(url));
            }
        }
    }
}