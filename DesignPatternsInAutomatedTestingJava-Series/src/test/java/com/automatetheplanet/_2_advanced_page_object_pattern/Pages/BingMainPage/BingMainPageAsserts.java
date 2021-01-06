package com.automatetheplanet._2_advanced_page_object_pattern.Pages.BingMainPage;

import com.automatetheplanet._2_advanced_page_object_pattern.BasePageAsserts;
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
