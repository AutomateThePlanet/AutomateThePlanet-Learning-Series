// <copyright file="BingMainPageElementMap.cs" company="Automate The Planet Ltd.">
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

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProjectTemplateMethodDesignPattern.Pages
{
    public partial class MainPageElements : WebElements
    {
        public MainPageElements(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement GetSearchBox()
        {
            return Driver.FindElement(By.Id("sb_form_q"));
        }

        public IWebElement GetResultsCountDiv()
        {
            return Driver.FindElement(By.Id("b_tween"));
        }

        public IWebElement GetImagesLink()
        {
            return Driver.FindElement(By.XPath("//a[text()='Images']"));
        }

        public SelectElement GetSizesSelect()
        {
            return new SelectElement(Driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Size']")));
        }

        public SelectElement GetColorSelect()
        {
            return new SelectElement(Driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Color']")));
        }

        public SelectElement GetTypeSelect()
        {
            return new SelectElement(Driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Type']")));
        }

        public SelectElement GetLayoutSelect()
        {
            return new SelectElement(Driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Layout']")));
        }

        public SelectElement GetPeopleSelect()
        {
            return new SelectElement(Driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'People']")));
        }

        public SelectElement GetDateSelect()
        {
            return new SelectElement(Driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Date']")));
        }

        public SelectElement GetLicenseSelect()
        {
            return new SelectElement(Driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'License']")));
        }

        public IWebElement GetImageResult(int num)
        {
            return Driver.FindElement(By.LinkText($"//li[@data-idx='{num}']"));
        }
    }
}