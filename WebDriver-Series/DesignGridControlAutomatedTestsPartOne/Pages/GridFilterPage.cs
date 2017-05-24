// <copyright file="GridFilterPage.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Ui.Automation.Core.Controls.Controls;

namespace DesignGridControlAutomatedTestsPartOne.Pages
{
    public class GridFilterPage : IGridPage
    {
        public readonly string Url = @"http://demos.telerik.com/kendo-ui/grid/filter-row";
        private readonly IWebDriver driver;

        public GridFilterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public KendoGrid Grid 
        {
            get
            {
                return new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            }
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/span")]
        public IWebElement PagerInfoLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[3]")]
        public IWebElement GoToNextPage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[1]")]
        public IWebElement GoToFirstPageButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[4]/span")]
        public IWebElement GoToLastPage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[2]/span")]
        public IWebElement GoToPreviousPage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[12]/a")]
        public IWebElement NextMorePages { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[2]/a")]
        public IWebElement PreviousMorePages { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[2]/a")]
        public IWebElement PageOnFirstPositionButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[3]/a")]
        public IWebElement PageOnSecondPositionButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[11]/a")]
        public IWebElement PageOnTenthPositionButton { get; set; }
        
        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}