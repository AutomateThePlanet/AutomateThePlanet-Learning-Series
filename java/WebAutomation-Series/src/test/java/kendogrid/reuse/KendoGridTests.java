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

package kendogrid.reuse;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;

import java.util.concurrent.TimeUnit;

public class KendoGridTests {
    private WebDriver driver;
    private IGridPage gridPage;
    private FreightColumnAsserter freightColumnAsserter;
    private OrderDateColumnAsserter orderDateColumnAsserter;
    private OrderIdColumnAsserter orderIdColumnAsserter;
    private ShipNameColumnAsserter shipNameColumnAsserter;
    private GridPageAsserter gridPageAsserter;

    @BeforeClass
    private void classInit() {
        WebDriverManager.chromedriver().setup();
    }

    @BeforeTest
    public void testSetup() {
        driver = new ChromeDriver();
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();

        gridPage = new GridFilterPage(driver);
        freightColumnAsserter = new FreightColumnAsserter(gridPage, driver);
        orderDateColumnAsserter = new OrderDateColumnAsserter(gridPage, driver);
        orderIdColumnAsserter = new OrderIdColumnAsserter(gridPage, driver);
        shipNameColumnAsserter = new ShipNameColumnAsserter(gridPage, driver);
        gridPageAsserter = new GridPageAsserter(gridPage, driver);

        driver.navigate().to("http://demos.telerik.com/kendo-ui/grid/remote-data-binding\"");
        var consentButton = driver.findElement(By.id("onetrust-accept-btn-handler"));
        consentButton.click();
    }

    @AfterClass
    public void afterClass() {
        driver.quit();
    }

    @Test
    public void orderIdEqualToFilter() throws Exception {
        orderIdColumnAsserter.orderIdEqualToFilter();
    }

    @Test
    public void orderIdGreaterThanOrEqualToFilter() throws Exception {
        orderIdColumnAsserter.orderIdGreaterThanOrEqualToFilter();
    }

    @Test
    public void orderIdGreaterThanFilter() throws Exception {
        orderIdColumnAsserter.orderIdGreaterThanFilter();
    }

    @Test
    public void OrderIdLessThanOrEqualToFilter() throws Exception {
        orderIdColumnAsserter.orderIdLessThanOrEqualToFilter();
    }

    @Test
    public void OrderIdLessThanFilter() throws Exception {
        orderIdColumnAsserter.orderIdLessThanFilter();
    }

    @Test
    public void OrderIdNotEqualToFilter() throws Exception {
        orderIdColumnAsserter.orderIdNotEqualToFilter();
    }

    @Test
    public void OrderIdClearFilter() throws Exception {
        orderIdColumnAsserter.orderIdClearFilter();
    }

    // ** OrderDate Test Cases ** (Date Type Column Test Cases)

    @Test
    public void OrderDateEqualToFilter() throws Exception {
        orderDateColumnAsserter.orderDateEqualToFilter();
    }

    @Test
    public void OrderDateNotEqualToFilter() throws Exception {
        orderDateColumnAsserter.orderDateNotEqualToFilter();
    }

    @Test
    public void OrderDateAfterFilter() throws Exception {
        orderDateColumnAsserter.orderDateAfterFilter();
    }

    @Test
    public void OrderDateIsAfterOrEqualToFilter() throws Exception {
        orderDateColumnAsserter.orderDateIsAfterOrEqualToFilter();
    }

    @Test
    public void OrderDateBeforeFilter() throws Exception {
        orderDateColumnAsserter.orderDateBeforeFilter();
    }

    @Test
    public void OrderDateIsBeforeOrEqualToFilter() throws Exception {
        orderDateColumnAsserter.orderDateIsBeforeOrEqualToFilter();
    }

    @Test
    public void OrderDateClearFilter() throws Exception {
        orderDateColumnAsserter.orderDateClearFilter();
    }

    @Test
    public void orderDateSortAsc() throws Exception {
        orderDateColumnAsserter.orderDateSortAsc();
    }

    @Test
    public void orderDateSortDesc() throws Exception {
        orderDateColumnAsserter.orderDateSortDesc();
    }

