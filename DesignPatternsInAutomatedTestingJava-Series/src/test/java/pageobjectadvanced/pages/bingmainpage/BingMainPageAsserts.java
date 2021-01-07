package pageobjectadvanced.pages.bingmainpage;

import pageobjectadvanced.BasePageAsserts;
import org.testng.Assert;

public class BingMainPageAsserts extends BasePageAsserts {
    @Override
    protected BingMainPageElements elements() {
        return new BingMainPageElements();
    }

    public void resultsCount(String expectedCount) {
        Assert.assertTrue(elements().resultsCountDiv().getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
