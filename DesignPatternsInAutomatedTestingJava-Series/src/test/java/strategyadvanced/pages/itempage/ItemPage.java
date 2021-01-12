package strategyadvanced.pages.itempage;

import strategyadvanced.core.BasePage;

public class ItemPage extends BasePage<ItemElements, ItemAssertions> {
    public ItemPage() {
        super("http://demos.bellatrix.solutions/product/");
    }

    public void clickBuyNowButton() {
        elements().addToCartButton().click();
    }

    public void clickViewShoppingCartButton() {
        elements().viewShoppingCartButton().click();
    }
}
