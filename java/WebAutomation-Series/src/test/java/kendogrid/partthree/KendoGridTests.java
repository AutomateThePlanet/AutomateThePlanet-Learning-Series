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

package kendogrid.partthree;

import io.github.bonigarcia.wdm.WebDriverManager;
import kendogrid.Order;
import kendogrid.components.*;
import org.apache.commons.lang3.StringUtils;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;

import java.util.LinkedList;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

public class KendoGridTests {
    private WebDriver driver;
    private WebDriverWait wait;
    private KendoGrid kendoGrid;
    private final String ShipNameColumnName = "ShipName";
    private String uniqueShippingName;
    private List<Order> testPagingItems;

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

    // ** Paging Test Cases **
    @Test
    public void navigateToFirstPage_GoToFirstPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 1;
        var goToFirstPageButton = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[1]"));
        goToFirstPageButton.click();
        waitForPageToLoad(targetPage, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());

        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }
    
    @Test
    public void navigateToLastPage_GoToLastPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        var goToLastPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[4]/span"));
        goToLastPage.click();
        waitForPageToLoad(targetPage, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(testPagingItems.stream().skip(testPagingItems.stream().count() - 1).findFirst().get().getOrderId(), results.stream().findFirst().get().getOrderId());
       assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    @Test
    public void navigateToPageNine_GoToPreviousPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 10;
        var goToPreviousPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[2]/span"));
        goToPreviousPage.click();
        waitForPageToLoad(targetPage, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
       assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    @Test
    public void navigateToPageTwo_GoToNextPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 2;
        var goToNextPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[3]"));
        goToNextPage.click();
        waitForPageToLoad(targetPage, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
       assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    @Test
    public void navigateToPageTwo_SecondPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 2;
        var pageOnSecondPositionButton = driver.findElement(By.xpath("//*[@id='grid']/div[3]/ul/li[3]/a"));
        pageOnSecondPositionButton.click();
        waitForPageToLoad(targetPage, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
       assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    @Test
    public void navigateToLastPage_MorePagesNextButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        var nextMorePages = driver.findElement(By.xpath("//*[@id='grid']/div[3]/ul/li[12]/a"));
        nextMorePages.click();
        waitForPageToLoad(targetPage, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
       assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    @Test
    public void NavigateToPageOne_MorePagesPreviousButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 1;
        var previousMorePages = driver.findElement(By.xpath("//*[@id='grid']/div[3]/ul/li[2]/a"));
        previousMorePages.click();
        waitForPageToLoad(targetPage, kendoGrid);
        List<Order> results = kendoGrid.getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
       assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    @Test
    public void goToFirstPageButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 1;
        var goToFirstPageButton = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[1]"));
        goToFirstPageButton.click();
        waitForPageToLoad(targetPage, kendoGrid);

        Assert.assertFalse(goToFirstPageButton.isEnabled());
    }

    @Test
    public void goToPreviousPageButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 1;
        var goToFirstPageButton = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[1]"));
        goToFirstPageButton.click();
        waitForPageToLoad(targetPage, kendoGrid);

        var goToPreviousPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[2]/span"));
        Assert.assertFalse(goToPreviousPage.isEnabled());
    }

    @Test
    public void previousMorePagesButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 1;
        var goToFirstPageButton = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[1]"));
        goToFirstPageButton.click();
        waitForPageToLoad(targetPage, kendoGrid);

        var previousMorePages = driver.findElement(By.xpath("//*[@id='grid']/div[3]/ul/li[2]/a"));
        Assert.assertFalse(previousMorePages.isDisplayed());
    }

    @Test
    public void goToLastPageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        var goToLastPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[4]/span"));
        goToLastPage.click();
        waitForPageToLoad(targetPage, kendoGrid);

        Assert.assertFalse(goToLastPage.isEnabled());
    }

    @Test
    public void goToNextPageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        var goToLastPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[4]/span"));
        goToLastPage.click();
        waitForPageToLoad(targetPage, kendoGrid);

        var goToNextPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[3]"));
        Assert.assertFalse(goToNextPage.isEnabled());
    }

    @Test
    public void nextMorePageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        var goToLastPage = driver.findElement(By.xpath("//*[@id='grid']/div[3]/a[4]/span"));
        goToLastPage.click();
        waitForPageToLoad(targetPage, kendoGrid);

        var previousMorePages = driver.findElement(By.xpath("//*[@id='grid']/div[3]/ul/li[2]/a"));
        Assert.assertFalse(previousMorePages.isEnabled());
    }

    private void navigateToGridInitialPage(int initialPageNumber) throws Exception {
        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/filter-row");
        var kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));
        kendoGrid.filter(ShipNameColumnName, FilterOperator.EQUAL_TO, uniqueShippingName);
        kendoGrid.changePageSize(1);
        waitForGridToLoad(1, kendoGrid);
        kendoGrid.navigateToPage(initialPageNumber);
        waitForPageToLoad(initialPageNumber, kendoGrid);

        assertPagerInfoLabel(initialPageNumber, initialPageNumber, testPagingItems.stream().count());
    }

    private void assertPagerInfoLabel(int startItems, int endItems, long totalItems) {
        var expectedLabel = String.format("%s - %s of %s items", startItems, endItems, totalItems);
        var pagerInfoLabel = driver.findElement(By.xpath("//*[@id='grid']/div[3]/span"));
        Assert.assertEquals(pagerInfoLabel.getText(), expectedLabel);
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

    private void initializeInvoicesForPaging() {
        var totalOrders = 11;
        if (!StringUtils.isEmpty(uniqueShippingName)) {
            uniqueShippingName = UUID.randomUUID().toString();
        }

        testPagingItems = new LinkedList<Order>();
        for (var i = 0; i < totalOrders; i++) {
            var newOrder = createNewItemInDb(uniqueShippingName);
            testPagingItems.add(newOrder);
        }
    }
}