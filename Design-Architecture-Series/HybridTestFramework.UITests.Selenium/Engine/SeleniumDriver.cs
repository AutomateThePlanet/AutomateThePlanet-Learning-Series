// <copyright file="seleniumdriver.cs" company="Automate The Planet Ltd.">
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
using HybridTestFramework.UITests.Core.Enums;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IDriver
    {
        private IWebDriver driver;
        private IUnityContainer container;
        private BrowserSettings browserSettings;
        private readonly ElementFinderService elementFinderService;

        public SeleniumDriver(IUnityContainer container, BrowserSettings browserSettings)
        {
            this.container = container;
            this.browserSettings = browserSettings;
            this.ResolveBrowser(browserSettings);
            this.elementFinderService = new ElementFinderService(container);
            driver.Manage().Timeouts().ImplicitlyWait(
                TimeSpan.FromSeconds(browserSettings.ElementsWaitTimeout));
        }

        private void ResolveBrowser(BrowserSettings browserSettings)
        {
            switch (browserSettings.Type)
            {
                case Browsers.NotSet:
                    break;
                case Browsers.Chrome:
                    break;
                case Browsers.Firefox:
                    this.driver = new FirefoxDriver();
                    break;
                case Browsers.InternetExplorer:
                    break;
                case Browsers.Safari:
                    break;
                case Browsers.NoBrowser:
                    break;
                default:
                    break;
            }
        }       
    }
}