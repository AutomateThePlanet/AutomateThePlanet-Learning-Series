package kendogrid.reuse;

import kendogrid.Order;
import kendogrid.components.GridItem;
import kendogrid.components.KendoGrid;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.time.LocalDateTime;
import java.util.LinkedList;
import java.util.List;

public class GridColumnAsserter {
    protected IGridPage GridPage;
    private WebDriver driver;
    private WebDriverWait wait;

    public GridColumnAsserter(IGridPage gridPage, WebDriver driver) {
        GridPage = gridPage;
        this.driver = driver;
        this.wait = new WebDriverWait(driver, 30);
    }

    public IGridPage getGridPage() {
        return GridPage;
    }

    protected void waitUntilLoaded() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            String isReady = (String) javascriptExecutor.executeScript("return document.readyState");
            return isReady.equals("complete");
        });
    }

    protected void waitForAjaxComplete() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            Boolean isAjaxCallComplete  = (Boolean) javascriptExecutor.executeScript("return window.jQuery != undefined && jQuery.active == 0");
            return isAjaxCallComplete ;
        });
    }

    protected void waitForPageToLoad(int expectedPage, KendoGrid grid) {
        wait.until(x ->
        {
            var currentPage = grid.getCurrentPageNumber();
            return currentPage == expectedPage;
        });
    }

    protected void waitForGridToLoad(int expectedCount, KendoGrid grid) {
        wait.until(x ->
        {
            List<GridItem> items = grid.getItems();
            return expectedCount == items.stream().count();
        });
    }

    protected void waitForGridToLoadAtLeast(int expectedCount, KendoGrid grid)
    {
        wait.until(x ->
        {
            List<GridItem> items = grid.getItems();
            return items.stream().count() >= expectedCount;
        });
    }

    protected Order createNewItemInDb(String shipName) {
        // Replace it with service oriented call to your DB. Create real entity in DB.
        return new Order(shipName);
    }

    protected Order createNewItemInDb() {
        // Replace it with service oriented call to your DB. Create real entity in DB.
        return new Order("");
    }

    protected List<Order> getAllItemsFromDb() {
        // Create dummy orders. This logic should be replaced with service oriented call to your DB and get all items that are populated in the grid.
        var orders = new LinkedList<Order>();
        for (var i = 0; i < 10; i++)
        {
            orders.add(new Order());
        }

        return orders;
    }

    protected void updateItemInDb(Order order) {
        // Replace it with service oriented call to your DB. Update the enity in the DB.
    }

    protected double getUniqueNumberValue() {
        var currentTime = LocalDateTime.now();
        var result = currentTime.getYear() + currentTime.getMonthValue() + currentTime.getHour() + currentTime.getMinute() + currentTime.getSecond() + currentTime.getNano();
        return result;
    }
}
