package strategy.pages.itempage;

import org.testng.Assert;
import strategy.core.BaseAssertions;

public class ItemAssertions extends BaseAssertions<ItemElements> {
    public void assertProductPrice(double productPrice) {
        Assert.assertEquals(elements().productPrice().getText(), formatCurrency(productPrice));
    }
}
