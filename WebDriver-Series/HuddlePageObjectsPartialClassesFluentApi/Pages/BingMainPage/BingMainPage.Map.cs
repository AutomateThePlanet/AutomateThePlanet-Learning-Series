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

namespace HuddlePageObjectsPartialClassesFluentApi.Pages
{
    public partial class BingMainPage
    {
        public IWebElement SearchBox => _driver.FindElement(By.Id("sb_form_q"));

        public IWebElement GoButton => _driver.FindElement(By.Id("sb_form_go"));

        public IWebElement ResultsCountDiv => _driver.FindElement(By.Id("b_tween"));

        public IWebElement ImagesLink => _driver.FindElement(By.LinkText("Images"));

        public SelectElement Sizes => new SelectElement(_driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Size']")));

        public SelectElement Color => new SelectElement(_driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Color']")));

        public SelectElement Type => new SelectElement(_driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Type']")));

        public SelectElement Layout => new SelectElement(_driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Layout']")));

        public SelectElement People => new SelectElement(_driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'People']")));

        public SelectElement Date => new SelectElement(_driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Date']")));

        public SelectElement License => new SelectElement(_driver.FindElement(By.XPath("//div/ul/li/span/span[text() = 'License']")));
    }
}