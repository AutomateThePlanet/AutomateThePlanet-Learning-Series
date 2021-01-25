// <copyright file="BingMainPage.cs" company="Automate The Planet Ltd.">
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

using TestProjectFacadeDesignPattern.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TestProjectFacadeDesignPattern.Pages;

namespace TestProjectFacadeDesignPattern.Pages
{
    public class MainPage : NavigatableWebPage<MainPageElements>
    {
        public MainPage(IWebDriver driver)
            : base(driver)
        {
        }

        protected override string Url => "http://www.bing.com/";

        protected override void WaitForPageLoad()
        {
            ////WaitForAjax();
            WaitForElementToExists(By.Id("sb_form_q"));
        }

        public MainPage Search(string textToType)
        {
            GetElements().GetSearchBox().Clear();
            GetElements().GetSearchBox().SendKeys(textToType);
            GetElements().GetSearchBox().SendKeys(Keys.Enter);
            return this;
        }

        public MainPage ClickImages()
        {
            GetElements().GetImagesLink().Click();
            return this;
        }

        public MainPage ClickImageResult(int num)
        {
            GetElements().GetImageResult(num).Click();
            return this;
        }

        public MainPage SetSize(Sizes size)
        {
            GetElements().GetSizesSelect().SelectByIndex((int)size);
            return this;
        }

        public MainPage SetColor(Colors color)
        {
            GetElements().GetColorSelect().SelectByIndex((int)color);
            return this;
        }

        public MainPage SetTypes(Types type)
        {
            GetElements().GetTypeSelect().SelectByIndex((int)type);
            return this;
        }

        public MainPage SetLayout(Layouts layout)
        {
            GetElements().GetLayoutSelect().SelectByIndex((int)layout);
            return this;
        }

        public MainPage SetPeople(People people)
        {
            GetElements().GetPeopleSelect().SelectByIndex((int)people);
            return this;
        }

        public MainPage SetDate(Dates date)
        {
            GetElements().GetDateSelect().SelectByIndex((int)date);
            return this;
        }

        public MainPage SetLicense(Licenses license)
        {
            GetElements().GetLicenseSelect().SelectByIndex((int)license);
            return this;
        }

        public MainPage ResultsCount(string expectedCount)
        {
            Assert.IsTrue(GetElements().GetResultsCountDiv().Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
            return this;
        }
    }
}