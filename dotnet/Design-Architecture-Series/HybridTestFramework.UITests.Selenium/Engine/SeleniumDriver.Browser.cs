// <copyright file="seleniumdriver.browser.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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
using System;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IBrowser
    {
        public BrowserSettings BrowserSettings
        {
            get
            {
                return _browserSettings;
            }
        }

        public string SourceString
        {
            get
            {
               return _driver.PageSource;
            }
        }

        public void SwitchToFrame(IFrame newContainer)
        {
            _driver.SwitchTo().Frame(newContainer.Name);
        }

        public IFrame GetFrameByName(string frameName)
        {
            return new SeleniumFrame(frameName);
        }

        public void SwitchToDefault()
        {
            _driver.SwitchTo().DefaultContent();
        }

        public void Quit()
        {
            _driver.Quit();
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
            _driver.Navigate().Back();
        }

        public void ClickForwardButton()
        {
            _driver.Navigate().Forward();
        }

        public void LaunchNewBrowser()
        {
            throw new NotImplementedException();
        }

        public void MaximizeBrowserWindow()
        {
            _driver.Manage().Window.Maximize();
        }

        public void ClickRefresh()
        {
            _driver.Navigate().Refresh();
        }
    }
}