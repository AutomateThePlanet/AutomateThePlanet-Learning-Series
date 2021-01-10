package pageobjectadvanced.pages.bingmainpage;

import pageobjectadvanced.core.BaseAssertions;
import org.testng.Assert;

public class BingMainAssertions extends BaseAssertions<BingMainElements> {

    public void resultsCount(String expectedCount) {
        Assert.assertTrue(elements().resultsCountDiv().getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
