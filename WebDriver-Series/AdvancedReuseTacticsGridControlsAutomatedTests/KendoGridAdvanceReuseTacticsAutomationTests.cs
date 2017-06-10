// <copyright file="KendoGridAdvanceReuseTacticsAutomationTests.cs" company="Automate The Planet Ltd.">
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
using System;
using AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriver.Series.Tests.GridTestCases;

namespace AdvancedReuseTacticsGridControlsAutomatedTests
{
    [TestClass]
    public class KendoGridAdvanceReuseTacticsAutomationTests
    {
        private IWebDriver driver;
        private IGridPage gridPage;
        private FreightColumnAsserter freightColumnAsserter;
        private GridTestCases.OrderDateColumnAsserter orderDateColumnAsserter;
        private GridTestCases.OrderIdColumnAsserter orderIdColumnAsserter;
        private GridTestCases.ShipNameColumnAsserter shipNameColumnAsserter;
        private GridTestCases.GridPagerAsserter gridPagerAsserter;

        // TODO: Fix Pages URLs

        [TestInitialize]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(5));
            gridPage = new GridFilterPage(driver);
            freightColumnAsserter = new FreightColumnAsserter(gridPage);
            orderDateColumnAsserter = new GridTestCases.OrderDateColumnAsserter(gridPage);
            orderIdColumnAsserter = new GridTestCases.OrderIdColumnAsserter(gridPage);
            shipNameColumnAsserter = new GridTestCases.ShipNameColumnAsserter(gridPage);
            gridPagerAsserter = new GridTestCases.GridPagerAsserter(gridPage);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            driver.Quit();
        }

        // ** OrderID Test Cases (Unique Identifier Type Column Test Cases) ** 
        
        #region OrderID Test Cases 
        
        [TestMethod]
        public void OrderIdEqualToFilter()
        {
            orderIdColumnAsserter.OrderIdEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdGreaterThanOrEqualToFilter()
        {
            orderIdColumnAsserter.OrderIdGreaterThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdGreaterThanFilter()
        {
            orderIdColumnAsserter.OrderIdGreaterThanFilter();
        }
        
        [TestMethod]
        public void OrderIdLessThanOrEqualToFilter()
        {
            orderIdColumnAsserter.OrderIdLessThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdLessThanFilter()
        {
            orderIdColumnAsserter.OrderIdLessThanFilter();
        }
        
        [TestMethod]
        public void OrderIdNotEqualToFilter()
        {
            orderIdColumnAsserter.OrderIdNotEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdClearFilter()
        {
            orderIdColumnAsserter.OrderIdClearFilter();
        }

        #endregion

        // ** OrderDate Test Cases ** (Date Type Column Test Cases)
        
        #region OrderDate Test Cases

        [TestMethod]
        public void OrderDateEqualToFilter()
        {
            orderDateColumnAsserter.OrderDateEqualToFilter();
        }

        [TestMethod]
        public void OrderDateNotEqualToFilter()
        {
            orderDateColumnAsserter.OrderDateNotEqualToFilter();
        }

        [TestMethod]
        public void OrderDateAfterFilter()
        {
            orderDateColumnAsserter.OrderDateAfterFilter();
        }

        [TestMethod]
        public void OrderDateIsAfterOrEqualToFilter()
        {
            orderDateColumnAsserter.OrderDateIsAfterOrEqualToFilter();
        }

        [TestMethod]
        public void OrderDateBeforeFilter()
        {
            orderDateColumnAsserter.OrderDateBeforeFilter();
        }

        [TestMethod]
        public void OrderDateIsBeforeOrEqualToFilter()
        {
            orderDateColumnAsserter.OrderDateIsBeforeOrEqualToFilter();
        }

        [TestMethod]
        public void OrderDateClearFilter()
        {
            orderDateColumnAsserter.OrderDateClearFilter();
        }

        [TestMethod]
        public void OrderDateSortAsc()
        {
            orderDateColumnAsserter.OrderDateSortAsc();
        }

        [TestMethod]
        public void OrderDateSortDesc()
        {
            orderDateColumnAsserter.OrderDateSortDesc();
        }
        
        #endregion
        
        // ** ShipName Test Cases) ** (Text Type Column Test Cases)
        
        #region ShipName Test Cases
        
        [TestMethod]
        public void ShipNameEqualToFilter()
        {
            shipNameColumnAsserter.ShipNameEqualToFilter();
        }
        
        [TestMethod]
        public void ShipNameContainsFilter()
        {
            shipNameColumnAsserter.ShipNameContainsFilter();
        }
        
        [TestMethod]
        public void ShipNameEndsWithFilter()
        {
            shipNameColumnAsserter.ShipNameEndsWithFilter();
        }
        
        [TestMethod]
        public void ShipNameStartsWithFilter()
        {
            shipNameColumnAsserter.ShipNameStartsWithFilter();
        }
        
        [TestMethod]
        public void ShipNameNotEqualToFilter()
        {
            shipNameColumnAsserter.ShipNameNotEqualToFilter();
        }
        
        [TestMethod]
        public void ShipNameNotContainsFilter()
        {
            shipNameColumnAsserter.ShipNameNotContainsFilter();
        }
        
        [TestMethod]
        public void ShipNameClearFilter()
        {
            shipNameColumnAsserter.ShipNameClearFilter();
        }
        
        #endregion      
        
        // ** Freight Test Cases ** (Money Type Column Test Cases)

        #region Freight Test Cases
        
        [TestMethod]
        public void FreightEqualToFilter()
        {
            freightColumnAsserter.FreightEqualToFilter();
        }
        
        [TestMethod]
        public void FreightGreaterThanOrEqualToFilter()
        {
            freightColumnAsserter.FreightGreaterThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void FreightGreaterThanFilter()
        {
            freightColumnAsserter.FreightGreaterThanFilter();
        }
        
        [TestMethod]
        public void FreightLessThanOrEqualToFilter()
        {
            freightColumnAsserter.FreightLessThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void FreightLessThanFilter()
        {
            freightColumnAsserter.FreightLessThanFilter();
        }
        
        [TestMethod]
        public void FreightNotEqualToFilter()
        {
            freightColumnAsserter.FreightNotEqualToFilter();
        }
        
        [TestMethod]
        public void FreightClearFilter()
        {
            freightColumnAsserter.FreightClearFilter();
        }

        #endregion
        
        // ** Paging Test Cases **
        
        #region Paging Test Cases
        
        [TestMethod]
        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            gridPagerAsserter.NavigateToFirstPage_GoToFirstPageButton();
        }

        [TestMethod]
        public void NavigateToLastPage_GoToLastPageButton()
        {
            gridPagerAsserter.NavigateToLastPage_GoToLastPageButton();
        }

        [TestMethod]
        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            gridPagerAsserter.NavigateToPageNine_GoToPreviousPageButton();
        }

        [TestMethod]
        public void NavigateToPageTwo_GoToNextPageButton()
        {
            gridPagerAsserter.NavigateToPageTwo_GoToNextPageButton();
        }

        [TestMethod]
        public void NavigateToPageTwo_SecondPageButton()
        {
            gridPagerAsserter.NavigateToPageTwo_SecondPageButton();
        }

        [TestMethod]
        public void NavigateToLastPage_MorePagesNextButton()
        {
            gridPagerAsserter.NavigateToLastPage_MorePagesNextButton();
        }

        [TestMethod]
        public void NavigateToPageOne_MorePagesPreviousButton()
        {
            gridPagerAsserter.NavigateToPageOne_MorePagesPreviousButton();
        }

        [TestMethod]
        public void GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            gridPagerAsserter.GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            gridPagerAsserter.GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded()
        {
            gridPagerAsserter.PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded()
        {
            gridPagerAsserter.GoToLastPageButtonDisabled_WhenLastPageIsLoaded();
        }

        [TestMethod]
        public void GoToNextPageButtonDisabled_WhenLastPageIsLoaded()
        {
            gridPagerAsserter.GoToNextPageButtonDisabled_WhenLastPageIsLoaded();
        }

        [TestMethod]
        public void NextMorePageButtonDisabled_WhenLastPageIsLoaded()
        {
            gridPagerAsserter.NextMorePageButtonDisabled_WhenLastPageIsLoaded();
        }
        
        #endregion
    }
}