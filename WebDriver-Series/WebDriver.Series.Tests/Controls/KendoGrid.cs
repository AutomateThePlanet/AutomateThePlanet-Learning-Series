using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriver.Series.Tests.Enums;

namespace WebDriver.Series.Tests.Controls
{
    public class KendoGrid
    {
        private readonly string gridId;
        private readonly IJavaScriptExecutor driver;

        public KendoGrid(IWebDriver driver, IWebElement gridDiv)
        {
            this.gridId = gridDiv.GetAttribute("id");
            this.driver = (IJavaScriptExecutor)driver;
        }

        public void RemoveFilters()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.filter([]);");
            this.driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public int TotalNumberRows()
        {
            ////this.Driver.FullWaitUntilReady();
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.total();");
            var jsResult = this.driver.ExecuteScript(jsToBeExecuted);
            return int.Parse(jsResult.ToString());
        }

        public void Reload()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.read();");
            this.driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public int GetPageSize()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.pageSize();");
            var currentResponse = this.driver.ExecuteScript(jsToBeExecuted);
            int pageSize = int.Parse(currentResponse.ToString());
            return pageSize;
        }

        public void ChangePageSize(int newSize)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.pageSize(", newSize, ");");
            this.driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public void NavigateToPage(int pageNumber)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.page(", pageNumber, ");");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public void Sort(string columnName, SortType sortType)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.sort({field: '", columnName, "', dir: '", sortType.ToString().ToLower(), "'});");
            this.driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public List<T> GetItems<T>() where T : class
        {
            ////this.Driver.FullWaitUntilReady();
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return JSON.stringify(grid.dataSource.data());");
            var jsResults = this.driver.ExecuteScript(jsToBeExecuted);
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void Filter(string columnName, FilterOperator filterOperator, string filterValue)
        {
            this.Filter(new GridFilter(columnName, filterOperator, filterValue));
        }

        public void Filter(params GridFilter[] gridFilters)
        {
            string jsToBeExecuted = this.GetGridReference();
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
                string kendoFilterOperator = this.ConvertFilterOperatorToKendoOperator(currentFilter.FilterOperator);
                sb.Append(string.Concat("{ field: \"", currentFilter.ColumnName, "\", operator: \"", kendoFilterOperator, "\", value: ", filterValueToBeApplied, " },"));
            }
            sb.Append("] });");
            jsToBeExecuted = sb.ToString().Replace(",]", "]");
            this.driver.ExecuteScript(jsToBeExecuted);
            ////this.Driver.FullWaitUntilReady();
        }

        public int GetCurrentPageNumber()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.page();");
            var result = this.driver.ExecuteScript(jsToBeExecuted);
            int pageNumber = int.Parse(result.ToString());
            return pageNumber;
        }

        private string GetGridReference()
        {
            string initializeKendoGrid = string.Format("var grid = $('#{0}').data('kendoGrid');", this.gridId);

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