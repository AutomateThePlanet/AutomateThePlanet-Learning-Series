package strategy.base;

import strategy.data.PurchaseInfo;
import strategy.pages.checkoutpage.CheckoutPage;
import strategy.pages.itempage.ItemPage;
import strategy.pages.shoppingcartpage.ShoppingCartPage;

public class PurchaseContext {
    private final OrderPurchaseStrategy orderPurchaseStrategy;
    private final ItemPage itemPage;
    private final ShoppingCartPage shoppingCartPage;
    private final CheckoutPage checkoutPage;

    public PurchaseContext(OrderPurchaseStrategy orderPurchaseStrategy) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
        itemPage = new ItemPage();
        shoppingCartPage = new ShoppingCartPage();
        checkoutPage = new CheckoutPage();
    }

    public void purchaseItem(String itemUrl, double itemPrice, PurchaseInfo purchaseInfo) {
        itemPage.navigate(itemUrl);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        checkoutPage.fillBillingInfo(purchaseInfo);
        orderPurchaseStrategy.assertOrderSummary(itemPrice, purchaseInfo);
    }
}
