// <copyright file="BingMainPage.cs" company="Automate The Planet Ltd.">
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

using FluentPageObjectPattern.Core;
using FluentPageObjectPattern.Enums;

namespace FluentPageObjectPattern.Pages.BingMainPage
{
    public class BingMainPage : BaseFluentPageSingleton<BingMainPage, BingMainPageElementMap, BingMainPageValidator>
    {
        public new BingMainPage Navigate(string url = "http://www.bing.com/")
        {
            base.Navigate(url);
            return this;
        }

        public BingMainPage Search(string textToType)
        {
            Map.SearchBox.Clear();
            Map.SearchBox.SendKeys(textToType);
            Map.GoButton.Click();
            return this;
        }

        public BingMainPage ClickImages()
        {
            Map.ImagesLink.Click();
            return this;
        }

        public BingMainPage SetSize(Sizes size)
        {
            Map.Sizes.SelectByIndex((int)size);
            return this;
        }

        public BingMainPage SetColor(Colors color)
        {
            Map.Color.SelectByIndex((int)color);
            return this;
        }

        public BingMainPage SetTypes(Types type)
        {
            Map.Type.SelectByIndex((int)type);
            return this;
        }

        public BingMainPage SetLayout(Layouts layout)
        {
            Map.Layout.SelectByIndex((int)layout);
            return this;
        }

        public BingMainPage SetPeople(People people)
        {
            Map.People.SelectByIndex((int)people);
            return this;
        }

        public BingMainPage SetDate(Dates date)
        {
            Map.Date.SelectByIndex((int)date);
            return this;
        }

        public BingMainPage SetLicense(Licenses license)
        {
            Map.License.SelectByIndex((int)license);
            return this;
        }
    }
}