// <copyright file="seleniumdriver.browser.cs" company="Automate The Planet Ltd.">
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
using System;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IBrowser
    {
        public BrowserSettings BrowserSettings
        {
            get
            {
                return this.browserSettings;
            }
        }

        public string SourceString
        {
            get
            {
               return this.driver.PageSource;
            }
        }

        public void SwitchToFrame(IFrame newContainer)
        {
            driver.SwitchTo().Frame(newContainer.Name);
        }

        public IFrame GetFrameByName(string frameName)
        {
            return new SeleniumFrame(frameName);
        }

        public void SwitchToDefault()
        {
            this.driver.SwitchTo().DefaultContent();
        }

        public void Quit()
        {
            this.driver.Quit();
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
            this.driver.Navigate().Back();
        }

        public void ClickForwardButton()
        {
            this.driver.Navigate().Forward();
        }

        public void LaunchNewBrowser()
        {
            throw new NotImplementedException();
        }

        public void MaximizeBrowserWindow()
        {
            driver.Manage().Window.Maximize();
        }

        public void ClickRefresh()
        {
            driver.Navigate().Refresh();
        }
    }
}