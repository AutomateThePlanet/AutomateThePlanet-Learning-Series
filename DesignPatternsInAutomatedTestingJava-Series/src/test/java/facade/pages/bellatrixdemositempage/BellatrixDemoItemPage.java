package facade.pages.bellatrixdemositempage;

import facade.core.BasePage;
import facade.pages.interfaces.ItemPage;

public class BellatrixDemoItemPage extends BasePage<BellatrixDemoItemElements, BellatrixDemoItemAssertions> implements ItemPage {
    public BellatrixDemoItemPage() {
        super("http://demos.bellatrix.solutions/product/");
    }

    @Override
    public void clickBuyNowButton() {
        elements().addToCartButton().click();
    }

    @Override
    public void clickViewShoppingCartButton() {
        elements().viewShoppingCartButton().click();
    }

    @Override
    public void assertPrice(double itemPrice) {
        assertions().assertProductPrice(itemPrice);
    }
}
