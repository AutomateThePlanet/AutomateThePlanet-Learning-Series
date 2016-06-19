using HybridTestFramework.UITests.Core;
// <copyright file="SeleniumDriver.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;

namespace CreateHybridTestFrameworkInterfaceContracts.HybridVersion
{
    public class SeleniumDriver : IDriver
    {
        public TElement Find<TElement>(By by) where TElement : class, HybridTestFramework.UITests.Core.Controls.IElement
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> FindAll<TElement>(By by) where TElement : class, HybridTestFramework.UITests.Core.Controls.IElement
        {
            throw new NotImplementedException();
        }

        public bool IsElementPresent(By by)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<HybridTestFramework.UITests.Core.Events.PageEventArgs> Navigated;

        public string Url
        {
            get { throw new NotImplementedException(); }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public void Navigate(string relativeUrl, string currentLocation, bool sslEnabled = false)
        {
            throw new NotImplementedException();
        }

        public void NavigateByAbsoluteUrl(string absoluteUrl, bool useDecodedUrl = true)
        {
            throw new NotImplementedException();
        }

        public void Navigate(string currentLocation, bool sslEnabled = false)
        {
            throw new NotImplementedException();
        }

        public void WaitForUrl(string url)
        {
            throw new NotImplementedException();
        }

        public void WaitForPartialUrl(string url)
        {
            throw new NotImplementedException();
        }

        public string GetCookie(string host, string cookieName)
        {
            throw new NotImplementedException();
        }

        public void AddCookie(string cookieName, string cookieValue, string host)
        {
            throw new NotImplementedException();
        }

        public void DeleteCookie(string cookieName)
        {
            throw new NotImplementedException();
        }

        public void CleanAllCookies()
        {
            throw new NotImplementedException();
        }

        public void Handle(Action action = null, DialogButton dialogButton = DialogButton.OK)
        {
            throw new NotImplementedException();
        }

        public void HandleLogonDialog(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void Upload(string filePath)
        {
            throw new NotImplementedException();
        }

        public string InvokeScript(string script)
        {
            throw new NotImplementedException();
        }

        public BrowserSettings BrowserSettings
        {
            get { throw new NotImplementedException(); }
        }

        public string SourceString
        {
            get { throw new NotImplementedException(); }
        }

        public void SwitchToFrame(IFrame newContainer)
        {
            throw new NotImplementedException();
        }

        public IFrame GetFrameByName(string frameName)
        {
            throw new NotImplementedException();
        }

        public void SwitchToDefault()
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public void WaitForAjax()
        {
            throw new NotImplementedException();
        }

        public void WaitUntilReady()
        {
            throw new NotImplementedException();
        }

        public void FullWaitUntilReady()
        {
            throw new NotImplementedException();
        }

        public void RefreshDomTree()
        {
            throw new NotImplementedException();
        }

        public void ClickBackButton()
        {
            throw new NotImplementedException();
        }

        public void ClickForwardButton()
        {
            throw new NotImplementedException();
        }

        public void LaunchNewBrowser()
        {
            throw new NotImplementedException();
        }

        public void MaximizeBrowserWindow()
        {
            throw new NotImplementedException();
        }

        public void ClickRefresh()
        {
            throw new NotImplementedException();
        }
    }
}
