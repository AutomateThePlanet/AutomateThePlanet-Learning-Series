package decorator.pages.shoppingcartpage;

import decorator.core.BaseAssertions;
import org.testng.Assert;

public class ShoppingCartAssertions extends BaseAssertions<ShoppingCartElements> {
    public void assertShoppingCartSubtotalPrice(double subtotalPrice) {
        Assert.assertEquals(elements().shoppingCartSubtotalPrice().getText(), formatCurrency(subtotalPrice));
    }
}
