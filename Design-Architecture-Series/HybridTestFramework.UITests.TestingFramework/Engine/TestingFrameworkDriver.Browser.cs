// <copyright file="TestingFrameworkDriver.Browser.cs" company="Automate The Planet Ltd.">
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

using ArtOfTest.WebAii.Core;
using HybridTestFramework.UITests.Core;
using System;

namespace HybridTestFramework.UITests.TestingFramework.Engine
{
    public partial class TestingFrameworkDriver : IBrowser
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
                return this.driver.ActiveBrowser.ViewSourceString;
            }
        }

        public void SwitchToFrame(IFrame newContainer)
        {
            this.RefreshDomTree();
            this.currentActiveBrowser =
                this.driver.ActiveBrowser.Frames[newContainer.Name];
        }

        public IFrame GetFrameByName(string frameName)
        {
            return new TestStudioFrame(frameName);
        }

        public void SwitchToDefault()
        {
            this.RefreshDomTree();
            this.currentActiveBrowser = this.originalBrowser;
        }

        public void Quit()
        {
            if (Manager.Current != null)
            {
                Manager.Current.Dispose();
            }
            if (this.driver != null)
            {
                this.driver.Dispose();
                this.driver = null;
            }
        }

        public void WaitForAjax()
        {
            this.driver.ActiveBrowser.WaitForAjax(browserSettings.ScriptTimeout);
        }

        public void WaitUntilReady()
        {
            this.driver.ActiveBrowser.WaitUntilReady();
        }

        public void FullWaitUntilReady()
        {
            this.WaitForAjax();
            this.WaitUntilReady();
        }

        public void RefreshDomTree()
        {
            this.driver.ActiveBrowser.RefreshDomTree();
        }

        public void ClickBackButton()
        {
            this.driver.ActiveBrowser.GoBack();
        }

        public void ClickForwardButton()
        {
            this.driver.ActiveBrowser.GoForward();
        }

        public void LaunchNewBrowser()
        {
            this.driver.LaunchNewBrowser();
        }

        public void MaximizeBrowserWindow()
        {
            if (!this.currentActiveBrowser.Window.IsMaximized)
            {
                this.currentActiveBrowser.Window.Maximize();
            }
        }

        public void ClickRefresh()
        {
            this.driver.ActiveBrowser.Refresh();
        }
    }
}