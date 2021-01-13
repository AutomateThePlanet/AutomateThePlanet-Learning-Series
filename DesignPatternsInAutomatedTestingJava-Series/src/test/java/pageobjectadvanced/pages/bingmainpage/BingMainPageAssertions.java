package pageobjectadvanced.pages.bingmainpage;

import org.testng.Assert;
import pageobjectadvanced.core.BaseAssertions;

public class BingMainPageAssertions extends BaseAssertions<BingMainPageElements> {
    public void resultsCount(String expectedCount) {
        Assert.assertTrue(elements().resultsCountDiv().getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
