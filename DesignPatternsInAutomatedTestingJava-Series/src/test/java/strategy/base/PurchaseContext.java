package strategy.base;

import strategy.data.PurchaseInfo;
import strategy.pages.checkoutpage.CheckoutPage;
import strategy.pages.itempage.ItemPage;
import strategy.pages.shoppingcartpage.ShoppingCartPage;

public class PurchaseContext {
    private final OrderPurchaseStrategy orderPurchaseStrategy;

    public PurchaseContext(OrderPurchaseStrategy orderPurchaseStrategy) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
    }

    public void purchaseItem(String itemUrl, double itemPrice, PurchaseInfo purchaseInfo) {
        var itemPage = new ItemPage();
        var shoppingCartPage = new ShoppingCartPage();
        var checkoutPage = new CheckoutPage();
        itemPage.navigate(itemUrl);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        checkoutPage.fillBillingInfo(purchaseInfo);
        orderPurchaseStrategy.assertOrderSummary(itemPrice, purchaseInfo);
    }
}
