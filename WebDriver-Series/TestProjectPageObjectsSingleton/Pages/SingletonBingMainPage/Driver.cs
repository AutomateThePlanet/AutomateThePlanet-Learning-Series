﻿// <copyright file="Driver.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
// <site>https://automatetheplanet.com/</site>

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TestProjectPageObjectsSingleton.Pages.SingletonBingMainPage
{
    public static class Driver
    {
        private static WebDriverWait _browserWait;

        private static IWebDriver _browser;

        public static IWebDriver GetBrowser()
        {
            if (_browser == null)
            {
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method Start.");
            }

            return _browser;
        }

        private static void SetBrowser(IWebDriver value)
        {
            _browser = value;
        }

        public static WebDriverWait GetBrowserWait()
        {
            if (_browserWait == null || _browser == null)
            {
                throw new NullReferenceException("The WebDriver browser wait instance was not initialized. You should first call the method Start.");
            }

            return _browserWait;
        }

        private static void SetBrowserWait(WebDriverWait value)
        {
            _browserWait = value;
        }

        public static void StartBrowser(BrowserType browserType = BrowserType.Firefox, int defaultTimeOut = 30)
        {
            switch (browserType)
            {
                case BrowserType.Firefox:
                    SetBrowser(new FirefoxDriver());
                    break;
                case BrowserType.InternetExplorer:
                    break;
                case BrowserType.Chrome:
                    break;
                default:
                    throw new ArgumentException("You need to set a valid browser type.");
            }

            SetBrowserWait(new WebDriverWait(GetBrowser(), TimeSpan.FromSeconds(defaultTimeOut)));
        }

        public static void StopBrowser()
        {
            GetBrowser().Quit();
            SetBrowser(null);
            SetBrowserWait(null);
        }
    }
}
