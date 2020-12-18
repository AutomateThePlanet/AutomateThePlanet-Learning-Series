// <copyright file="BingMainPage.cs" company="Automate The Planet Ltd.">
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

using HuddlePageObjectsPartialClassesFluentApi.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace HuddlePageObjectsPartialClassesFluentApi.Pages
{
    public partial class BingMainPage
    {
        private readonly IWebDriver _driver;
        private readonly BingMainPageElements _elements;
        private readonly string _url = @"http://www.bing.com/";

        public BingMainPage(IWebDriver driver)
        {
            _driver = driver;
            _elements = new BingMainPageElements(_driver);
        }

        public BingMainPage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            return this;
        }

        public BingMainPage Search(string textToType)
        {
            _elements.GetSearchBox().Clear();
            _elements.GetSearchBox().SendKeys(textToType);
            _elements.GetGoButton().Click();
            return this;
        }

        public BingMainPage ClickImages()
        {
            _elements.GetImagesLink().Click();
            return this;
        }

        public BingMainPage SetSize(Sizes size)
        {
            _elements.GetSizesSelect().SelectByIndex((int)size);
            return this;
        }

        public BingMainPage SetColor(Colors color)
        {
            _elements.GetColorSelect().SelectByIndex((int)color);
            return this;
        }

        public BingMainPage SetTypes(Types type)
        {
            _elements.GetTypeSelect().SelectByIndex((int)type);
            return this;
        }

        public BingMainPage SetLayout(Layouts layout)
        {
            _elements.GetLayoutSelect().SelectByIndex((int)layout);
            return this;
        }

        public BingMainPage SetPeople(People people)
        {
            _elements.GetPeopleSelect().SelectByIndex((int)people);
            return this;
        }

        public BingMainPage SetDate(Dates date)
        {
            _elements.GetDateSelect().SelectByIndex((int)date);
            return this;
        }

        public BingMainPage SetLicense(Licenses license)
        {
            _elements.GetLicenseSelect().SelectByIndex((int)license);
            return this;
        }

        public BingMainPage ResultsCount(string expectedCount)
        {
            Assert.IsTrue(_elements.GetResultsCountDiv().Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
            return this;
        }
    }
}