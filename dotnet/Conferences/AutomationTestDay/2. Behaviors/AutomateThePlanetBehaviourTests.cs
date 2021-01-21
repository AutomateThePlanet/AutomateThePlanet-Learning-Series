// <copyright file="AutomateThePlanetFacadeTests.cs" company="Automate The Planet Ltd.">
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

using AutomationTestDay.Behaviors.Base;
using AutomationTestDay.Facades.Pages;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationTestDay.Behaviors
{
    /*
     * 1. Refactor tests so that category page background is asserted only for the 'Design Pattern' category
     * 2. Create a new test for 'Design And Architecture' category, article- 'Create Hybrid Test Framework – Advanced Element Find Extensions'
     * 3. Remove Find How button assert from all tests
     * 4. Modify the code to use Enums instead of plain text for the categories' names
     * 5. Fill online Assessment System - https://www.surveymonkey.com/r/RS326GP
     */
    [TestFixture]
    public class AutomateThePlanetBehaviourTests
    {
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void ClassInit()
        {
            UnityContainerFactory.GetContainer().RegisterType<HomePage>();
            UnityContainerFactory.GetContainer().RegisterType<ArticlePage>();
            UnityContainerFactory.GetContainer().RegisterType<CategoryPage>();
            _driver = new ChromeDriver();
            UnityContainerFactory.GetContainer().RegisterInstance<IWebDriver>(_driver);
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _driver.Dispose();
        }

        [Test]
        public void DownloadSourceCode_DesignPatterns_Singleton()
        {
            string categoryName = "Design Pattern";
            string articleTitle = "Singleton Design Pattern in Automated Testing";
            BehaviorExecutor.Execute(
                new HomePageNavigateBehavior(),
                new HomePageNavigateFindHowAssertBehavior(categoryName),
                new HomePageFindsHowNavigateBehavior(categoryName),
                new CategoryPageCategoryBackgroundAssertBehavior(categoryName),
                new CategoryPageOpenArticleBehavior(articleTitle),
                new ArticlePageDownloadSourceCodeBehavior());
        }

        [Test]
        public void DownloadSourceCode_DesignPatterns_Facade()
        {
            string categoryName = "Design Pattern";
            string articleTitle = "Facade Design Pattern in Automated Testing";
            BehaviorExecutor.Execute(
                new HomePageNavigateBehavior(),
                new HomePageNavigateFindHowAssertBehavior(categoryName),
                new HomePageFindsHowNavigateBehavior(categoryName),
                new CategoryPageCategoryBackgroundAssertBehavior(categoryName),
                new CategoryPageOpenArticleBehavior(articleTitle),
                new ArticlePageDownloadSourceCodeBehavior());
        }

        [Test]
        public void DownloadSourceCode_Jenkins_Outpout()
        {
            string categoryName = "DevOps and CI";
            string articleTitle = "Output MSTest Tests Logs To Jenkins Console Log";
            BehaviorExecutor.Execute(
                new HomePageNavigateBehavior(),
                new HomePageNavigateFindHowAssertBehavior(categoryName),
                new HomePageFindsHowNavigateBehavior(categoryName),
                new CategoryPageCategoryBackgroundAssertBehavior(categoryName),
                new CategoryPageOpenArticleBehavior(articleTitle),
                new ArticlePageDownloadSourceCodeBehavior());
        }

        [Test]
        public void DownloadSourceCode_AutomationTools_MultipleFiles()
        {
            string categoryName = "Automation Tools";
            string articleTitle = "Create Multiple Files Page Objects with Visual Studio Item Templates";
            BehaviorExecutor.Execute(
                new HomePageNavigateBehavior(),
                new HomePageNavigateFindHowAssertBehavior(categoryName),
                new HomePageFindsHowNavigateBehavior(categoryName),
                new CategoryPageCategoryBackgroundAssertBehavior(categoryName),
                new CategoryPageOpenArticleBehavior(articleTitle),
                new ArticlePageDownloadSourceCodeBehavior());
        }
    }
}
