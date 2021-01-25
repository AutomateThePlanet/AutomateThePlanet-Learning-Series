// <copyright file="ResultDetailedPage.cs" company="Automate The Planet Ltd.">
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

using TestProjectTemplateMethodDesignPattern.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using HuddlePageObjectsPartialClassesFluentApi.Pages;

namespace TestProjectTemplateMethodDesignPattern.Pages
{
    public class ResultDetailedPage : WebPage<ResultDetailedPageElements>
    {
        public ResultDetailedPage(IWebDriver driver)
            : base(driver)
        {
        }

        public ResultDetailedPage ClickVisitSiteButton()
        {
            GetElements().GetVisitSiteButton().Click();
            return this;
        }

        public ResultDetailedPage AssertResultTitle(string expectedTitle)
        {
            Assert.AreEqual(GetElements().GetTitle().Text, expectedTitle);
            return this;
        }

        public ResultDetailedPage AssertResultLink(string expectedResultLink)
        {
            Assert.AreEqual(GetElements().GetResultLink().GetAttribute("href"), expectedResultLink);
            return this;
        }
    }
}