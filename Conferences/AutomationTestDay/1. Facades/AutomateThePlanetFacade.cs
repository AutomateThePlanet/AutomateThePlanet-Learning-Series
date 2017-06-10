// <copyright file="AutomateThePlanetFacade.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>
using AutomationTestDay.Facades.Pages;
using OpenQA.Selenium;

namespace AutomationTestDay.Facades
{
    public class AutomateThePlanetFacade
    {
        private readonly ArticlePage _articlePage;
        private readonly CategoryPage _categoryPage;
        private readonly HomePage _homePage;

        public AutomateThePlanetFacade(IWebDriver webDriver)
        {
            _articlePage = new ArticlePage(webDriver);
            _categoryPage = new CategoryPage(webDriver);
            _homePage = new HomePage(webDriver);
        }

        public void DownloadSourceCode(string categoryText, string articleText)
        {
            _homePage.NavigateTo();
            _homePage.AssertFindHowText(categoryText);
            var findHowButton = _homePage.GetFindHowButtonByText(categoryText);
            findHowButton.Click();

            _categoryPage.AssertCategoryBackgroundWhenSelected(categoryText);
            var articleAnchor = default(IWebElement);
            do
            {
                try
                {
                    articleAnchor = _categoryPage.GetArticleAnchorByName(articleText);
                }
                catch (NoSuchElementException)
                {
                    _categoryPage.NavigateToNextPage();
                }
            }
            while (articleAnchor == null);
            articleAnchor.Click();

            _articlePage.DownloadFullSourceCode.Click();
        }
    }
}

// 1. Extend the test to check the links' category before clicking. Green icon.
// 2. Create two different version of the facade's method to accept enums Topic and Framework instead using string.