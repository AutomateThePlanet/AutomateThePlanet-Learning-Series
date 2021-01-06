package com.automatetheplanet.pageobject_advanced.Pages.BingMainPage;

import com.automatetheplanet.pageobject_advanced.BasePageAsserts;
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
