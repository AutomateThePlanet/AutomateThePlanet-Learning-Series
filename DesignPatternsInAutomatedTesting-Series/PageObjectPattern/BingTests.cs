// <copyright file="BingTests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using PageObjectPattern.Selenium.Bing.Pages;

namespace PageObjectPattern
{
    [TestClass]
    public class BingTests
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            Driver = new FirefoxDriver();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.Quit();
        }

        [TestMethod]
        public void SearchTextInBing_First()
        {
            var bingMainPage = new BingMainPage(Driver);
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.ValidateResultsCount("264,000 RESULTS");
        }

        [TestMethod]
        public void SearchTextInBing_Second()
        {
            var bingMainPage = new Pages.BingMainPage(Driver);
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.Validate().ResultsCount("264,000 RESULTS");
        }

        [TestMethod]
        public void ClickEveryHrefMenu()
        {
            Driver.Navigate().GoToUrl(@"http://www.telerik.com/");
            // get the menu div
            var menuList = Driver.FindElement(By.Id("GeneralContent_T73A12E0A142_Col01"));
            // get all links from the menu div
            var menuHrefs = menuList.FindElements(By.ClassName("Bar-menu-link"));

            // Now start clicking and navigating back
            foreach (var currentHref in menuHrefs)
            {
                Driver.Navigate().GoToUrl(@"http://www.telerik.com/");
                currentHref.Click();
                var currentElementHref = currentHref.GetAttribute("href");
                Assert.IsTrue(Driver.Url.Contains(currentElementHref));
                // Now the same will happen for the next href
            }
        }
    }
}