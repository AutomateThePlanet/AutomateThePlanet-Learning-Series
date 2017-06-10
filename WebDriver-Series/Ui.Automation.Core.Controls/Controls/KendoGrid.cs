// <copyright file="KendoGrid.cs" company="Automate The Planet Ltd.">
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

using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Ui.Automation.Core.Controls.Enums;

namespace Ui.Automation.Core.Controls.Controls
{
    public class KendoGrid
    {
        private readonly string gridId;
        private readonly IJavaScriptExecutor driver;

        public KendoGrid(IWebDriver driver, IWebElement gridDiv)
        {
            gridId = gridDiv.GetAttribute("id");
            this.driver = (IJavaScriptExecutor)driver;
        }

        public void RemoveFilters()
        {
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.filter([]);");
            driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public int TotalNumberRows()
        {
            ////this.Driver.FullWaitUntilReady();
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.total();");
            var jsResult = driver.ExecuteScript(jsToBeExecuted);
            return int.Parse(jsResult.ToString());
        }

        public void Reload()
        {
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.read();");
            driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public int GetPageSize()
        {
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.pageSize();");
            var currentResponse = driver.ExecuteScript(jsToBeExecuted);
            int pageSize = int.Parse(currentResponse.ToString());
            return pageSize;
        }

        public void ChangePageSize(int newSize)
        {
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.pageSize(", newSize, ");");
            driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public void NavigateToPage(int pageNumber)
        {
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.page(", pageNumber, ");");
            driver.ExecuteScript(jsToBeExecuted);
        }

        public void Sort(string columnName, SortType sortType)
        {
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.sort({field: '", columnName, "', dir: '", sortType.ToString().ToLower(), "'});");
            driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public List<T> GetItems<T>() where T : class
        {
            ////this.Driver.FullWaitUntilReady();
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return JSON.stringify(grid.dataSource.data());");
            var jsResults = driver.ExecuteScript(jsToBeExecuted);
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void Filter(string columnName, FilterOperator filterOperator, string filterValue)
        {
            Filter(new GridFilter(columnName, filterOperator, filterValue));
        }

        public void Filter(params GridFilter[] gridFilters)
        {
            string jsToBeExecuted = GetGridReference();
            StringBuilder sb = new StringBuilder();
            sb.Append(jsToBeExecuted);
            sb.Append("grid.dataSource.filter({ logic: \"and\", filters: [");
            foreach (var currentFilter in gridFilters)
            {
                DateTime filterDateTime;
                bool isFilterDateTime = DateTime.TryParse(currentFilter.FilterValue, out filterDateTime);
                string filterValueToBeApplied =
                                               isFilterDateTime ? string.Format("new Date({0}, {1}, {2})", filterDateTime.Year, filterDateTime.Month - 1, filterDateTime.Day) :
                                                string.Format("\"{0}\"", currentFilter.FilterValue);
                string kendoFilterOperator = ConvertFilterOperatorToKendoOperator(currentFilter.FilterOperator);
                sb.Append(string.Concat("{ field: \"", currentFilter.ColumnName, "\", operator: \"", kendoFilterOperator, "\", value: ", filterValueToBeApplied, " },"));
            }
            sb.Append("] });");
            jsToBeExecuted = sb.ToString().Replace(",]", "]");
            driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public int GetCurrentPageNumber()
        {
            string jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.page();");
            var result = driver.ExecuteScript(jsToBeExecuted);
            int pageNumber = int.Parse(result.ToString());
            return pageNumber;
        }

        private string GetGridReference()
        {
            string initializeKendoGrid = string.Format("var grid = $('#{0}').data('kendoGrid');", gridId);

            return initializeKendoGrid;
        }

        private string ConvertFilterOperatorToKendoOperator(FilterOperator filterOperator)
        {
            string kendoFilterOperator = string.Empty;
            switch (filterOperator)
            {
                case FilterOperator.EqualTo:
                    kendoFilterOperator = "eq";
                    break;
                case FilterOperator.NotEqualTo:
                    kendoFilterOperator = "neq";
                    break;
                case FilterOperator.LessThan:
                    kendoFilterOperator = "lt";
                    break;
                case FilterOperator.LessThanOrEqualTo:
                    kendoFilterOperator = "lte";
                    break;
                case FilterOperator.GreaterThan:
                    kendoFilterOperator = "gt";
                    break;
                case FilterOperator.GreaterThanOrEqualTo:
                    kendoFilterOperator = "gte";
                    break;
                case FilterOperator.StartsWith:
                    kendoFilterOperator = "startswith";
                    break;
                case FilterOperator.EndsWith:
                    kendoFilterOperator = "endswith";
                    break;
                case FilterOperator.Contains:
                    kendoFilterOperator = "contains";
                    break;
                case FilterOperator.NotContains:
                    kendoFilterOperator = "doesnotcontain";
                    break;
                case FilterOperator.IsAfter:
                    kendoFilterOperator = "gt";
                    break;
                case FilterOperator.IsAfterOrEqualTo:
                    kendoFilterOperator = "gte";
                    break;
                case FilterOperator.IsBefore:
                    kendoFilterOperator = "lt";
                    break;
                case FilterOperator.IsBeforeOrEqualTo:
                    kendoFilterOperator = "lte";
                    break;
                default:
                    throw new ArgumentException("The specified filter operator is not supported.");
            }

            return kendoFilterOperator;
        }
    }
}