package facade.pages.bellatrixdemositempage;

import facade.core.BaseAssertions;
import org.testng.Assert;

public class BellatrixDemoItemAssertions extends BaseAssertions<BellatrixDemoItemElements> {
    public void assertProductPrice(double productPrice) {
        Assert.assertEquals(elements().productPrice().getText(), formatCurrency(productPrice));
    }
}
