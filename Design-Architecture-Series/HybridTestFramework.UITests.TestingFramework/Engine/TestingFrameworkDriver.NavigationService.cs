// <copyright file="TestingFrameworkDriver.NavigationService.cs" company="Automate The Planet Ltd.">
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
using System;

namespace HybridTestFramework.UITests.TestingFramework.Engine
{
    public partial class TestingFrameworkDriver : INavigationService
    {
        public event EventHandler<PageEventArgs> Navigated;

        public string Url
        {
            get
            {
                return this.driver.ActiveBrowser.Url;                
            }
        }

        public string Title
        {
            get
            {
                return this.driver.ActiveBrowser.PageTitle;
            }
        }

        public void Navigate(
            string relativeUrl,
            string currentLocation,
            bool sslEnabled = false)
        {
            throw new NotImplementedException();
        }

        public void NavigateByAbsoluteUrl(
            string absoluteUrl,
            bool useDecodedUrl = true)
        {
            this.currentActiveBrowser.NavigateTo(absoluteUrl, true);
        }

        public void Navigate(
            string currentLocation,
            bool sslEnabled = false)
        {
            throw new NotImplementedException();
        }

        public void WaitForUrl(string url)
        {
            int timeout = this.BrowserSettings.PageLoadTimeout;
            this.driver.ActiveBrowser.WaitForUrl(url, false, timeout);
        }

        public void WaitForPartialUrl(string url)
        {
            int timeout = this.BrowserSettings.PageLoadTimeout;
            this.driver.ActiveBrowser.WaitForUrl(url, true, timeout);
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