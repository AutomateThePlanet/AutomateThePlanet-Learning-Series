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

using IoCContainerPageObjectPattern.Base;
using IoCContainerPageObjectPattern.Enums;

namespace IoCContainerPageObjectPattern.BingMainPage
{
    public class BingMainPage : BasePage<BingMainPageElementMap, BingMainPageValidator>, IBingMainPage
    {
        public BingMainPage() : base(@"http://www.bing.com/")
        {
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }

        public void ClickImages()
        {
            this.Map.ImagesLink.Click();
        }

        public void SetSize(Sizes size)
        {
            this.Map.Sizes.SelectByIndex((int)size);
        }

        public void SetColor(Colors color)
        {
            this.Map.Color.SelectByIndex((int)color);
        }

        public void SetTypes(Types type)
        {
            this.Map.Type.SelectByIndex((int)type);
        }

        public void SetLayout(Layouts layout)
        {
            this.Map.Layout.SelectByIndex((int)layout);
        }

        public void SetPeople(People people)
        {
            this.Map.People.SelectByIndex((int)people);
        }

        public void SetDate(Dates date)
        {
            this.Map.Date.SelectByIndex((int)date);
        }

        public void SetLicense(Licenses license)
        {
            this.Map.License.SelectByIndex((int)license);
        }
    }
}