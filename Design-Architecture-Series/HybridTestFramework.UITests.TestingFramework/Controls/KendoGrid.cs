// <copyright file="KendoGrid.cs" company="Automate The Planet Ltd.">
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

using ArtOfTest.WebAii.Controls.HtmlControls;
using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Data;
using HybridTestFramework.UITests.Core.Enums;
using Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HybridTestFramework.UITests.TestingFramework.Controls
{
    public class KendoGrid : Element<HtmlControl>, IKendoGrid
    {
        private readonly string _gridId;

        public KendoGrid(IDriver driver,
            ArtOfTest.WebAii.ObjectModel.Element element,
            IUnityContainer container) : base(driver, element, container)
        {
            _gridId = HtmlControl.ID;
        }

        public void RemoveFilters()
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.filter([]);");
            Driver.InvokeScript(jsToBeExecuted);
        }

        public int TotalNumberRows()
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.total();");
            var jsResult = Driver.InvokeScript(jsToBeExecuted);
            return int.Parse(jsResult.ToString());
        }

        public void Reload()
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.read();");
            Driver.InvokeScript(jsToBeExecuted);
        }

        public int GetPageSize()
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.pageSize();");
            var currentResponse = Driver.InvokeScript(jsToBeExecuted);
            var pageSize = int.Parse(currentResponse.ToString());
            return pageSize;
        }

        public void ChangePageSize(int newSize)
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.pageSize(", newSize, ");");
            Driver.InvokeScript(jsToBeExecuted);
        }

        public void NavigateToPage(int pageNumber)
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.page(", pageNumber, ");");
            Driver.InvokeScript(jsToBeExecuted);
        }

        public void Sort(string columnName, SortType sortType)
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted =
                string.Concat(jsToBeExecuted,
                    "grid.dataSource.sort({field: '",
                    columnName, "', dir: '",
                    sortType.ToString().ToLower(), "'});");
            Driver.InvokeScript(jsToBeExecuted);
        }

        public List<T> GetItems<T>() where T : class
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted =
                string.Concat(
                    jsToBeExecuted,
                    "grid.dataSource.data();");
            var jsResults = Driver.InvokeScript(jsToBeExecuted);
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void Filter(
            string columnName,
            FilterOperator filterOperator,
            string filterValue)
        {
            Filter(new GridFilter(columnName, filterOperator, filterValue));
        }

        public void Filter(params GridFilter[] gridFilters)
        {
            var jsToBeExecuted = GetGridReference();
            var sb = new StringBuilder();
            sb.Append(jsToBeExecuted);
            sb.Append("grid.dataSource.filter({ logic: \"and\", filters: [");
            foreach (var currentFilter in gridFilters)
            {
                DateTime filterDateTime;
                var isFilterDateTime = DateTime.TryParse(currentFilter.FilterValue, out filterDateTime);
                var filterValueToBeApplied =
                                               isFilterDateTime ? string.Format("new Date({0}, {1}, {2})",
                                                   filterDateTime.Year,
                                                   filterDateTime.Month - 1,
                                                   filterDateTime.Day) :
                                                string.Format("\"{0}\"", currentFilter.FilterValue);
                var kendoFilterOperator =
                    ConvertFilterOperatorToKendoOperator(currentFilter.FilterOperator);
                sb.Append(string.Concat("{ field: \"",
                    currentFilter.ColumnName,
                    "\", operator: \"",
                    kendoFilterOperator,
                    "\", value: ",
                    filterValueToBeApplied,
                    " },"));
            }
            sb.Append("] });");
            jsToBeExecuted = sb.ToString().Replace(",]", "]");
            Driver.InvokeScript(jsToBeExecuted);
        }

        public int GetCurrentPageNumber()
        {
            var jsToBeExecuted = GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.page();");
            var result = Driver.InvokeScript(jsToBeExecuted);
            var pageNumber = int.Parse(result.ToString());
            return pageNumber;
        }

        private string GetGridReference()
        {
            var initializeKendoGrid =
                string.Format("var grid = $('#{0}').data('kendoGrid');", _gridId);

            return initializeKendoGrid;
        }

        private string ConvertFilterOperatorToKendoOperator(FilterOperator filterOperator)
        {
            var kendoFilterOperator = string.Empty;
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