    // ** ShipName Test Cases) ** (Text Type Column Test Cases)
    @Test
    public void shipNameEqualToFilter() throws Exception {
        shipNameColumnAsserter.shipNameEqualToFilter();
    }

    @Test
    public void shipNameContainsFilter() throws Exception {
        shipNameColumnAsserter.shipNameContainsFilter();
    }

    @Test
    public void ShipNameEndsWithFilter() throws Exception {
        shipNameColumnAsserter.shipNameEndsWithFilter();
    }

    @Test
    public void ShipNameStartsWithFilter() throws Exception {
        shipNameColumnAsserter.shipNameStartsWithFilter();
    }

    @Test
    public void shipNameNotEqualToFilter() throws Exception {
        shipNameColumnAsserter.shipNameNotEqualToFilter();
    }

    @Test
    public void shipNameNotContainsFilter() throws Exception {
        shipNameColumnAsserter.shipNameNotContainsFilter();
    }

    @Test
    public void shipNameClearFilter() throws Exception {
        shipNameColumnAsserter.shipNameClearFilter();
    }

    // ** Freight Test Cases ** (Money Type Column Test Cases)
    @Test
    public void freightEqualToFilter() throws Exception {
        freightColumnAsserter.freightEqualToFilter();
    }

    @Test
    public void freightGreaterThanOrEqualToFilter() throws Exception {
        freightColumnAsserter.freightGreaterThanOrEqualToFilter();
    }

    @Test
    public void freightGreaterThanFilter() throws Exception {
        freightColumnAsserter.freightGreaterThanFilter();
    }

    @Test
    public void freightLessThanOrEqualToFilter() throws Exception {
        freightColumnAsserter.freightLessThanOrEqualToFilter();
    }

    @Test
    public void freightLessThanFilter() throws Exception {
        freightColumnAsserter.freightLessThanFilter();
    }

    @Test
    public void freightClearFilter() throws Exception {
        freightColumnAsserter.freightClearFilter();
    }

    // ** Paging Test Cases **
    @Test
    public void navigateToFirstPage_GoToFirstPageButton() throws Exception {
        gridPageAsserter.navigateToFirstPage_GoToFirstPageButton();
    }

    @Test
    public void navigateToLastPage_GoToLastPageButton() throws Exception {
        gridPageAsserter.navigateToLastPage_GoToLastPageButton();
    }

    @Test
    public void navigateToPageNine_GoToPreviousPageButton() throws Exception {
        gridPageAsserter.navigateToPageNine_GoToPreviousPageButton();
    }

    @Test
    public void navigateToPageTwo_GoToNextPageButton() throws Exception {
        gridPageAsserter.navigateToPageTwo_GoToNextPageButton();
    }

    @Test
    public void navigateToPageTwo_SecondPageButton() throws Exception {
        gridPageAsserter.navigateToPageTwo_SecondPageButton();
    }

    @Test
    public void navigateToLastPage_MorePagesNextButton() throws Exception {
        gridPageAsserter.navigateToLastPage_MorePagesNextButton();
    }

    @Test
    public void navigateToPageOne_MorePagesPreviousButton() throws Exception {
        gridPageAsserter.navigateToPageOne_MorePagesPreviousButton();
    }

    @Test
    public void goToFirstPageButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        gridPageAsserter.goToFirstPageButtonDisabled_WhenFirstPageIsLoaded();
    }

    @Test
    public void goToPreviousPageButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        gridPageAsserter.goToPreviousPageButtonDisabled_WhenFirstPageIsLoaded();
    }

    @Test
    public void previousMorePagesButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        gridPageAsserter.previousMorePagesButtonDisabled_WhenFirstPageIsLoaded();
    }

    @Test
    public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        gridPageAsserter.goToLastPageButtonDisabled_WhenLastPageIsLoaded();
    }

    @Test
    public void goToNextPageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        gridPageAsserter.goToNextPageButtonDisabled_WhenLastPageIsLoaded();
    }

    @Test
    public void nextMorePageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        gridPageAsserter.nextMorePageButtonDisabled_WhenLastPageIsLoaded();
    }
}