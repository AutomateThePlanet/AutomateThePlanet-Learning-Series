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

package kendogrid.simple;

import io.github.bonigarcia.wdm.WebDriverManager;
import kendogrid.components.*;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.*;

import java.util.List;
import java.util.concurrent.TimeUnit;

public class KendoGridTests {
    private WebDriver driver;
    private WebDriverWait wait;
    private KendoGrid kendoGrid;
    private final String OrderIdColumnName = "OrderID";
    private final String ShipNameColumnName = "ShipName";

    @BeforeClass
    public void classInit() {
        WebDriverManager.chromedriver().setup();
    }

    @BeforeTest
    public void testSetup() {
        driver = new ChromeDriver();
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        wait = new WebDriverWait(driver, 30);

        driver.navigate().to("https://demos.telerik.com/kendo-ui/grid/basic-usage");
        var consentButton = driver.findElement(By.id("onetrust-accept-btn-handler"));
        consentButton.click();

        kendoGrid = new KendoGrid(driver, driver.findElement(By.id("grid")));
    }

    @AfterTest
    public void afterClass() {
        driver.quit();
    }

    @Test
    public void filterContactName() throws Exception {
        kendoGrid.filter("ContactName", FilterOperator.CONTAINS, "Thomas");
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 1);
    }

    @Test
    public void sortContactTitleDesc() {
        kendoGrid.sort("ContactTitle", SortType.DESC);
        List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.get(0).getContactTitle(), "Sales Representative");
        Assert.assertEquals(items.get(1).getContactTitle(), "Sales Representative");
    }

    @Test
    public void testCurrentPage() {
        var pageNumber = kendoGrid.getCurrentPageNumber();

        Assert.assertEquals(pageNumber, 1);
    }

    @Test
    public void getPageSize() {
        var pageNumber = kendoGrid.getPageSize();

        Assert.assertEquals(pageNumber, 20);
    }

     @Test
    public void getAllItems() {
         List<GridItem> items = kendoGrid.getItems();

        Assert.assertEquals(items.stream().count(), 20);
    }
}