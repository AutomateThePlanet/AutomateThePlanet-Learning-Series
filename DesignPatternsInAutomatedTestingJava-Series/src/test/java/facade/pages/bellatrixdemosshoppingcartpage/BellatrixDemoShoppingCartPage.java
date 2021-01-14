package facade.pages.bellatrixdemosshoppingcartpage;

import facade.core.BasePage;
import facade.pages.interfaces.ShoppingCartPage;

public class BellatrixDemoShoppingCartPage extends BasePage<BellatrixDemoShoppingCartElements, BellatrixDemoShoppingCartAssertions> implements ShoppingCartPage {
    public BellatrixDemoShoppingCartPage() {
        super("http://demos.bellatrix.solutions/checkout/");
    }

    @Override
    public void clickProceedToCheckoutButton() {
        elements().proceedToCheckoutButton().click();
    }

    @Override
    public void assertSubtotalAmount(double itemPrice) {
        assertions().assertShoppingCartSubtotalPrice(itemPrice);
    }
}
