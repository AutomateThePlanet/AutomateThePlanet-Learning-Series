package kendogrid.reuse;

import kendogrid.Order;
import kendogrid.components.FilterOperator;
import org.apache.commons.lang3.StringUtils;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.testng.Assert;

import java.util.LinkedList;
import java.util.List;
import java.util.UUID;

public class GridPageAsserter extends GridColumnAsserter {
    private WebDriver driver;
    private String uniqueShippingName;
    private List<Order> testPagingItems;

    public GridPageAsserter(IGridPage gridPage, WebDriver driver) {
        super(gridPage, driver);
        this.driver = driver;
    }

    public void navigateToFirstPage_GoToFirstPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 1;
        getGridPage().getGoToFirstPageButton().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());

        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    public void navigateToLastPage_GoToLastPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        getGridPage().getGoToLastPage().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(testPagingItems.stream().skip(testPagingItems.stream().count() - 1).findFirst().get().getOrderId(), results.stream().findFirst().get().getOrderId());
        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    public void navigateToPageNine_GoToPreviousPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 10;
        getGridPage().getGoToPreviousPage().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    public void navigateToPageTwo_GoToNextPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 2;
        getGridPage().getGoToNextPage().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    public void navigateToPageTwo_SecondPageButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 2;
        getGridPage().getPageOnSecondPositionButton().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    public void navigateToLastPage_MorePagesNextButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        getGridPage().getNextMorePages().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    public void navigateToPageOne_MorePagesPreviousButton() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 1;
        getGridPage().getPreviousMorePages().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());
        List<Order> results = getGridPage().getGrid().getItems();

        Assert.assertEquals(testPagingItems.get(targetPage - 1).getOrderId(), results.stream().findFirst().get().getOrderId());
        assertPagerInfoLabel(targetPage, targetPage, testPagingItems.stream().count());
    }

    public void goToFirstPageButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 1;
        getGridPage().getGoToFirstPageButton().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());

        Assert.assertFalse(getGridPage().getGoToFirstPageButton().isEnabled());
    }

    public void goToPreviousPageButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 1;
        getGridPage().getGoToFirstPageButton().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());

        Assert.assertFalse(getGridPage().getGoToPreviousPage().isEnabled());
    }

    public void previousMorePagesButtonDisabled_WhenFirstPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(11);
        var targetPage = 1;
        getGridPage().getGoToFirstPageButton().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());

        Assert.assertFalse(getGridPage().getPreviousMorePages().isDisplayed());
    }

    public void goToLastPageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        getGridPage().getGoToLastPage().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());

        Assert.assertFalse(getGridPage().getGoToLastPage().isEnabled());
    }

    public void goToNextPageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        getGridPage().getGoToLastPage().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());

        Assert.assertFalse(getGridPage().getGoToNextPage().isEnabled());
    }

    public void nextMorePageButtonDisabled_WhenLastPageIsLoaded() throws Exception {
        initializeInvoicesForPaging();
        navigateToGridInitialPage(1);
        var targetPage = 11;
        getGridPage().getGoToLastPage().click();
        waitForPageToLoad(targetPage, getGridPage().getGrid());

        Assert.assertFalse(getGridPage().getPreviousMorePages().isEnabled());
    }

    private void navigateToGridInitialPage(int initialPageNumber) throws Exception {
        navigateToGridInitialPage(1);
        getGridPage().getGrid().filter(GridColumns.SHIP_NAME, FilterOperator.EQUAL_TO, uniqueShippingName);
        getGridPage().getGrid().changePageSize(1);
        waitForGridToLoad(1, getGridPage().getGrid());
        getGridPage().getGrid().navigateToPage(initialPageNumber);
        waitForPageToLoad(initialPageNumber, getGridPage().getGrid());

        assertPagerInfoLabel(initialPageNumber, initialPageNumber, testPagingItems.stream().count());
    }

    private void assertPagerInfoLabel(int startItems, int endItems, long totalItems) {
        var expectedLabel = String.format("%s - %s of %s items", startItems, endItems, totalItems);
        var pagerInfoLabel = driver.findElement(By.xpath("//*[@id='grid']/div[3]/span"));
        Assert.assertEquals(pagerInfoLabel.getText(), expectedLabel);
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
