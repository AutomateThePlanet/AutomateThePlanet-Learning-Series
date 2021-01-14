package facade.pages.bellatrixdemosshoppingcartpage;

import facade.core.BaseAssertions;
import org.testng.Assert;

public class BellatrixDemoShoppingCartAssertions extends BaseAssertions<BellatrixDemoShoppingCartElements> {
    public void assertShoppingCartSubtotalPrice(double subtotalPrice) {
        Assert.assertEquals(elements().shoppingCartSubtotalPrice().getText(), formatCurrency(subtotalPrice));
    }
}
