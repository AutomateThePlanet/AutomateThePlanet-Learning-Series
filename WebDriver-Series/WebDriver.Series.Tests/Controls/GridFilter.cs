using WebDriver.Series.Tests.Enums;

namespace WebDriver.Series.Tests.Controls
{
    public class GridFilter
    {
        public GridFilter(string columnName, FilterOperator filterOperator, string filterValue)
        {
            this.ColumnName = columnName;
            this.FilterOperator = filterOperator;
            this.FilterValue = filterValue;
        }

        public string ColumnName { get; set; }

        public FilterOperator FilterOperator { get; set; }

        public string FilterValue { get; set; }
    }
}
