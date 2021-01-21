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

using HuddlePageObjectsPartialClassesFluentApi.Enums;
using OpenQA.Selenium;

namespace HuddlePageObjectsPartialClassesFluentApi.Pages
{
    public partial class BingMainPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"http://www.bing.com/";

        public BingMainPage(IWebDriver browser)
        {
            _driver = browser;
        }

        public BingMainPage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            return this;
        }

        public BingMainPage Search(string textToType)
        {
            SearchBox.Clear();
            SearchBox.SendKeys(textToType);
            GoButton.Click();
            return this;
        }

        public BingMainPage ClickImages()
        {
            ImagesLink.Click();
            return this;
        }

        public BingMainPage SetSize(Sizes size)
        {
            Sizes.SelectByIndex((int)size);
            return this;
        }

        public BingMainPage SetColor(Colors color)
        {
            Color.SelectByIndex((int)color);
            return this;
        }

        public BingMainPage SetTypes(Types type)
        {
            Type.SelectByIndex((int)type);
            return this;
        }

        public BingMainPage SetLayout(Layouts layout)
        {
            Layout.SelectByIndex((int)layout);
            return this;
        }

        public BingMainPage SetPeople(People people)
        {
            People.SelectByIndex((int)people);
            return this;
        }

        public BingMainPage SetDate(Dates date)
        {
            Date.SelectByIndex((int)date);
            return this;
        }

        public BingMainPage SetLicense(Licenses license)
        {
            License.SelectByIndex((int)license);
            return this;
        }
    }
}