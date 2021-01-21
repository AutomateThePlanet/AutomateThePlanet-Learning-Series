// <copyright file="WebDriverRedirectStrategy.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TestUrlRedirectsWebDriverHttpWebRequest.Redirects.Core
{
    public class WebDriverRedirectStrategy : IRedirectStrategy
    {
        private IWebDriver _driver;

        public void Initialize()
        {
            _driver = new FirefoxDriver();
        }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            _driver.Quit();
        }

        public string NavigateToFromUrl(string fromUrl)
        {
            _driver.Navigate().GoToUrl(fromUrl);
            var currentSitesUrl = _driver.Url;

            return currentSitesUrl;
        }
    }
}
