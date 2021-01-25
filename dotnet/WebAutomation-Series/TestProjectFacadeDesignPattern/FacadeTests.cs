// <copyright file="FacadeTests.cs" company="Automate The Planet Ltd.">
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
using TestProjectFacadeDesignPattern.Enums;
using TestProjectFacadeDesignPattern.Pages;

namespace TestProjectFacadeDesignPattern
{
    [TestClass]
    public class FacadeTests
    {
        private IWebDriver _driver;
        private MainPage _mainPage;
        private ResultDetailedPage _resultDetailedPage;
        private ImageSearchFacade _imageSearchFacade;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new ChromeDriver();
            _mainPage = new MainPage(_driver);
            _resultDetailedPage = new ResultDetailedPage(_driver);
            _imageSearchFacade = new ImageSearchFacade(_driver, _mainPage, _resultDetailedPage);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void AssertAtpSearchImageResults_NoFacade()
        {
            _mainPage
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

        [TestMethod]
        public void AssertTPSearchImageResults_NoFacade()
        {
            _mainPage
                    .Open<MainPage>()
                    .Search("testproject.io")
                    .ClickImages()
                    .SetSize(Sizes.ExtraLarge)
                    .SetColor(Colors.ColorOnly)
                    .SetTypes(Types.Clipart)
                    .SetPeople(People.All)
                    .SetDate(Dates.PastWeek)
                    .SetLicense(Licenses.All)
                    .ClickImageResult(1);
            _resultDetailedPage.AssertResultTitle("TestProject · GitHub")
                .AssertResultLink("https://github.com/testproject-io")
                .ClickVisitSiteButton();

            Assert.AreEqual("https://github.com/testproject-io", _driver.Url);
        }

        [TestMethod]
        public void AssertAtpSearchImageResults_WithFacade()
        {
            var searchData = new SearchData()
            {
                SearchTerm = "automate the planet",
                Size = Sizes.Large,
                Color = Colors.BlackWhite,
                Type = Types.Clipart,
                People = People.All,
                Date = Dates.PastYear,
                License = Licenses.All,
                ResultNumber = 1,
            };

            _imageSearchFacade.SearchImage(
                searchData,
                "Homepage - Automate The Planet",
                "https://www.automatetheplanet.com/");
        }

        [TestMethod]
        public void AssertTPSearchImageResults_WithFacade()
        {
            var searchData = new SearchData()
            {
                SearchTerm = "testproject.io",
                Size = Sizes.ExtraLarge,
                Color = Colors.ColorOnly,
                Type = Types.Clipart,
                People = People.All,
                Date = Dates.PastWeek,
                License = Licenses.All,
                ResultNumber = 1,
            };

            _imageSearchFacade.SearchImage(
                searchData,
                "TestProject · GitHub",
                "https://github.com/testproject-io");
        }
    }
}