// <copyright file="App.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace HuddlePageObjectsAppDesignPattern.Core
{
    public class App : IDisposable
    {
        private IWebDriver _driver;

        public App(BrowserType browserType = BrowserType.Firefox)
        {
            StartBrowser(browserType);
        }

        public void Dispose() => _driver.Dispose();

        public TPage Create<TPage>()
            where TPage : Page
        {
            var constructor = typeof(TPage).GetTypeInfo().GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();
            var page = constructor.Invoke(new object[] { _driver }) as TPage;
            return page;
        }

        public TPage GoTo<TPage>()
            where TPage : NavigatablePage
        {
            var constructor = typeof(TPage).GetTypeInfo().GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();
            var page = constructor.Invoke(new object[] { _driver }) as TPage;
            page.GoTo();
            return page;
        }

        private void StartBrowser(BrowserType browserType = BrowserType.Firefox)
        {
            switch (browserType)
            {
                case BrowserType.Firefox:
                    _driver = new FirefoxDriver();
                    break;
                case BrowserType.InternetExplorer:
                    break;
                case BrowserType.Chrome:
                    break;
                default:
                    throw new ArgumentException("You need to set a valid browser type.");
            }
        }
    }
}
