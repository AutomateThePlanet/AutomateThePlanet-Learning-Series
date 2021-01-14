package strategyadvanced.pages.shoppingcartpage;

import org.testng.Assert;
import strategyadvanced.core.BaseAssertions;

public class ShoppingCartAssertions extends BaseAssertions<ShoppingCartElements> {
    public void assertShoppingCartSubtotalPrice(double subtotalPrice) {
        Assert.assertEquals(elements().shoppingCartSubtotalPrice().getText(), formatCurrency(subtotalPrice));
    }
}
