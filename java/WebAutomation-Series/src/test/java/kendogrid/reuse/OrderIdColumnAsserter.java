package kendogrid.reuse;

import kendogrid.Order;
import kendogrid.components.FilterOperator;
import kendogrid.components.GridFilter;
import kendogrid.components.GridItem;
import kendogrid.components.SortType;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Test;

import java.time.LocalDateTime;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

public class OrderIdColumnAsserter extends GridColumnAsserter {
    public OrderIdColumnAsserter(IGridPage gridPage, WebDriver driver) {
        super(gridPage, driver);
    }

    @Test
    public void orderIdEqualToFilter() throws Exception {
        getGridPage().navigateTo();
        var newItem = createNewItemInDb();

        getGridPage().getGrid().filter(GridColumns.ORDER_ID, FilterOperator.EqualTo, newItem.getOrderId());
        waitForGridToLoad(1, getGridPage().getGrid());
        List<GridItem> items = getGridPage().getGrid().getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    public void orderIdGreaterThanOrEqualToFilter() throws Exception {
        getGridPage().navigateTo();
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // When we filter by the second unique column ShippingName, only one item will be displayed. Once we apply the second not equal to filter the grid should be empty.
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.GreaterThanOrEqualTo, newItem.getOrderId()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));

        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == secondNewItem.getShipName()).findFirst().get().getOrderId(), secondNewItem.getOrderId());
        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 2);
    }

    public void orderIdGreaterThanFilter() throws Exception {
        getGridPage().navigateTo();
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the smaller orderId but also by the second unique column in this case shipping name
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.GreaterThan, newItem.getOrderId()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == secondNewItem.getShipName()).findFirst().get().getOrderId(), secondNewItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 1);
    }

    public void orderIdLessThanOrEqualToFilter() throws Exception {
        getGridPage().navigateTo();
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the larger orderId but also by the second unique column in this case shipping name
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.LessThanOrEqualTo, secondNewItem.getOrderId()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));
        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == secondNewItem.getShipName()).skip(results.stream().count() - 1).findFirst().get().getOrderId(), secondNewItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 2);
    }

    public void orderIdLessThanFilter() throws Exception {
        getGridPage().navigateTo();
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the larger orderId but also by the second unique column in this case shipping name
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.LessThan, secondNewItem.getOrderId()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, secondNewItem.getShipName()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 1);
    }

    public void orderIdNotEqualToFilter() throws Exception {
        getGridPage().navigateTo();
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the larger orderId but also by the second unique column in this case shipping name
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.NotEqualTo, secondNewItem.getOrderId()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, secondNewItem.getShipName()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 1);
    }

    public void orderIdClearFilter() throws Exception {
        getGridPage().navigateTo();
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Make sure that we have at least 2 items if the grid is empty. The tests are designed to run against empty DB.
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        getGridPage().getGrid().filter(GridColumns.ORDER_ID, FilterOperator.EqualTo, newItem.getOrderId());
        waitForGridToLoad(1, getGridPage().getGrid());
        getGridPage().getGrid().removeFilters();

        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() > 1);
    }
}
