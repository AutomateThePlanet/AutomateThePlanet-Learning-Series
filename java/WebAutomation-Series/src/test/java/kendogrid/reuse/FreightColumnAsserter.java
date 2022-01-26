package kendogrid.reuse;

import kendogrid.Order;
import kendogrid.components.FilterOperator;
import kendogrid.components.GridFilter;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

public class FreightColumnAsserter extends GridColumnAsserter {
    public FreightColumnAsserter(IGridPage gridPage, WebDriver driver) {
        super(gridPage, driver);
    }

    public void freightEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var newItem = createNewItemInDb();
        newItem.setFreight(getUniqueNumberValue());
        updateItemInDb(newItem);

        getGridPage().getGrid().filter(GridColumns.FREIGHT, FilterOperator.EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(Double.toString(newItem.getFreight()), results.get(0).getFreight());
    }

    public void freightGreaterThanOrEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var biggestFreight = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(biggestFreight + getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() + 1).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(GridColumns.FREIGHT, FilterOperator.GREATER_THAN_OR_EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 2);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == newItem.getFreight()).count(), 1);
    }

    public void freightGreaterThanFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var biggestFreight = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(biggestFreight + getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() + 1).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(GridColumns.FREIGHT, FilterOperator.GREATER_THAN, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 1);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
    }

    public void freightLessThanOrEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var smallestFreight = allItems.stream().findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(smallestFreight - getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() - 0.01).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(GridColumns.FREIGHT, FilterOperator.LESS_THAN_OR_EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 2);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == newItem.getFreight()).count(), 1);
    }

    public void freightLessThanFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var smallestFreight = allItems.stream().findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(smallestFreight - getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() - 0.01).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(GridColumns.FREIGHT, FilterOperator.LESS_THAN, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(1, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 1);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
    }

    public void FreightNotEqualToFilter() throws Exception {
        getGridPage().navigateTo();

        var newItem = createNewItemInDb();
        newItem.setFreight(getUniqueNumberValue());
        updateItemInDb(newItem);

        // After we apply the orderId filter, only 1 item is displayed in the grid. When we apply the NotEqualTo filter this item will disappear.
        getGridPage().getGrid().filter(
                new GridFilter(GridColumns.FREIGHT, FilterOperator.NOT_EQUAL_TO, Double.toString(newItem.getFreight())),
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.EQUAL_TO, newItem.getOrderId().toString()));
        waitForGridToLoad(0, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertTrue(results.stream().count() == 0);
    }

    public void freightClearFilter() throws Exception {
        getGridPage().navigateTo();

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var biggestFreight = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(biggestFreight + getUniqueNumberValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(newItem.getFreight() + 1);
        updateItemInDb(secondNewItem);

        getGridPage().getGrid().filter(GridColumns.FREIGHT, FilterOperator.EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoad(1, getGridPage().getGrid());
        getGridPage().getGrid().removeFilters();

        waitForGridToLoadAtLeast(2, getGridPage().getGrid());
    }
}
