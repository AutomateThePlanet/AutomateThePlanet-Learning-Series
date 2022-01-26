package kendogrid.reuse;

import kendogrid.Order;
import kendogrid.components.FilterOperator;
import kendogrid.components.GridFilter;
import kendogrid.components.KendoGrid;
import kendogrid.components.SortType;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.Test;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDateTime;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

public class OrderDateColumnAsserter extends GridColumnAsserter {
    public OrderDateColumnAsserter(IGridPage gridPage, WebDriver driver) {
        super(gridPage, driver);
    }

    public void orderDateEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderId)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(1));
        updateItemInDb(newItem);

        getGridPage().getGrid().filter(GridColumns.ORDER_DATE, FilterOperator.EqualTo, newItem.getOrderDate().toString());
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(newItem.getOrderDate().toString(), results.get(0).getOrderDate());
    }

    public void orderDateNotEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(2));
        updateItemInDb(secondNewItem);

        // After we filter by the unique shipping name, two items will be displayed in the grid.
        // After we apply the date after filter only the second item should be visible in the grid.
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_DATE, FilterOperator.NotEqualTo, newItem.getOrderDate().toString()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
    }

    public void orderDateAfterFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(2));
        updateItemInDb(secondNewItem);

        // After we filter by the unique shipping name, two items will be displayed in the grid.
        // After we apply the date after filter only the second item should be visible in the grid.
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_DATE, FilterOperator.IsAfter, newItem.getOrderDate().toString()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
    }

    public void orderDateIsAfterOrEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(2));
        updateItemInDb(secondNewItem);

        // After we filter by the unique shipping name, two items will be displayed in the grid.
        // After we apply the date after filter only the second item should be visible in the grid.
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_DATE, FilterOperator.IsAfterOrEqualTo, newItem.getOrderDate().toString()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));
        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(newItem.toString(), results.get(1).getOrderDate());
    }

    public void orderDateBeforeFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(-1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(-2));
        updateItemInDb(secondNewItem);

        // After we filter by the unique shipping name, two items will be displayed in the grid.
        // After we apply the date after filter only the second item should be visible in the grid.
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_DATE, FilterOperator.IsBefore, newItem.getOrderDate().toString()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
    }

    public void orderDateIsBeforeOrEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(-1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(-2));
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.ORDER_DATE, FilterOperator.IsBeforeOrEqualTo, newItem.getOrderDate().toString()),
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName()));
        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(newItem.toString(), results.get(1).getOrderDate());
    }

    public void orderDateClearFilter() throws Exception {
        getGridPage().navigateTo();

        var newItem = createNewItemInDb();

        getGridPage().getGrid().filter(GridColumns.ORDER_DATE, FilterOperator.IsAfter, LocalDateTime.MAX.toString());
        waitForGridToLoad(0, getGridPage().getGrid());
        getGridPage().getGrid().removeFilters();

        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
    }

    public void orderDateSortAsc() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(-1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(-2));
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName());
        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        getGridPage().getGrid().sort(GridColumns.ORDER_DATE, SortType.ASC);
        Thread.sleep(1000);
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(newItem.toString(), results.get(1).getOrderDate());
    }

    public void orderDateSortDesc() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(-1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(-2));
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.EqualTo, newItem.getShipName());
        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        getGridPage().getGrid().sort(GridColumns.ORDER_DATE, SortType.DESC);
        Thread.sleep(1000);
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(newItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(secondNewItem.toString(), results.get(1).getOrderDate());
    }
}
