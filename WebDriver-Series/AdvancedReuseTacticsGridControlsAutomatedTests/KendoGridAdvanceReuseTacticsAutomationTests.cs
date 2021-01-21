// <copyright file="KendoGridAdvanceReuseTacticsAutomationTests.cs" company="Automate The Planet Ltd.">
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
        private IWebDriver _driver;
        private IGridPage _gridPage;
        private FreightColumnAsserter _freightColumnAsserter;
        private GridTestCases.OrderDateColumnAsserter _orderDateColumnAsserter;
        private GridTestCases.OrderIdColumnAsserter _orderIdColumnAsserter;
        private GridTestCases.ShipNameColumnAsserter _shipNameColumnAsserter;
        private GridTestCases.GridPagerAsserter _gridPagerAsserter;

        // TODO: Fix Pages URLs
        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            _gridPage = new GridFilterPage(_driver);
            _freightColumnAsserter = new FreightColumnAsserter(_gridPage);
            _orderDateColumnAsserter = new GridTestCases.OrderDateColumnAsserter(_gridPage);
            _orderIdColumnAsserter = new GridTestCases.OrderIdColumnAsserter(_gridPage);
            _shipNameColumnAsserter = new GridTestCases.ShipNameColumnAsserter(_gridPage);
            _gridPagerAsserter = new GridTestCases.GridPagerAsserter(_gridPage);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        // ** OrderID Test Cases (Unique Identifier Type Column Test Cases) **
        #region OrderID Test Cases

        [TestMethod]
        public void OrderIdEqualToFilter()
        {
            _orderIdColumnAsserter.OrderIdEqualToFilter();
        }

        [TestMethod]
        public void OrderIdGreaterThanOrEqualToFilter()
        {
            _orderIdColumnAsserter.OrderIdGreaterThanOrEqualToFilter();
        }

        [TestMethod]
        public void OrderIdGreaterThanFilter()
        {
            _orderIdColumnAsserter.OrderIdGreaterThanFilter();
        }

        [TestMethod]
        public void OrderIdLessThanOrEqualToFilter()
        {
            _orderIdColumnAsserter.OrderIdLessThanOrEqualToFilter();
        }

        [TestMethod]
        public void OrderIdLessThanFilter()
        {
            _orderIdColumnAsserter.OrderIdLessThanFilter();
        }

        [TestMethod]
        public void OrderIdNotEqualToFilter()
        {
            _orderIdColumnAsserter.OrderIdNotEqualToFilter();
        }

        [TestMethod]
        public void OrderIdClearFilter()
        {
            _orderIdColumnAsserter.OrderIdClearFilter();
        }

        #endregion

        // ** OrderDate Test Cases ** (Date Type Column Test Cases)
        #region OrderDate Test Cases

        [TestMethod]
        public void OrderDateEqualToFilter()
        {
            _orderDateColumnAsserter.OrderDateEqualToFilter();
        }

        [TestMethod]
        public void OrderDateNotEqualToFilter()
        {
            _orderDateColumnAsserter.OrderDateNotEqualToFilter();
        }

        [TestMethod]
        public void OrderDateAfterFilter()
        {
            _orderDateColumnAsserter.OrderDateAfterFilter();
        }

        [TestMethod]
        public void OrderDateIsAfterOrEqualToFilter()
        {
            _orderDateColumnAsserter.OrderDateIsAfterOrEqualToFilter();
        }

        [TestMethod]
        public void OrderDateBeforeFilter()
        {
            _orderDateColumnAsserter.OrderDateBeforeFilter();
        }

        [TestMethod]
        public void OrderDateIsBeforeOrEqualToFilter()
        {
            _orderDateColumnAsserter.OrderDateIsBeforeOrEqualToFilter();
        }

        [TestMethod]
        public void OrderDateClearFilter()
        {
            _orderDateColumnAsserter.OrderDateClearFilter();
        }

        [TestMethod]
        public void OrderDateSortAsc()
        {
            _orderDateColumnAsserter.OrderDateSortAsc();
        }

        [TestMethod]
        public void OrderDateSortDesc()
        {
            _orderDateColumnAsserter.OrderDateSortDesc();
        }

        #endregion

        // ** ShipName Test Cases) ** (Text Type Column Test Cases)
        #region ShipName Test Cases

        [TestMethod]
        public void ShipNameEqualToFilter()
        {
            _shipNameColumnAsserter.ShipNameEqualToFilter();
        }

        [TestMethod]
        public void ShipNameContainsFilter()
        {
            _shipNameColumnAsserter.ShipNameContainsFilter();
        }

        [TestMethod]
        public void ShipNameEndsWithFilter()
        {
            _shipNameColumnAsserter.ShipNameEndsWithFilter();
        }

        [TestMethod]
        public void ShipNameStartsWithFilter()
        {
            _shipNameColumnAsserter.ShipNameStartsWithFilter();
        }

        [TestMethod]
        public void ShipNameNotEqualToFilter()
        {
            _shipNameColumnAsserter.ShipNameNotEqualToFilter();
        }

        [TestMethod]
        public void ShipNameNotContainsFilter()
        {
            _shipNameColumnAsserter.ShipNameNotContainsFilter();
        }

        [TestMethod]
        public void ShipNameClearFilter()
        {
            _shipNameColumnAsserter.ShipNameClearFilter();
        }

        #endregion

        // ** Freight Test Cases ** (Money Type Column Test Cases)
        #region Freight Test Cases

        [TestMethod]
        public void FreightEqualToFilter()
        {
            _freightColumnAsserter.FreightEqualToFilter();
        }

        [TestMethod]
        public void FreightGreaterThanOrEqualToFilter()
        {
            _freightColumnAsserter.FreightGreaterThanOrEqualToFilter();
        }

        [TestMethod]
        public void FreightGreaterThanFilter()
        {
            _freightColumnAsserter.FreightGreaterThanFilter();
        }

        [TestMethod]
        public void FreightLessThanOrEqualToFilter()
        {
            _freightColumnAsserter.FreightLessThanOrEqualToFilter();
        }

        [TestMethod]
        public void FreightLessThanFilter()
        {
            _freightColumnAsserter.FreightLessThanFilter();
        }

        [TestMethod]
        public void FreightNotEqualToFilter()
        {
            _freightColumnAsserter.FreightNotEqualToFilter();
        }

        [TestMethod]
        public void FreightClearFilter()
        {
            _freightColumnAsserter.FreightClearFilter();
        }

        #endregion

        // ** Paging Test Cases **
        #region Paging Test Cases

        [TestMethod]
        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            _gridPagerAsserter.NavigateToFirstPage_GoToFirstPageButton();
        }

        [TestMethod]
        public void NavigateToLastPage_GoToLastPageButton()
        {
            _gridPagerAsserter.NavigateToLastPage_GoToLastPageButton();
        }

        [TestMethod]
        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            _gridPagerAsserter.NavigateToPageNine_GoToPreviousPageButton();
        }

        [TestMethod]
        public void NavigateToPageTwo_GoToNextPageButton()
        {
            _gridPagerAsserter.NavigateToPageTwo_GoToNextPageButton();
        }

        [TestMethod]
        public void NavigateToPageTwo_SecondPageButton()
        {
            _gridPagerAsserter.NavigateToPageTwo_SecondPageButton();
        }

        [TestMethod]
        public void NavigateToLastPage_MorePagesNextButton()
        {
            _gridPagerAsserter.NavigateToLastPage_MorePagesNextButton();
        }

        [TestMethod]
        public void NavigateToPageOne_MorePagesPreviousButton()
        {
            _gridPagerAsserter.NavigateToPageOne_MorePagesPreviousButton();
        }

        [TestMethod]
        public void GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            _gridPagerAsserter.GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            _gridPagerAsserter.GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded()
        {
            _gridPagerAsserter.PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded();
        }

        [TestMethod]
        public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded()
        {
            _gridPagerAsserter.GoToLastPageButtonDisabled_WhenLastPageIsLoaded();
        }

        [TestMethod]
        public void GoToNextPageButtonDisabled_WhenLastPageIsLoaded()
        {
            _gridPagerAsserter.GoToNextPageButtonDisabled_WhenLastPageIsLoaded();
        }

        [TestMethod]
        public void NextMorePageButtonDisabled_WhenLastPageIsLoaded()
        {
            _gridPagerAsserter.NextMorePageButtonDisabled_WhenLastPageIsLoaded();
        }

        #endregion
    }
}