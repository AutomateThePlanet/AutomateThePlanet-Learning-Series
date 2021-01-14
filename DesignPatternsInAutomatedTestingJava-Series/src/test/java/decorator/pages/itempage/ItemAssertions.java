package decorator.pages.itempage;

import decorator.core.BaseAssertions;
import org.testng.Assert;

public class ItemAssertions extends BaseAssertions<ItemElements> {
    public void assertProductPrice(double productPrice) {
        Assert.assertEquals(elements().productPrice().getText(), formatCurrency(productPrice));
    }
}
