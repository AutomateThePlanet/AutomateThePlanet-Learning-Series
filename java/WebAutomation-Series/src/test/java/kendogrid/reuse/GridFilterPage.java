package kendogrid.reuse;

import kendogrid.components.KendoGrid;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class GridFilterPage implements IGridPage {
    public final String Url = "http://demos.telerik.com/kendo-ui/grid/filter-row";
    private final WebDriver driver;

    public GridFilterPage(WebDriver driver)
    {
        this.driver = driver;
        PageFactory.initElements(driver, this);
    }

    @FindBy(xpath = "//*[@id='grid']/div[3]/span") public WebElement pagerInfoLabel;
    @FindBy(xpath = "//*[@id='grid']/div[3]/a[3]") public WebElement goToNextPage;
    @FindBy(xpath = "//*[@id='grid']/div[3]/a[1]") public WebElement goToFirstPageButton;
    @FindBy(xpath = "//*[@id='grid']/div[3]/a[4]/span") public WebElement goToLastPage;
    @FindBy(xpath = "//*[@id='grid']/div[3]/a[2]/span") public WebElement goToPreviousPage;
    @FindBy(xpath = "//*[@id='grid']/div[3]/ul/li[12]/a") public WebElement nextMorePages;
    @FindBy(xpath = "//*[@id='grid']/div[3]/ul/li[2]/a") public WebElement previousMorePages;
    @FindBy(xpath = "//*[@id='grid']/div[3]/ul/li[2]/a") public WebElement pageOnFirstPositionButton;
    @FindBy(xpath = "//*[@id='grid']/div[3]/ul/li[3]/a") public WebElement pageOnSecondPositionButton;
    @FindBy(xpath = "//*[@id='grid']/div[3]/ul/li[11]/a") public WebElement pageOnTenthPositionButton;

    @Override
    public WebElement getPagerInfoLabel() {
        return pagerInfoLabel;
    }

    @Override
    public WebElement getGoToNextPage() {
        return goToNextPage;
    }

    @Override
    public WebElement getGoToFirstPageButton() {
        return goToFirstPageButton;
    }

    @Override
    public WebElement getGoToLastPage() {
        return goToLastPage;
    }

    @Override
    public WebElement getGoToPreviousPage() {
        return goToPreviousPage;
    }

    @Override
    public WebElement getNextMorePages() {
        return nextMorePages;
    }

    @Override
    public WebElement getPreviousMorePages() {
        return previousMorePages;
    }

    @Override
    public WebElement getPageOnFirstPositionButton() {
        return pageOnFirstPositionButton;
    }

    @Override
    public WebElement getPageOnSecondPositionButton() {
        return pageOnSecondPositionButton;
    }

    @Override
    public WebElement getPageOnTenthPositionButton() {
        return pageOnTenthPositionButton;
    }

    public KendoGrid getGrid() {
         return new KendoGrid(driver, driver.findElement(By.id("grid")));
    }

    public void navigateTo() {
        driver.navigate().to(Url);
        var consentButton = driver.findElement(By.id("onetrust-accept-btn-handler"));
        consentButton.click();
    }
}
