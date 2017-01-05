// <copyright file="ArticlesPage.Map.cs" company="Automate The Planet Ltd.">
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

using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace CodeProjectStatisticsCalculator.Pages.ItemPage
{
    public partial class ArticlesPage
    {
        public ReadOnlyCollection<IWebElement> ArticlesRows
        {
            get
            {
                return this.driver.FindElements(By.XPath("//tr[contains(@id,'CAR_MainArticleRow')]"));
            }
        }

        public IWebElement GetArticleStatisticsElement(IWebElement articleRow)
        {
            return articleRow.FindElement(By.CssSelector("div[id$='CAR_SbD']"));
        }

        public IWebElement GetArticleTitleElement(IWebElement articleRow)
        {
            return articleRow.FindElement(By.CssSelector("a[id$='CAR_Title']"));
        }
    }
}