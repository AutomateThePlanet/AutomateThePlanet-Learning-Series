package decorator.pages.shoppingcartpage;

import decorator.core.BasePage;

public class ShoppingCartPage extends BasePage<ShoppingCartElements, ShoppingCartAssertions> {
    public ShoppingCartPage() {
        super("http://demos.bellatrix.solutions/checkout/");
    }

    @Override
    public ShoppingCartAssertions assertions() {
        return new ShoppingCartAssertions();
    }

    public void clickProceedToCheckoutButton() {
        elements().proceedToCheckoutButton().click();
    }
}
