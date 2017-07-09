// <copyright file="TestingFrameworkDriver.CookieService.cs" company="Automate The Planet Ltd.">
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

using System.Net;
using ArtOfTest.WebAii.Core;
using HybridTestFramework.UITests.Core;

namespace HybridTestFramework.UITests.TestingFramework.Engine
{
    public partial class TestingFrameworkDriver : ICookieService
    {
        public string GetCookie(string host, string cookieName)
        {
            string currentCookieValue = null;
            var cookies =
                _driver.ActiveBrowser.Cookies.GetCookies(host);
            foreach (Cookie currentCookie in cookies)
            {
                if (currentCookie.Name.Equals(cookieName))
                {
                    currentCookieValue = currentCookie.Value;
                }
            }

            return currentCookieValue;
        }

        public void AddCookie(string cookieName, string cookieValue, string host)
        {
            var cookie = new Cookie()
            {
                Name = cookieName,
                Value = cookieValue,
                Domain = host,
                Path = "/"
            };
            _driver.ActiveBrowser.Cookies.SetCookie(cookie);
        }

        public void DeleteCookie(string cookieName)
        {
            _driver.ActiveBrowser.Cookies.DeleteCookie(cookieName);
        }

        public void CleanAllCookies()
        {
            _driver.ActiveBrowser.ClearCache(
                BrowserCacheType.Cookies);
            _driver.ActiveBrowser.ClearCache(
                BrowserCacheType.TempFilesCache);
        }
    }
}