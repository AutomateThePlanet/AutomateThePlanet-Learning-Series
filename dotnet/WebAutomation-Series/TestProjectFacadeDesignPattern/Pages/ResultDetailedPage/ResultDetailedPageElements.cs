// <copyright file="ResultDetailedPageElements.cs" company="Automate The Planet Ltd.">
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
using TestProjectFacadeDesignPattern.Pages;

namespace HuddlePageObjectsPartialClassesFluentApi.Pages
{
    public partial class ResultDetailedPageElements : WebElements
    {
        public ResultDetailedPageElements(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement GetTitle()
        {
            return Driver.FindElements(By.XPath("//a[@title='View page']"))[0];
        }

        public IWebElement GetResultLink()
        {
            return Driver.FindElements(By.XPath("//a[@title='View page']"))[1];
        }

        public IWebElement GetVisitSiteButton()
        {
            return Driver.FindElement(By.LinkText("Visit site"));
        }
    }
}