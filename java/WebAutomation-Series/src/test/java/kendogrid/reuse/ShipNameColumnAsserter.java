package kendogrid.reuse;

import kendogrid.components.FilterOperator;
import kendogrid.components.GridFilter;
import kendogrid.components.GridItem;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;

import java.util.List;
import java.util.UUID;

public class ShipNameColumnAsserter extends GridColumnAsserter {
    public ShipNameColumnAsserter(IGridPage gridPage, WebDriver driver) {
        super(gridPage, driver);
    }

    public void shipNameEqualToFilter() throws Exception {
        getGridPage().navigateTo();
        var newItem = createNewItemInDb();

        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.EQUAL_TO, newItem.getShipName());
        waitForGridToLoad(1, getGridPage().getGrid());
        List<GridItem> items = getGridPage().getGrid().getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    public void shipNameContainsFilter() throws Exception {
        getGridPage().navigateTo();
        var shipName = UUID.randomUUID().toString();

        // Remove first and last letter
        shipName = removeLastChar(removeFirstChar(shipName));
        var newItem = createNewItemInDb(shipName);

        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.CONTAINS, newItem.getShipName());
        waitForGridToLoad(1, getGridPage().getGrid());
        List<GridItem> items = getGridPage().getGrid().getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    public void shipNameEndsWithFilter() throws Exception {
        getGridPage().navigateTo();

        // Remove first letter
        var shipName = UUID.randomUUID().toString();
        shipName = removeFirstChar(shipName);
        var newItem = createNewItemInDb(shipName);

        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.ENDS_WITH, newItem.getShipName());
        waitForGridToLoad(1, getGridPage().getGrid());
        List<GridItem> items = getGridPage().getGrid().getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    public void shipNameStartsWithFilter() throws Exception {
        getGridPage().navigateTo();

        // Remove last letter
        var shipName = UUID.randomUUID().toString();
        shipName = removeFirstChar(shipName);
        var newItem = createNewItemInDb(shipName);

        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.STARTS_WITH, newItem.getShipName());
        waitForGridToLoad(1, getGridPage().getGrid());
        List<GridItem> items = getGridPage().getGrid().getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    public void shipNameNotEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        // Apply combined filter. First filter by ID and than by ship name (not equal filter).
        // After the first filter there is only one element when we apply the second we expect 0 elements.
        var newItem = createNewItemInDb();

        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.NOT_EQUAL_TO, newItem.getShipName()),
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.EQUAL_TO, newItem.getOrderId().toString()));
        waitForGridToLoad(0, getGridPage().getGrid());
        List<GridItem> items = getGridPage().getGrid().getItems();

        Assert.assertEquals(items.stream().count(), 0);
    }

    public void shipNameNotContainsFilter() throws Exception {
        getGridPage().navigateTo();

        // Remove first and last letter
        var shipName = UUID.randomUUID().toString();
        shipName = removeLastChar(removeFirstChar(shipName));
        var newItem = createNewItemInDb(shipName);

        // Apply combined filter. First filter by ID and than by ship name (not equal filter).
        // After the first filter there is only one element when we apply the second we expect 0 elements.
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.NOT_CONTAINS, newItem.getShipName()),
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.EQUAL_TO, newItem.getOrderId().toString()));
        waitForGridToLoad(0, getGridPage().getGrid());
        List<GridItem> items = getGridPage().getGrid().getItems();

        Assert.assertEquals(items.stream().count(), 0);
    }

    public void shipNameClearFilter() throws Exception {
        getGridPage().navigateTo();
        createNewItemInDb();

        // Filter by GUID and we know we wait the grid to be empty
        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.STARTS_WITH, UUID.randomUUID().toString());
        waitForGridToLoad(0, getGridPage().getGrid());

        // Remove all filters and we expect that the grid will contain at least 1 item.
        getGridPage().getGrid().removeFilters();
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
    }

    private String removeFirstChar(String s) {
        return s.substring(1);
    }

    private String removeLastChar(String s) {
        return s.substring(0, s.length() - 1);
    }
}
