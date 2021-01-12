package pageobjectadvanced.pages.bingmainpage;

import pageobjectadvanced.core.BaseAssertions;
import org.testng.Assert;

public class BingMainPageAssertions extends BaseAssertions<BingMainPageElements> {
    public void resultsCount(String expectedCount) {
        Assert.assertTrue(elements().resultsCountDiv().getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
