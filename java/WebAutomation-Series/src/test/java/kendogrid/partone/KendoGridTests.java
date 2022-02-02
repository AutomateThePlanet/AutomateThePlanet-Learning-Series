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

package kendogrid.partone;

import io.github.bonigarcia.wdm.WebDriverManager;
import kendogrid.Order;
import kendogrid.components.FilterOperator;
import kendogrid.components.GridFilter;
import kendogrid.components.GridItem;
import kendogrid.components.KendoGrid;
import kendogrid.reuse.GridColumns;
import org.openqa.selenium.*;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;

import java.util.List;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

import static java.lang.System.getProperty;

public class KendoGridTests {
    private WebDriver driver;
    private WebDriverWait wait;
    private KendoGrid kendoGrid;
    private final String OrderIdColumnName = "OrderID";
    private final String ShipNameColumnName = "ShipName";

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

        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/frozen-columns");
        var consentButton = driver.findElement(By.id("onetrust-accept-btn-handler"));
        consentButton.click();

        kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));
    }

    @AfterClass
    public void afterClass() {
        driver.quit();
    }

    // OrderID Test Cases (Unique Identifier Type Column Test Cases)/ OrderID Test Cases
    @Test
    public void orderIdEqualToFilter() throws Exception {
        var newItem = createNewItemInDb();

        kendoGrid.filter(OrderIdColumnName, FilterOperator.EQUAL_TO, newItem.getOrderId());
        waitForGridToLoad(1, kendoGrid);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    @Test
    public void orderIdGreaterThanOrEqualToFilter() throws Exception {
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // When we filter by the second unique column ShippingName, only one item will be displayed. Once we apply the second not equal to filter the grid should be empty.
        kendoGrid.filter(
                new GridFilter(OrderIdColumnName, FilterOperator.GREATER_THAN_OR_EQUAL_TO, newItem.getOrderId()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));

        waitForGridToLoadAtLeast(2, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == secondNewItem.getShipName()).findFirst().get().getOrderId(), secondNewItem.getOrderId());
        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 2);
    }

    @Test
    public void orderIdGreaterThanFilter() throws Exception {
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the smaller orderId but also by the second unique column in this case shipping name
        kendoGrid.filter(
                new GridFilter(OrderIdColumnName, FilterOperator.GREATER_THAN, newItem.getOrderId()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == secondNewItem.getShipName()).findFirst().get().getOrderId(), secondNewItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 1);
    }

    @Test
    public void orderIdLessThanOrEqualToFilter() throws Exception {
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the larger orderId but also by the second unique column in this case shipping name
        kendoGrid.filter(
                new GridFilter(OrderIdColumnName, FilterOperator.LESS_THAN_OR_EQUAL_TO, secondNewItem.getOrderId()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, newItem.getShipName()));
        waitForGridToLoadAtLeast(2, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == secondNewItem.getShipName()).skip(results.stream().count() - 1).findFirst().get().getOrderId(), secondNewItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 2);
    }

    @Test
    public void orderIdLessThanFilter() throws Exception {
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the larger orderId but also by the second unique column in this case shipping name
        kendoGrid.filter(
                new GridFilter(OrderIdColumnName, FilterOperator.LESS_THAN, secondNewItem.getOrderId()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, secondNewItem.getShipName()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 1);
    }

    @Test
    public void orderIdNotEqualToFilter() throws Exception {
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Create second new item with the same unique shipping name
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        // Filter by the larger orderId but also by the second unique column in this case shipping name
        kendoGrid.filter(
                new GridFilter(OrderIdColumnName, FilterOperator.NOT_EQUAL_TO, secondNewItem.getOrderId()),
                new GridFilter(ShipNameColumnName, FilterOperator.EQUAL_TO, secondNewItem.getShipName()));
        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(results.stream().filter(x -> x.getShipName() == newItem.getShipName()).findFirst().get().getOrderId(), newItem.getOrderId());
        Assert.assertTrue(results.stream().count() == 1);
    }

    @Test
    public void orderIdClearFilter() throws Exception {
        // Create new item with unique ship name;
        var newItem = createNewItemInDb();

        // Make sure that we have at least 2 items if the grid is empty. The tests are designed to run against empty DB.
        var secondNewItem = createNewItemInDb(newItem.getShipName());

        kendoGrid.filter(OrderIdColumnName, FilterOperator.EQUAL_TO, newItem.getOrderId());
        waitForGridToLoad(1, kendoGrid);
        kendoGrid.removeFilters();

        waitForGridToLoadAtLeast(1, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertTrue(results.stream().count() > 1);
    }

    // ship name tests
    @Test
    public void shipNameEqualToFilter() throws Exception {
        var newItem = createNewItemInDb();

        kendoGrid.filter(GridColumns.SHIP_NAME, FilterOperator.EQUAL_TO, newItem.getShipName());
        waitForGridToLoad(1, kendoGrid);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    @Test
    public void shipNameContainsFilter() throws Exception {
        var shipName = UUID.randomUUID().toString();

        // Remove first and last letter
        shipName = removeLastChar(removeFirstChar(shipName));
        var newItem = createNewItemInDb(shipName);

        kendoGrid.filter(GridColumns.SHIP_NAME, FilterOperator.CONTAINS, newItem.getShipName());
        waitForGridToLoad(1, kendoGrid);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    @Test
    public void shipNameEndsWithFilter() throws Exception {
        // Remove first letter
        var shipName = UUID.randomUUID().toString();
        shipName = removeFirstChar(shipName);
        var newItem = createNewItemInDb(shipName);

        kendoGrid.filter(GridColumns.SHIP_NAME, FilterOperator.ENDS_WITH, newItem.getShipName());
        waitForGridToLoad(1, kendoGrid);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    @Test
    public void shipNameStartsWithFilter() throws Exception {
        // Remove last letter
        var shipName = UUID.randomUUID().toString();
        shipName = removeFirstChar(shipName);
        var newItem = createNewItemInDb(shipName);

        kendoGrid.filter(GridColumns.SHIP_NAME, FilterOperator.STARTS_WITH, newItem.getShipName());
        waitForGridToLoad(1, kendoGrid);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    @Test
    public void shipNameNotEqualToFilter() throws Exception {
        // Apply combined filter. First filter by ID and than by ship name (not equal filter).
        // After the first filter there is only one element when we apply the second we expect 0 elements.
        var newItem = createNewItemInDb();

        kendoGrid.filter(
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.NOT_EQUAL_TO, newItem.getShipName()),
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.EQUAL_TO, newItem.getOrderId().toString()));
        waitForGridToLoad(0, kendoGrid);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 0);
    }

    @Test
    public void shipNameNotContainsFilter() throws Exception {
        // Remove first and last letter
        var shipName = UUID.randomUUID().toString();
        shipName = removeLastChar(removeFirstChar(shipName));
        var newItem = createNewItemInDb(shipName);

        // Apply combined filter. First filter by ID and than by ship name (not equal filter).
        // After the first filter there is only one element when we apply the second we expect 0 elements.
        kendoGrid.filter(
                new GridFilter(GridColumns.SHIP_NAME, FilterOperator.NOT_CONTAINS, newItem.getShipName()),
                new GridFilter(GridColumns.ORDER_ID, FilterOperator.EQUAL_TO, newItem.getOrderId().toString()));
        waitForGridToLoad(0, kendoGrid);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 0);
    }

    @Test
    public void shipNameClearFilter() throws Exception {
        createNewItemInDb();

        // Filter by GUID and we know we wait the grid to be empty
        kendoGrid.filter(GridColumns.SHIP_NAME, FilterOperator.STARTS_WITH, UUID.randomUUID().toString());
        waitForGridToLoad(0, kendoGrid);

        // Remove all filters and we expect that the grid will contain at least 1 item.
        kendoGrid.removeFilters();
        waitForGridToLoadAtLeast(1, kendoGrid);
    }

    private String removeFirstChar(String s) {
        return s.substring(1);
    }

    private String removeLastChar(String s) {
        return s.substring(0, s.length() - 1);
    }

    private void waitUntilLoaded() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            String isReady = (String) javascriptExecutor.executeScript("return document.readyState");
            return isReady.equals("complete");
        });
    }

    private void waitForAjaxComplete() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            Boolean isAjaxCallComplete  = (Boolean) javascriptExecutor.executeScript("return window.jQuery != undefined && jQuery.active == 0");
            return isAjaxCallComplete ;
        });
    }

    public void waitForPageToLoad(int expectedPage, KendoGrid grid) {
        wait.until(x ->
        {
            var currentPage = grid.getCurrentPageNumber();
            return currentPage == expectedPage;
        });
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
}