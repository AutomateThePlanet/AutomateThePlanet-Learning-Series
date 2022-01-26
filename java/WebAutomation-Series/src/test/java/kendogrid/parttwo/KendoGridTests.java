/*
 * Copyright 2020 Automate The Planet Ltd.
 * Author: Anton Angelov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package kendogrid.parttwo;

import io.github.bonigarcia.wdm.WebDriverManager;
import kendogrid.Order;
import kendogrid.components.*;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDateTime;
import java.util.Comparator;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.TimeUnit;
import java.util.stream.Collectors;

public class KendoGridTests {
    private WebDriver driver;
    private WebDriverWait wait;
    private KendoGrid kendoGrid;
    private final String OrderIdColumnName = "OrderID";
    private final String ShipNameColumnName = "ShipName";
    private final String FreightColumnName = "Freight";
    private final String OrderDateColumnName = "OrderDate";

    @BeforeClass
    private void classInit() {
        WebDriverManager.chromedriver().setup();
    }

    @BeforeTest
    public void testSetup() {
        driver = new ChromeDriver();
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        wait = new WebDriverWait(driver, 30);

        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding\"");
        var consentButton = driver.findElement(By.id("onetrust-accept-btn-handler"));
        consentButton.click();

        kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));
    }

    @AfterClass
    public void afterClass() {
        driver.quit();
    }

    @Test
    public void orderDateEqualToFilter() throws Exception {

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderId)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
        newItem.setOrderDate(lastOrderDate.plusDays(1));
        updateItemInDb(newItem);

        kendoGrid.filter(OrderDateColumnName, FilterOperator.EQUAL_TO, newItem.getOrderDate().toString());
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(newItem.getOrderDate().toString(), results.get(0).getOrderDate());
    }

    @Test
    public void orderDateNotEqualToFilter() throws Exception {
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
        kendoGrid.filter(
                new GridFilter(OrderDateColumnName, FilterOperator.NOT_EQUAL_TO, newItem.getOrderDate().toString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
    }

    @Test
    public void orderDateAfterFilter() throws Exception {
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
        kendoGrid.filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IS_AFTER, newItem.getOrderDate().toString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
    }

    @Test
    public void orderDateIsAfterOrEqualToFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

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
        kendoGrid.filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IS_AFTER_OR_EQUAL_TO, newItem.getOrderDate().toString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));
        waitForGridToLoadAtLeast(2, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(newItem.toString(), results.get(1).getOrderDate());
    }

    @Test
    public void orderDateBeforeFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

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
        kendoGrid.filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IS_BEFORE, newItem.getOrderDate().toString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
    }

    @Test
    public void orderDateIsBeforeOrEqualToFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
       newItem.setOrderDate(lastOrderDate.plusDays(-1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(-2));
        updateItemInDb(secondNewItem);

        kendoGrid.filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IS_BEFORE_OR_EQUAL_TO, newItem.getOrderDate().toString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));
        waitForGridToLoadAtLeast(2, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(newItem.toString(), results.get(1).getOrderDate());
    }

    @Test
    public void orderDateClearFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var newItem = createNewItemInDb();

        kendoGrid.filter(OrderDateColumnName, FilterOperator.IS_AFTER, LocalDateTime.MAX.toString());
        waitForGridToLoad(0, kendoGrid);
        kendoGrid.removeFilters();

        waitForGridToLoadAtLeast(1, kendoGrid);
    }

    @Test
    public void orderDateSortAsc() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
       newItem.setOrderDate(lastOrderDate.plusDays(-1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(-2));
        updateItemInDb(secondNewItem);

        kendoGrid.filter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName());
        waitForGridToLoadAtLeast(2, kendoGrid);
        kendoGrid.sort(OrderDateColumnName, SortType.ASC);
        Thread.sleep(1000);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(secondNewItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(newItem.toString(), results.get(1).getOrderDate());
    }

    @Test
    public void orderDateSortDesc() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getOrderDate)).collect(Collectors.toList());
        var lastOrderDate = allItems.stream().findFirst().get().getOrderDate();

        var newItem = createNewItemInDb();
       newItem.setOrderDate(lastOrderDate.plusDays(-1));
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setOrderDate(lastOrderDate.plusDays(-2));
        updateItemInDb(secondNewItem);

        kendoGrid.filter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName());
        waitForGridToLoadAtLeast(2, kendoGrid);
        kendoGrid.sort(OrderDateColumnName, SortType.DESC);
        Thread.sleep(1000);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 2);
        Assert.assertEquals(newItem.toString(), results.get(0).getOrderDate());
        Assert.assertEquals(secondNewItem.toString(), results.get(1).getOrderDate());
    }

    // ** Freight Test Cases ** (Money Type Column Test Cases)
    @Test
    public void freightEqualToFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var newItem = createNewItemInDb();
        newItem.setFreight(getUniqueNumberValue());
        updateItemInDb(newItem);

        kendoGrid.filter(FreightColumnName, FilterOperator.EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 1);
        Assert.assertEquals(Double.toString(newItem.getFreight()), results.get(0).getFreight());
    }

    @Test
    public void freightGreaterThanOrEqualToFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var biggestFreight = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(biggestFreight + getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() + 1).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        kendoGrid.filter(FreightColumnName, FilterOperator.GREATER_THAN_OR_EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(2, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 2);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == newItem.getFreight()).count(), 1);
    }

    @Test
    public void freightGreaterThanFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var biggestFreight = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(biggestFreight + getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() + 1).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        kendoGrid.filter(FreightColumnName, FilterOperator.GREATER_THAN, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 1);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
    }

    @Test
    public void freightLessThanOrEqualToFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

       var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
       var smallestFreight = allItems.stream().findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(smallestFreight - getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() - 0.01).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        kendoGrid.filter(FreightColumnName, FilterOperator.LESS_THAN_OR_EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(2, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 2);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
        Assert.assertEquals(results.stream().filter(x -> x.getFreight() == newItem.getFreight()).count(), 1);
    }

    @Test
    public void freightLessThanFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var smallestFreight = allItems.stream().findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(BigDecimal.valueOf(smallestFreight - getUniqueNumberValue()).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(BigDecimal.valueOf(newItem.getFreight() - 0.01).setScale(3, RoundingMode.HALF_UP).doubleValue());
        updateItemInDb(secondNewItem);

        kendoGrid.filter(FreightColumnName, FilterOperator.LESS_THAN, Double.toString(newItem.getFreight()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 1);

        // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
           Assert.assertEquals(results.stream().filter(x -> x.getFreight() == secondNewItem.getFreight()).count(), 1);
    }
    
    @Test
    public void freightNotEqualToFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var newItem = createNewItemInDb();
        newItem.setFreight(getUniqueNumberValue());
        updateItemInDb(newItem);

        // After we apply the orderId filter, only 1 item is displayed in the grid. When we apply the NotEqualTo filter this item will disappear.
        kendoGrid.filter(
                new GridFilter(FreightColumnName, FilterOperator.NOT_EQUAL_TO, Double.toString(newItem.getFreight())),
                new GridFilter(OrderIdColumnName, FilterOperator.EQUAL_TO, newItem.getOrderId().toString()));
        waitForGridToLoad(0, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() == 0);
    }

    @Test
    public void freightClearFilter() throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));

        var allItems = getAllItemsFromDb().stream().sorted(Comparator.comparing(Order::getFreight)).collect(Collectors.toList());
        var biggestFreight = allItems.stream().skip(allItems.stream().count() - 1).findFirst().get().getFreight();

        var newItem = createNewItemInDb();
        newItem.setFreight(biggestFreight + getUniqueNumberValue());
        updateItemInDb(newItem);

        var secondNewItem = createNewItemInDb(newItem.getShipName());
        secondNewItem.setFreight(newItem.getFreight() + 1);
        updateItemInDb(secondNewItem);

        kendoGrid.filter(FreightColumnName, FilterOperator.EQUAL_TO, Double.toString(newItem.getFreight()));
        waitForGridToLoad(1, kendoGrid);
        kendoGrid.removeFilters();

        waitForGridToLoadAtLeast(2, kendoGrid);
    }

    private void waitForGridToLoad(int expectedCount, KendoGrid grid) {
        wait.until(x ->
        {
            List<GridItem> items = grid.getItems();
            return expectedCount == items.stream().count();
        });
    }

    private void waitForGridToLoadAtLeast(int expectedCount, KendoGrid grid)
    {
        wait.until(x ->
        {
            List<GridItem> items = grid.getItems();
            return items.stream().count() >= expectedCount;
        });
    }

    private Order createNewItemInDb(String shipName) {
        // Replace it with service oriented call to your DB. Create real entity in DB.
        return new Order(shipName);
    }

    private Order createNewItemInDb() {
        // Replace it with service oriented call to your DB. Create real entity in DB.
        return new Order("");
    }

    private List<Order> getAllItemsFromDb() {
        // Create dummy orders. This logic should be replaced with service oriented call to your DB and get all items that are populated in the grid.
        var orders = new LinkedList<Order>();
        for (var i = 0; i < 10; i++)
        {
            orders.add(new Order());
        }

        return orders;
    }

    private void updateItemInDb(Order order) {
        // Replace it with service oriented call to your DB. Update the enity in the DB.
    }

    private double getUniqueNumberValue() {
        var currentTime = LocalDateTime.now();
        var result = currentTime.getYear() + currentTime.getMonthValue() + currentTime.getHour() + currentTime.getMinute() + currentTime.getSecond() + currentTime.getNano();
        return result;
    }
}