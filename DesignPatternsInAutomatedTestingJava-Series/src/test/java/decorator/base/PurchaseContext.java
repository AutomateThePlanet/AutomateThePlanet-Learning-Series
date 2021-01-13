package decorator.base;

import decorator.strategies.OrderPurchaseStrategy;
import decorator.data.PurchaseInfo;
import decorator.pages.checkoutpage.CheckoutPage;
import decorator.pages.itempage.ItemPage;
import decorator.pages.shoppingcartpage.ShoppingCartPage;

public class PurchaseContext {
    private final OrderPurchaseStrategy orderPurchaseStrategy;

    public PurchaseContext(OrderPurchaseStrategy orderPurchaseStrategy) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
    }

    public void purchaseItem(String itemUrl, PurchaseInfo clientPurchaseInfo) {
        var itemPage = new ItemPage();
        var shoppingCartPage = new ShoppingCartPage();
        var checkoutPage = new CheckoutPage();
        itemPage.navigate(itemUrl);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        checkoutPage.fillBillingInfo(clientPurchaseInfo);
        var expectedTotalPrice = orderPurchaseStrategy.calculateTotalPrice();
        orderPurchaseStrategy.assertOrderSummary(expectedTotalPrice);
    }
}
