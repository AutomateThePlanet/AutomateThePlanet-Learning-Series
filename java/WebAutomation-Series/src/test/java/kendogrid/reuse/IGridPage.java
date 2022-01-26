package kendogrid.reuse;

import kendogrid.components.KendoGrid;
import org.openqa.selenium.WebElement;

public interface IGridPage {
    KendoGrid getGrid();
    WebElement getPagerInfoLabel();
    WebElement getGoToNextPage();
    WebElement getGoToFirstPageButton();
    WebElement getGoToLastPage();
    WebElement getGoToPreviousPage();
    WebElement getNextMorePages();
    WebElement getPreviousMorePages();
    WebElement getPageOnFirstPositionButton();
    WebElement getPageOnSecondPositionButton();
    WebElement getPageOnTenthPositionButton();
    void navigateTo();
}
