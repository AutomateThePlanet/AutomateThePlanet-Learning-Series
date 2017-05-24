// <copyright file="FluentBingTests.cs" company="Automate The Planet Ltd.">
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

using System;
using HuddlePageObjectsPartialClassesFluentApi.Enums;
using HuddlePageObjectsPartialClassesFluentApi.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace HuddlePageObjectsPartialClassesFluentApi
{
    [TestClass]
    public class FluentBingTests
    {
        private IWebDriver _driver;
        private BingMainPage _bingPage;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            _bingPage = new BingMainPage(_driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void SearchForImageFuent()
        {
            _bingPage
                    .Navigate()
                    .Search("facebook")
                    .ClickImages()
                    .SetSize(Sizes.Large)
                    .SetColor(Colors.BlackWhite)
                    .SetTypes(Types.Clipart)
                    .SetPeople(People.All)
                    .SetDate(Dates.PastYear)
                    .SetLicense(Licenses.All);
        }
    }
}