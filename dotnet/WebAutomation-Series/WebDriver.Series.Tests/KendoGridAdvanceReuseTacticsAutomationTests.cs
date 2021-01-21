// <copyright file="KendoGridAdvanceReuseTacticsAutomationTests.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriver.Series.Tests.GridTestCases;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests
{
    [TestClass]
    public class KendoGridAdvanceReuseTacticsAutomationTests
    {
        private IWebDriver driver;
        private IGridPage gridPage;
        private FreightColumnAsserter freightColumnAsserter;
        private OrderDateColumnAsserter orderDateColumnAsserter;
        private OrderIdColumnAsserter orderIdColumnAsserter;
        private ShipNameColumnAsserter shipNameColumnAsserter;
        private GridPagerAsserter gridPagerAsserter;

        // TODO: Fix Pages URLs

        [TestInitialize]
        public void SetupTest()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(5));
            this.gridPage = new GridFilterPage(this.driver);
            this.freightColumnAsserter = new FreightColumnAsserter(this.gridPage);
            this.orderDateColumnAsserter = new OrderDateColumnAsserter(this.gridPage);
            this.orderIdColumnAsserter = new OrderIdColumnAsserter(this.gridPage);
            this.shipNameColumnAsserter = new ShipNameColumnAsserter(this.gridPage);
            this.gridPagerAsserter = new GridPagerAsserter(this.gridPage);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        // ** OrderID Test Cases (Unique Identifier Type Column Test Cases) ** 
        
        #region OrderID Test Cases 
        
        [TestMethod]
        public void OrderIdEqualToFilter()
        {
            this.orderIdColumnAsserter.OrderIdEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdGreaterThanOrEqualToFilter()
        {
            this.orderIdColumnAsserter.OrderIdGreaterThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdGreaterThanFilter()
        {
            this.orderIdColumnAsserter.OrderIdGreaterThanFilter();
        }
        
        [TestMethod]
        public void OrderIdLessThanOrEqualToFilter()
        {
            this.orderIdColumnAsserter.OrderIdLessThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdLessThanFilter()
        {
            this.orderIdColumnAsserter.OrderIdLessThanFilter();
        }
        
        [TestMethod]
        public void OrderIdNotEqualToFilter()
        {
            this.orderIdColumnAsserter.OrderIdNotEqualToFilter();
        }
        
        [TestMethod]
        public void OrderIdClearFilter()
        {
            this.orderIdColumnAsserter.OrderIdClearFilter();
        }

        #endregion

        // ** OrderDate Test Cases ** (Date Type Column Test Cases)
        
        #region OrderDate Test Cases

        [TestMethod]
        public void OrderDateEqualToFilter()
        {
            this.orderDateColumnAsserter.OrderDateEqualToFilter();
        }

        [TestMethod]
        public void OrderDateNotEqualToFilter()
        {
            this.orderDateColumnAsserter.OrderDateNotEqualToFilter();
        }

        [TestMethod]
        public void OrderDateAfterFilter()
        {
            this.orderDateColumnAsserter.OrderDateAfterFilter();
        }

        [TestMethod]
        public void OrderDateIsAfterOrEqualToFilter()
        {
            this.orderDateColumnAsserter.OrderDateIsAfterOrEqualToFilter();
        }

        [TestMethod]
        public void OrderDateBeforeFilter()
        {
            this.orderDateColumnAsserter.OrderDateBeforeFilter();
        }

        [TestMethod]
        public void OrderDateIsBeforeOrEqualToFilter()
        {
            this.orderDateColumnAsserter.OrderDateIsBeforeOrEqualToFilter();
        }

        [TestMethod]
        public void OrderDateClearFilter()
        {
            this.orderDateColumnAsserter.OrderDateClearFilter();
        }

        [TestMethod]
        public void OrderDateSortAsc()
        {
            this.orderDateColumnAsserter.OrderDateSortAsc();
        }

        [TestMethod]
        public void OrderDateSortDesc()
        {
            this.orderDateColumnAsserter.OrderDateSortDesc();
        }
        
        #endregion
        
        // ** ShipName Test Cases) ** (Text Type Column Test Cases)
        
        #region ShipName Test Cases
        
        [TestMethod]
        public void ShipNameEqualToFilter()
        {
            this.shipNameColumnAsserter.ShipNameEqualToFilter();
        }
        
        [TestMethod]
        public void ShipNameContainsFilter()
        {
            this.shipNameColumnAsserter.ShipNameContainsFilter();
        }
        
        [TestMethod]
        public void ShipNameEndsWithFilter()
        {
            this.shipNameColumnAsserter.ShipNameEndsWithFilter();
        }
        
        [TestMethod]
        public void ShipNameStartsWithFilter()
        {
            this.shipNameColumnAsserter.ShipNameStartsWithFilter();
        }
        
        [TestMethod]
        public void ShipNameNotEqualToFilter()
        {
            this.shipNameColumnAsserter.ShipNameNotEqualToFilter();
        }
        
        [TestMethod]
        public void ShipNameNotContainsFilter()
        {
            this.shipNameColumnAsserter.ShipNameNotContainsFilter();
        }
        
        [TestMethod]
        public void ShipNameClearFilter()
        {
            this.shipNameColumnAsserter.ShipNameClearFilter();
        }
        
        #endregion      
        
        // ** Freight Test Cases ** (Money Type Column Test Cases)

        #region Freight Test Cases
        
        [TestMethod]
        public void FreightEqualToFilter()
        {
            this.freightColumnAsserter.FreightEqualToFilter();
        }
        
        [TestMethod]
        public void FreightGreaterThanOrEqualToFilter()
        {
            this.freightColumnAsserter.FreightGreaterThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void FreightGreaterThanFilter()
        {
            this.freightColumnAsserter.FreightGreaterThanFilter();
        }
        
        [TestMethod]
        public void FreightLessThanOrEqualToFilter()
        {
            this.freightColumnAsserter.FreightLessThanOrEqualToFilter();
        }
        
        [TestMethod]
        public void FreightLessThanFilter()
        {
            this.freightColumnAsserter.FreightLessThanFilter();
        }
        
        [TestMethod]
        public void FreightNotEqualToFilter()
        {
            this.freightColumnAsserter.FreightNotEqualToFilter();
        }
        
        [TestMethod]
        public void FreightClearFilter()
        {
            this.freightColumnAsserter.FreightClearFilter();
        }

        #endregion
        
        // ** Paging Test Cases **
        
        #region Paging Test Cases
        
        [TestMethod]
        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            this.gridPagerAsserter.NavigateToFirstPage_GoToFirstPageButton();
        }

        [TestMethod]
        public void NavigateToLastPage_GoToLastPageButton()
        {
            this.gridPagerAsserter.NavigateToLastPage_GoToLastPageButton();
        }

        [TestMethod]
        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            this.gridPagerAsserter.NavigateToPageNine_GoToPreviousPageButton();
        }

        [TestMethod]
        public void NavigateToPageTwo_GoToNextPageButton()
        {
            this.gridPagerAsserter.NavigateToPageTwo_GoToNextPageButton();
        }

        [TestMethod]
        public void NavigateToPageTwo_SecondPageButton()
        {
            this.gridPagerAsserter.NavigateToPageTwo_SecondPageButton();
        }

        [TestMethod]
        public void NavigateToLastPage_MorePagesNextButton()
        {
            this.gridPagerAsserter.NavigateToLastPage_MorePagesNextButton();
        }

        [TestMethod]
        public void NavigateToPageOne_MorePagesPreviousButton()
        {
            this.gridPagerAsserter.NavigateToPageOne_MorePagesPreviousButton();
        }

        [TestMethod]
        public void GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            this.gridPagerAsserter.GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            this.gridPagerAsserter.GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded()
        {
            this.gridPagerAsserter.PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded()
        {
            this.gridPagerAsserter.GoToLastPageButtonDisabled_WhenLastPageIsLoaded();
        }

        [TestMethod]
        public void GoToNextPageButtonDisabled_WhenLastPageIsLoaded()
        {
            this.gridPagerAsserter.GoToNextPageButtonDisabled_WhenLastPageIsLoaded();
        }

        [TestMethod]
        public void NextMorePageButtonDisabled_WhenLastPageIsLoaded()
        {
            this.gridPagerAsserter.NextMorePageButtonDisabled_WhenLastPageIsLoaded();
        }
        
        #endregion
    }
}