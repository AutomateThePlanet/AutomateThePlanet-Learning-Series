package kendogrid.components;

public class GridFilter {
    private String columnName;
    private FilterOperator filterOperator;
    private String filterValue;

    public GridFilter(String columnName, FilterOperator filterOperator, String filterValue) {
        this.columnName = columnName;
        this.filterOperator = filterOperator;
        this.filterValue = filterValue;
    }

    public String getColumnName() {
        return columnName;
    }

    public void setColumnName(String columnName) {
        this.columnName = columnName;
    }

    public FilterOperator getFilterOperator() {
        return filterOperator;
    }

    public void setFilterOperator(FilterOperator filterOperator) {
        this.filterOperator = filterOperator;
    }

    public String getFilterValue() {
        return filterValue;
    }

    public void setFilterValue(String filterValue) {
        this.filterValue = filterValue;
    }
}
