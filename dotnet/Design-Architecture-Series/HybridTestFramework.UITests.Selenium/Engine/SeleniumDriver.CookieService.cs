// <copyright file="seleniumdriver.cookieservice.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : ICookieService
    {
        public string GetCookie(string host, string cookieName)
        {
            var myCookie = _driver.Manage().Cookies.GetCookieNamed(cookieName);
            return myCookie.Value;
        }

        public void AddCookie(string cookieName, string cookieValue, string host)
        {
            var cookie = new Cookie(cookieName, cookieValue);
            _driver.Manage().Cookies.AddCookie(cookie);
        }

        public void DeleteCookie(string cookieName)
        {
            _driver.Manage().Cookies.DeleteCookieNamed("CookieName");
        }

        public void CleanAllCookies()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}
