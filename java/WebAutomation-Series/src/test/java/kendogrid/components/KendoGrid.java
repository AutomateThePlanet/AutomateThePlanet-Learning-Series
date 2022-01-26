package kendogrid.components;

import com.google.gson.Gson;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.time.format.DateTimeParseException;
import java.util.ArrayList;
import java.util.List;

public class KendoGrid {
    private final String _gridId;
    private final JavascriptExecutor driver;
    private final WebDriverWait _wait;

    public KendoGrid(WebDriver driver, WebElement gridDiv) {
        _gridId = gridDiv.getAttribute("id");
        this.driver = (JavascriptExecutor) driver;
        _wait = new WebDriverWait(driver, 30);
    }

    public void removeFilters() {
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "grid.dataSource.filter([]);";
        driver.executeScript(jsToBeExecuted);
        waitForAjax();
    }

    public int totalNumberRows() {
        waitForAjax();
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "grid.dataSource.total();";
        var jsResult = driver.executeScript(jsToBeExecuted);
        return Integer.parseInt(jsResult.toString());
    }

    public void reload() {
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "grid.dataSource.read();";
        driver.executeScript(jsToBeExecuted);
        waitForAjax();
    }

    public int getPageSize() {
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "return grid.dataSource.pageSize();";
        var currentResponse = driver.executeScript(jsToBeExecuted);
        var pageSize = Integer.parseInt(currentResponse.toString());
        return pageSize;
    }

    public void changePageSize(int newSize) {
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "grid.dataSource.pageSize(" + newSize + ");";
        driver.executeScript(jsToBeExecuted);
        waitForAjax();
    }

    public void navigateToPage(int pageNumber) {
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "grid.dataSource.page(" + pageNumber + ");";
        driver.executeScript(jsToBeExecuted);
    }

    public void sort(String columnName, SortType sortType) {
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "grid.dataSource.sort({field: '" + columnName + "', dir: '" + sortType.toString().toLowerCase() + "'});";
        driver.executeScript(jsToBeExecuted);
        waitForAjax();
    }

    public <T> List<T> getItems() {
        waitForAjax();
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "return JSON.stringify(grid.dataItems());";
        var jsResults = driver.executeScript(jsToBeExecuted);

        Gson gson = new Gson();
        List<T> items = gson.fromJson(jsResults.toString(), ArrayList.class);
        return items;
    }

    public void filter(String columnName, FilterOperator filterOperator, String filterValue) throws Exception {
        filter(new GridFilter(columnName, filterOperator, filterValue));
    }

    public void filter(GridFilter... gridFilters) throws Exception {
        var jsToBeExecuted = getGridReference();
        var sb = new StringBuilder();
        sb.append(jsToBeExecuted);
        sb.append("grid.dataSource.filter({ logic: \"and\", filters: [");
        for (var currentFilter: gridFilters) {
            var filterValueToBeApplied =  String.format("\"%s\"", currentFilter.getFilterValue());
            try {
                LocalDateTime filterDateTime = LocalDateTime.parse(currentFilter.getFilterValue());
                filterValueToBeApplied = String.format("new Date(%s, %s, %s})", filterDateTime.getYear(), filterDateTime.getMonthValue() - 1, filterDateTime.getDayOfYear());
            } catch (DateTimeParseException ex) {
                // ignore
            }

            var kendoFilterOperator = convertFilterOperatorToKendoOperator(currentFilter.getFilterOperator());
            sb.append("{ field: \"" + currentFilter.getColumnName() + "\", operator: \"" + kendoFilterOperator + "\", value: " + filterValueToBeApplied + " },");
        }
        sb.append("] });");
        jsToBeExecuted = sb.toString().replace(",]", "]");
        driver.executeScript(jsToBeExecuted);
        waitForAjax();
    }

    public int getCurrentPageNumber() {
        var jsToBeExecuted = getGridReference();
        jsToBeExecuted = jsToBeExecuted + "return grid.dataSource.page();";
        var result = driver.executeScript(jsToBeExecuted);
        var pageNumber = Integer.parseInt(result.toString());
        return pageNumber;
    }

    private String getGridReference() {
        var initializeKendoGrid = String.format("var grid = $('#%s').data('kendoGrid');", _gridId);

        return initializeKendoGrid;
    }

    private String convertFilterOperatorToKendoOperator(FilterOperator filterOperator) throws Exception {
        var kendoFilterOperator = "";
        switch (filterOperator)
        {
            case EqualTo:
                kendoFilterOperator = "eq";
                break;
            case NotEqualTo:
                kendoFilterOperator = "neq";
                break;
            case LessThan:
                kendoFilterOperator = "lt";
                break;
            case LessThanOrEqualTo:
                kendoFilterOperator = "lte";
                break;
            case GreaterThan:
                kendoFilterOperator = "gt";
                break;
            case GreaterThanOrEqualTo:
                kendoFilterOperator = "gte";
                break;
            case StartsWith:
                kendoFilterOperator = "startswith";
                break;
            case EndsWith:
                kendoFilterOperator = "endswith";
                break;
            case Contains:
                kendoFilterOperator = "contains";
                break;
            case NotContains:
                kendoFilterOperator = "doesnotcontain";
                break;
            case IsAfter:
                kendoFilterOperator = "gt";
                break;
            case IsAfterOrEqualTo:
                kendoFilterOperator = "gte";
                break;
            case IsBefore:
                kendoFilterOperator = "lt";
                break;
            case IsBeforeOrEqualTo:
                kendoFilterOperator = "lte";
                break;
            default:
                throw new Exception("The specified filter operator is not supported.");
        }

        return kendoFilterOperator;
    }

    private void waitForAjax()
    {
        _wait.until(d -> (Boolean)((JavascriptExecutor)d).executeScript("return jQuery.active == 0"));
    }
}
