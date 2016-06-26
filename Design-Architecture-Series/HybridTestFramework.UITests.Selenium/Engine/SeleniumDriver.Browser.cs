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