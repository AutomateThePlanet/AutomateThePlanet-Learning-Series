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
// <site>https://automatetheplanet.com/</site>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TestProjectTemplateMethodDesignPattern.Enums;
using TestProjectTemplateMethodDesignPattern.Pages;

namespace TestProjectTemplateMethodDesignPattern
{
    [TestClass]
    public class TemplateMethodBingTests
    {
        private IWebDriver _driver;
        private MainPage _bingPage;
        private ResultDetailedPage _resultDetailedPage;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new ChromeDriver();
            _bingPage = new MainPage(_driver);
            _resultDetailedPage = new ResultDetailedPage(_driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void AssertSearchImageResults()
        {
            _bingPage
                    .Open<MainPage>()
                    .Search("automate the planet")
                    .ClickImages()
                    .SetSize(Sizes.Large)
                    .SetColor(Colors.BlackWhite)
                    .SetTypes(Types.Clipart)
                    .SetPeople(People.All)
                    .SetDate(Dates.PastYear)
                    .SetLicense(Licenses.All)
                    .ClickImageResult(1);
            _resultDetailedPage.AssertResultTitle("Homepage - Automate The Planet")
                .AssertResultLink("https://www.automatetheplanet.com/")
                .ClickVisitSiteButton();

            Assert.AreEqual("https://www.automatetheplanet.com/", _driver.Url);
        }
    }
}