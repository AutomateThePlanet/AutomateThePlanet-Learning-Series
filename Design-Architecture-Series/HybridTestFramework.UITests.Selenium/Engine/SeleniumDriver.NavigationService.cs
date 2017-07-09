// <copyright file="seleniumdriver.navigationservice.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
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
            WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.browserSettings.ScriptTimeout));
            wait.PollingInterval = TimeSpan.FromSeconds(0.8);
            wait.Until(x => string.Compare(x.Url, url, StringComparison.InvariantCultureIgnoreCase) == 0);
            this.RaiseNavigated(this.driver.Url);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        public void WaitForPartialUrl(string url)
        {
            WebDriverWait wait = new WebDriverWait(
                this.driver, 
                TimeSpan.FromSeconds(this.browserSettings.ScriptTimeout));
            wait.PollingInterval = TimeSpan.FromSeconds(0.8);
            wait.Until(x => x.Url.Contains(url) == true);
            this.RaiseNavigated(this.driver.Url);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
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