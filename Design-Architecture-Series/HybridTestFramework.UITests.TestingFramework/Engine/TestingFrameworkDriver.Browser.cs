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
                return _browserSettings;
            }
        }

        public string SourceString
        {
            get
            {
                return _driver.ActiveBrowser.ViewSourceString;
            }
        }

        public void SwitchToFrame(IFrame newContainer)
        {
            RefreshDomTree();
            _currentActiveBrowser =
                _driver.ActiveBrowser.Frames[newContainer.Name];
        }

        public IFrame GetFrameByName(string frameName)
        {
            return new TestStudioFrame(frameName);
        }

        public void SwitchToDefault()
        {
            RefreshDomTree();
            _currentActiveBrowser = _originalBrowser;
        }

        public void Quit()
        {
            if (Manager.Current != null)
            {
                Manager.Current.Dispose();
            }
            if (_driver != null)
            {
                _driver.Dispose();
                _driver = null;
            }
        }

        public void WaitForAjax()
        {
            _driver.ActiveBrowser.WaitForAjax(_browserSettings.ScriptTimeout);
        }

        public void WaitUntilReady()
        {
            _driver.ActiveBrowser.WaitUntilReady();
        }

        public void FullWaitUntilReady()
        {
            WaitForAjax();
            WaitUntilReady();
        }

        public void RefreshDomTree()
        {
            _driver.ActiveBrowser.RefreshDomTree();
        }

        public void ClickBackButton()
        {
            _driver.ActiveBrowser.GoBack();
        }

        public void ClickForwardButton()
        {
            _driver.ActiveBrowser.GoForward();
        }

        public void LaunchNewBrowser()
        {
            if (_driver != null)
            {
                _driver.LaunchNewBrowser();
            }
        }

        public void MaximizeBrowserWindow()
        {
            if (!_currentActiveBrowser.Window.IsMaximized)
            {
                _currentActiveBrowser.Window.Maximize();
            }
        }

        public void ClickRefresh()
        {
            _driver.ActiveBrowser.Refresh();
        }
    }
}